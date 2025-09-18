using System;
using SocketGameProtocol;
using UI;
using UnityEngine;

namespace Request
{
    public class LogonRequest : BaseRequest 
    {
        private UILogon _uiLogon; 
        
        private MainPack _pack;
        
        public override void Awake()
        {
            requestCode = RequestCode.User;
            actionCode = ActionCode.Logon;
            
            _uiLogon = transform.GetComponent<UILogon>();
            base.Awake();
        }

        private void Update()
        {
            if(_pack != null)
            {
                _uiLogon.OnResponse(_pack);
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
}