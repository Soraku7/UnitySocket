using System;
using Request;
using SocketGameProtocol;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class LoginRequest : BaseRequest
{
    private UILogin _uiLogin;

    private MainPack _pack;
    
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        
        _uiLogin = transform.GetComponent<UILogin>();
        
        base.Awake();
    }

    private void Update()
    {
        if(_pack != null)
        {
            _uiLogin.OnResponse(_pack);
            _pack = null;
        }
    }

    public override void OnResponse(MainPack pack)
    {
        this._pack = pack;
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
