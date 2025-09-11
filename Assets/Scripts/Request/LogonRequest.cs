using SocketGameProtocol;
using UnityEngine;

namespace Request
{
    public class LogonRequest : BaseRequest 
    {
        public override void Awake()
        {
            requestCode = RequestCode.User;
            actionCode = ActionCode.Logon;
            base.Awake();
        }
        
        public override void OnResponse(MainPack pack)
        {
            switch (pack.Returncode)
            {
                case ReturnCode.Succeed:
                    GameFace.Instance.ShowMessage("注册成功" , true);
                    break;
                
                case ReturnCode.Fail:
                    GameFace.Instance.ShowMessage("注册失败" , true);
                    break;
            }
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