using SocketGameProtocol;
using UI;
using UnityEngine;

namespace Request
{
    public class LogonRequest : BaseRequest 
    {
        public UILogon uiLogon; 
        
        public override void Awake()
        {
            requestCode = RequestCode.User;
            actionCode = ActionCode.Logon;
            
            uiLogon = transform.GetComponent<UILogon>();
            base.Awake();
        }
        
        public override void OnResponse(MainPack pack)
        {
            uiLogon.OnResponse(pack);
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
}