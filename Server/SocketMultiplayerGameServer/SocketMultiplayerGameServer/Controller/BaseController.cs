using SocketGameProtocol;

namespace SocketMultiplayerGameServer.Controller;

public abstract class BaseController
{
    protected RequestCode requestCode = RequestCode.RequestNone;
    
    public RequestCode GetRequestCode
    {
        get { return requestCode; }
    }
}