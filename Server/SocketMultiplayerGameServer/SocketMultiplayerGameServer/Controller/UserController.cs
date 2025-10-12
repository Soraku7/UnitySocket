using MySql.Data.MySqlClient;
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
        Console.WriteLine("注册");
        if (client.GetUserData.Logon(pack , client.GetMysqlConnection))
        {
            pack.Returncode = ReturnCode.Succeed;
        }
        else
        {
            pack.Returncode = ReturnCode.Fail;
            Console.WriteLine("注册失败");
        }
        
        return pack;
    }
    
    //登录
    public MainPack Login(Server server , Client client , MainPack pack)
    {
        if (client.GetUserData.Login(pack, client.GetMysqlConnection))
        {
            pack.Returncode = ReturnCode.Succeed;
            client.GetUserInfo.UserName = pack.Loginpack.Username;
        }
        else
        {
            pack.Returncode = ReturnCode.Fail;
            Console.WriteLine("登录失败");
        }

        return pack;
    }
}