using System.Net;
using System.Net.Sockets;
using SocketGameProtocol;
using SocketMultiplayerGameServer.Controller;
using SocketMultiplayerGameServer.DAO;

namespace SocketMultiplayerGameServer.Servers;

public class Server
{
    private Socket _socket;
    
    private List<Client> _clients = new List<Client>();
    private List<Room> _rooms = new List<Room>();
    
    private ControllerManager _controllerManager;

    public Server(int port)
    {
        _controllerManager = new ControllerManager(this);
        
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _socket.Bind(new IPEndPoint(IPAddress.Any, port));
        _socket.Listen(0);
        StartAccept();
    }
    
    private void StartAccept()
    {
        _socket.BeginAccept(AcceptCallback, null);
    }
    
    private void AcceptCallback(IAsyncResult iar)
    {
        Socket clientSocket = _socket.EndAccept(iar);
        _clients.Add(new Client(clientSocket , this));
        StartAccept();
    }
    
    public void HandleRequest(MainPack pack , Client client)
    {
        _controllerManager.HandleRequest(pack, client);
    }
    
    public void RemoveClient(Client client)
    {
        _clients.Remove(client);
        client = null;
    }
    
    public ReturnCode CreateRoom(Client client , MainPack pack)
    {
        try
        {
            Room room = new Room(client, pack.Roompack[0]);
            _rooms.Add(room);
            return ReturnCode.Succeed;
        }
        catch
        {
            return ReturnCode.Fail;
        }
    }

    public MainPack FindRoom()
    {
        MainPack pack = new MainPack
        {
            Actioncode = ActionCode.FindRoom
        };
        try
        {
            if(_rooms.Count == 0)
            {
                pack.Returncode = ReturnCode.NotRoom;
                return pack;
            }
            
            foreach (Room room in _rooms)
            {
                pack.Roompack.Add(room.GetRoomInfo);
            }
            pack.Returncode = ReturnCode.Succeed;
        }
        catch(Exception e)
        {
            pack.Returncode = ReturnCode.Fail;
            Console.WriteLine("查询房间失败");
            Console.WriteLine(e.Message);
        }

        return pack;
    }
}