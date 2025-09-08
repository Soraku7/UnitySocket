using System.Net;
using System.Net.Sockets;
using SocketGameProtocol;
using SocketMultiplayerGameServer.Controller;
using SocketMultiplayerGameServer.DAO;

namespace SocketMultiplayerGameServer.Servers;

public class Server
{
    private Socket socket;
    private List<Client> clients = new List<Client>();
    private ControllerManager controllerManager;

    public Server(int port)
    {
        controllerManager = new ControllerManager(this);
        
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind(new IPEndPoint(IPAddress.Any, port));
        socket.Listen(0);
        StartAccept();
    }
    
    private void StartAccept()
    {
        socket.BeginAccept(AcceptCallback, null);
    }
    
    private void AcceptCallback(IAsyncResult iar)
    {
        Socket clientSocket = socket.EndAccept(iar);
        clients.Add(new Client(socket));
        StartAccept();
    }

    public bool Logon(Client client , MainPack pack)
    {
        return client.GetUserData.Logon(pack);
    }
}