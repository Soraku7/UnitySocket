using SocketGameProtocol;
using SocketMultiplayerGameServer.Servers;

namespace SocketMultiplayerGameServer.Controller;

public class GameController : BaseController
{
    public GameController()
    {
        requestCode = RequestCode.Game;
    }
    
    public MainPack ExitGame(Server server, Client client, MainPack pack)
    {
        client.GetRoom.ExitGame(client);
        return null;
    }
    
    public MainPack UpPos(Server server , Client client , MainPack pack)
    {
        client.GetRoom.Boardcast(client , pack);
        client.UpPos(pack);//更新位置信息
        return null;
    }
}