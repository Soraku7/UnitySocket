using System;
using Request;
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
            _logonRequest = transform.parent.GetComponent<LogonRequest>();
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

    }
}
