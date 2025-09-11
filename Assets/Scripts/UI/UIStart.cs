using UnityEngine;
using UnityEngine.UI;

public class UIStart : BasePanel
{
    private Button _loginBtn;
    
    private void Awake()
    {
        _loginBtn = transform.Find("LoginBtn").GetComponent<Button>();
    }
    
    private void Start()
    {
        _loginBtn.onClick.AddListener(OnStartClick);
    }
    
    private void OnStartClick()
    {
        UIManager.PushPanel(PanelType.Login);
    }
}
