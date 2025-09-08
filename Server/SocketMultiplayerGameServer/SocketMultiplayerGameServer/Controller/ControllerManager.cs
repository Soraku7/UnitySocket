using System.Reflection;
using SocketGameProtocol;
using SocketMultiplayerGameServer.Servers;

namespace SocketMultiplayerGameServer.Controller;

public class ControllerManager
{
    private Dictionary<RequestCode , BaseController> controllers = new Dictionary<RequestCode , BaseController>();

    private Server server;
    
    public ControllerManager(Server server)
    {
        this.server = server;   
        
        UserController userController = new UserController();
        controllers.Add(userController.GetRequestCode , userController);
    }

    public void HandleRequest(MainPack pack , Client client)
    {
        if (controllers.TryGetValue(pack.Requestcode, out BaseController controller))
        {
            string metname = pack.Actioncode.ToString();
            MethodInfo method = controller.GetType().GetMethod(metname);

            if (method == null)
            {
                Console.WriteLine("没有找到对应的处理方法");
                return;
            }
            
            object[] obj = new object[] { server , client , pack };
            object ret = method.Invoke(controller, obj);

            if (ret != null)
            {
                client.Send((MainPack)ret);
            }
        }
        else
        {
            Console.WriteLine("没有找到对应的controller处理");
        }
    }
}