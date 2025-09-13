using Request;
using SocketGameProtocol;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UILogin : BasePanel
    {
        private LoginRequest _loginRequest;
        private InputField _user, _pass;
        private Button _logonBtn , _loginBtn;

        private void Awake()
        {
            _loginRequest = transform.parent.GetComponent<LoginRequest>();
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

        private void OnLoginClick()
        {
            if(_user.text == "" || _pass.text == "")
            {
                Debug.Log("用户名或密码不能为空");
                return;
            }
            Debug.Log(_user.text + " " + _pass.text);
            _loginRequest.SendRequest(_user.text , _pass.text);
        }

        private void OnLogonClick()
        {
           UIManager.PushPanel(PanelType.Logon);
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
                    UIManager.ShowMessage("登录成功" , true);
                    UIManager.PushPanel(PanelType.RoomList);
                    break;
                
                case ReturnCode.Fail:
                    UIManager.ShowMessage("登录失败" , true);
                    break;
            }
        }
    }
}