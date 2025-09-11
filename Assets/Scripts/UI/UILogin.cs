using Request;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UILogin : BasePanel
    {
        private LogonRequest loginRequest;
        private InputField user, pass;
        private Button logonBtn , loginBtn;

        private void Awake()
        {
            loginRequest = transform.parent.GetComponent<LogonRequest>();
            user = transform.Find("UserInput").GetComponent<InputField>();
            pass = transform.Find("PassInput").GetComponent<InputField>();
            logonBtn = transform.Find("LogonBtn").GetComponent<Button>();
            loginBtn = transform.Find("LoginBtn").GetComponent<Button>();
        }
        
        private void Start()
        {
            logonBtn.onClick.AddListener(OnLogonClick);
            loginBtn.onClick.AddListener(OnLoginClick);
        }

        private void OnLoginClick()
        {
            if(user.text == "" || pass.text == "")
            {
                Debug.Log("用户名或密码不能为空");
                return;
            }
            Debug.Log(user.text + " " + pass.text);
            loginRequest.SendRequest(user.text , pass.text);
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

    }
}