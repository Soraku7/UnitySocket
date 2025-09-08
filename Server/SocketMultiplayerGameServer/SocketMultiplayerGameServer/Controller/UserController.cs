using SocketGameProtocol;
using SocketMultiplayerGameServer.Servers;

namespace SocketMultiplayerGameServer.Controller;

public class UserController : BaseController
{
    public UserController()
    {
        requestCode = RequestCode.User;
    }

    //注册
    public MainPack Logon(Server server , Client client , MainPack pack)
    {
        if (server.Logon(client, pack))
        {
            pack.Returncode = ReturnCode.Succeed;
        }
        else
        {
            pack.Returncode = ReturnCode.Fail;
        }
        
        return pack;
    }
    
    //登录
    public MainPack Login(Server server , Client client , MainPack pack)
    {
        
    }
}