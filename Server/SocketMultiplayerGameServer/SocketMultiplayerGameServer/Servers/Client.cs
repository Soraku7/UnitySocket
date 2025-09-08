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
    
    public UserData GetUserData
    {
        get { return userData; }
    }
    
    public Client(Socket socket)
    {
        this.socket = socket;
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
        
            message.ReadBuffewr(len);
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
}