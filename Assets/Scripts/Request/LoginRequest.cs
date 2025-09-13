using Request;
using SocketGameProtocol;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class LoginRequest : BaseRequest
{
    public UILogin uiLogin;
    
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        
        uiLogin = transform.GetComponent<UILogin>();
        
        base.Awake();
    }

    public override void OnResponse(MainPack pack)
    {
        uiLogin.OnResponse(pack);
    }

    public void SendRequest(string user , string pass)
    {
        MainPack pack = new MainPack();
            
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        LoginPack loginPack = new LoginPack();
            
        loginPack.Username = user;
        loginPack.Password = pass;
        pack.Loginpack = loginPack; 
            
        base.SendRequest(pack);
    }
}
