using SocketGameProtocol;

namespace SocketMultiplayerGameServer.Servers;

public class Room
{
    private RoomPack _roomPack;

    List<Client> _clients = new List<Client>();
    public RoomPack GetRoomInfo
    {
        get
        {
            return _roomPack;
        }
    }
    
    public Room(Client client , RoomPack pack)
    {
        _roomPack = pack;
        _clients.Add(client);
    }

    
}