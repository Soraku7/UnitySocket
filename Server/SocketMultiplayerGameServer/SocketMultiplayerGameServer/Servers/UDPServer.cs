using System.Net;
using System.Net.Sockets;
using SocketGameProtocol;
using SocketMultiplayerGameServer.Controller;
using SocketMultiplayerGameServer.Tools;

namespace SocketMultiplayerGameServer.Servers;

public class UDPServer
{
    private Socket udpServer;
    //监听本地IP
    private IPEndPoint bindEP;
    //远程IP
    private EndPoint remoteEP;

    private Server _server;
    
    private ControllerManager _controllerManager;
    
    Byte[] _buffer = new Byte[1024];
    
    //接收线程
    Thread _receiveThread;

    public UDPServer(int port, Server server, ControllerManager controllerManager)
    {
        _server = server;
        _controllerManager = controllerManager;
        udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        bindEP = new IPEndPoint(IPAddress.Any, port);
        remoteEP = new IPEndPoint(IPAddress.Any, 0);
        udpServer.Bind(bindEP);
        _receiveThread = new Thread(ReceiveMsg);
        _receiveThread.Start();
        Console.WriteLine("UDP server started");
    }
    
    ~UDPServer()
    {
        if (_receiveThread != null)
        {
            _receiveThread.Abort();
            _receiveThread = null;
        }
    }

    public void ReceiveMsg()
    {
        while (true)
        {
            int len = udpServer.ReceiveFrom(_buffer, ref remoteEP);
            MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(_buffer , 0 , len);
            HandlerRequest(pack , remoteEP);
        }
    }

    public void HandlerRequest(MainPack pack, EndPoint endPoint)
    {
        Client client = _server.ClientFromUserName(pack.User);
        if (client.IEP == null)
        {
            client.IEP = endPoint;
        }
        _controllerManager.HandleRequest(pack, client , true);
    }
    
    public void SendTo(MainPack pack , EndPoint endPoint)
    {
        byte[] data = Message.PackDataUDP(pack);
        udpServer.SendTo(data , data.Length , SocketFlags.None , endPoint);
    }
}