using System;
using Request;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UILogon : MonoBehaviour
    {
        private LogonRequest logonRequest;
        private InputField user, pass;
        private Button logonBtn;

        private void Awake()
        {
            logonRequest = transform.parent.GetComponent<LogonRequest>();
            user = transform.Find("UserInput").GetComponent<InputField>();
            pass = transform.Find("PassInput").GetComponent<InputField>();
            logonBtn = transform.Find("LogonBtn").GetComponent<Button>();
        }

        private void Start()
        {
            logonBtn.onClick.AddListener(OnLogonClick);
        }

        private void OnLogonClick()
        {
            if(user.text == "" || pass.text == "")
            {
                Debug.Log("用户名或密码不能为空");
                return;
            }
            Debug.Log(user.text + " " + pass.text);
            logonRequest.SendRequest(user.text , pass.text);
        }
    }
}
