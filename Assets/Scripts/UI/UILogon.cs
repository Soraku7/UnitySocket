using System;
using Request;
using SocketGameProtocol;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UILogon : BasePanel
    {
        private LogonRequest _logonRequest;
        private InputField _user, _pass;
        private Button _logonBtn , _loginBtn;

        private void Awake()
        {
            _logonRequest = transform.GetComponent<LogonRequest>();
            _user = transform.Find("UserInput").GetComponent<InputField>();
            _pass = transform.Find("PassInput").GetComponent<InputField>();
            _logonBtn = transform.Find("LogonBtn").GetComponent<Button>();
            _loginBtn = transform.Find("LoginBtn").GetComponent<Button>();
        }
        
        private void Start()
        {
            _logonBtn.onClick.AddListener(OnLogonClick);
            _loginBtn.onClick.AddListener(OnLoginClick);
        }

        private void OnLogonClick()
        {
            if(_user.text == "" || _pass.text == "")
            {
                Debug.Log("用户名或密码不能为空");
                return;
            }
            Debug.Log(_user.text + " " + _pass.text);
            _logonRequest.SendRequest(_user.text , _pass.text);
        }

        private void OnLoginClick()
        {
            UIManager.PopPanel();
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
        }
        
        public override void OnExit()
        {
            base.OnExit();
        }
        
        public override void OnPause()
        {
            base.OnPause();
        }
        
        public override void OnRecovery()
        {
            base.OnRecovery();
        }
        
        public void OnResponse(MainPack pack)
        {
            switch (pack.Returncode)
            {
                case ReturnCode.Succeed:
                    UIManager.ShowMessage("注册成功" , true);
                    UIManager.PushPanel(PanelType.RoomList);
                    break;
                
                case ReturnCode.Fail:
                    UIManager.ShowMessage("注册失败" , true);
                    break;
            }
        }

    }
}
