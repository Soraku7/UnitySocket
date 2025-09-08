using System.Net.Sockets;
using SocketGameProtocol;
using SocketMultiplayerGameServer.DAO;
using SocketMultiplayerGameServer.Tools;

namespace SocketMultiplayerGameServer.Servers;

public class Client
{
    private Socket socket;
    private Message message;    
    private UserData userData;
    private Server server;
    
    public UserData GetUserData
    {
        get { return userData; }
    }
    
    public Client(Socket socket , Server server)
    {
        this.socket = socket;
        this.server = server;
        userData = new UserData();
        message = new Message();
        StartRecieve();
    }

    private void StartRecieve()
    {
        socket.BeginReceive(message.Buffer, message.StartIndex, message.RemSize, SocketFlags.None, ReceiveCallback , null);
    }
    
    private void ReceiveCallback(IAsyncResult iar)
    {
        try
        {
            if (socket == null || socket.Connected == false) return;
            
            int len = socket.EndReceive(iar);
            if(len <= 0) return;
        
            message.ReadBuffer(len , HandleRequest);
            StartRecieve();
        }
        catch
        {
            
        }

    }

    public void Send(MainPack pack)
    {
        socket.Send(Message.PackData(pack));
    }
    
    private void HandleRequest(MainPack pack)
    {
        server.HandleRequest(pack , this);
    }
    
    public bool Logon(MainPack pack)
    {
        return GetUserData.Logon(pack);
    }
}