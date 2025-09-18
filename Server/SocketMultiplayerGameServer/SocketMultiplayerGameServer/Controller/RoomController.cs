using SocketGameProtocol;
using SocketMultiplayerGameServer.Servers;

namespace SocketMultiplayerGameServer.Controller;

public class RoomController : BaseController
{
    public RoomController()
    {
        requestCode = RequestCode.Room;
    }
    
    public MainPack CreateRoom(Server server , Client client , MainPack pack)
    {
        pack.Returncode = server.CreateRoom(client, pack);
        return pack;
    }

    public MainPack FindRoom(Server server , Client client , MainPack pack)
    {
        return server.FindRoom();
    }
}