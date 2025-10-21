using System.Net;
using System.Net.Sockets;
using SocketGameProtocol;
using SocketMultiplayerGameServer.Controller;
using SocketMultiplayerGameServer.DAO;

namespace SocketMultiplayerGameServer.Servers;

public class Server
{
    private Socket _socket;
    private UDPServer us;

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
        us = new UDPServer(6667, this, _controllerManager);
    }

    private void StartAccept()
    {
        _socket.BeginAccept(AcceptCallback, null);
    }

    private void AcceptCallback(IAsyncResult iar)
    {
        Socket clientSocket = _socket.EndAccept(iar);
        _clients.Add(new Client(clientSocket, this , us));
        StartAccept();
    }
    
    public Client ClientFromUserName(string username)
    {
        foreach (var client in _clients)
        {
            if (client.GetUserInfo != null && client.GetUserInfo.UserName.Equals(username))
            {
                return client;
            }
        }

        return null;
    }

    public bool SetIEP(EndPoint ipEnd , string user)
    {
        foreach (var client in _clients)
        {
            if (client.GetUserInfo.UserName == user)
            {
                client.IEP = ipEnd;
                return true;
            }
        }

        return false;
    }

    public void HandleRequest(MainPack pack, Client client)
    {
        _controllerManager.HandleRequest(pack, client);
    }

    public void RemoveClient(Client client)
    {
        _clients.Remove(client);
        client = null;
    }

    public MainPack CreateRoom(Client client, MainPack pack)
    {
        try
        {
            Room room = new Room(client, pack.Roompack[0] , this);
            _rooms.Add(room);

            foreach (var p in room.GetplayerInfo())
            {
                pack.Playerpack.Add(p);
            }
            
            pack.Returncode = ReturnCode.Succeed;
            return pack;
        }
        catch
        {
            pack.Returncode = ReturnCode.Fail;
            return pack;
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
            if (_rooms.Count == 0)
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
        catch (Exception e)
        {
            pack.Returncode = ReturnCode.Fail;
            Console.WriteLine("查询房间失败");
            Console.WriteLine(e.Message);
        }

        return pack;
    }

    public MainPack JoinRoom(Client client , MainPack mainPack)
    {
        foreach (Room r in _rooms)
        {
            if (r.GetRoomInfo.Roomname.Equals(mainPack.Str))
            {
                if (r.GetRoomInfo.Statc == 0)
                {
                    //可以加入房间
                    r.Join(client);
                    mainPack.Roompack.Add(r.GetRoomInfo);
                    
                    foreach (var p in r.GetplayerInfo())
                    {
                        mainPack.Playerpack.Add(p);
                    }
                    
                    mainPack.Returncode = ReturnCode.Succeed;
                    return mainPack;
                }
                else
                {
                    mainPack.Returncode = ReturnCode.Fail;
                    return mainPack;
                }
            }
        }
        
        //没有此房间
        mainPack.Returncode = ReturnCode.NotRoom;
        return mainPack;
    }

    public MainPack ExitRoom(Client client, MainPack pack)
    {
        if (client.GetRoom == null)
        {
            pack.Returncode = ReturnCode.Fail;
            return pack;
        }
        
        client.GetRoom.Exit(this , client);
        pack.Returncode = ReturnCode.Succeed;
        return pack;
    }

    public void RemoveRoom(Room room)
    {
        _rooms.Remove(room);
    }

    public void Chat(Client client , MainPack pack)
    {
        pack.Str = client.GetUserInfo.UserName + ":" + pack.Str;
        
        client.GetRoom.Broadcast(client , pack);
    }
}