using System.Net;
using System.Net.Sockets;

namespace SocketMultiplayerGameServer.Servers;

public class Server
{
    private Socket socket;
    private List<Client> clients = new List<Client>();

    public Server(int port)
    {
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
}