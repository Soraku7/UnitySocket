using UnityEngine;
using UnityEngine.UI;

public class UIStart : BasePanel
{
    private Button loginBtn;
    
    private void Awake()
    {
        loginBtn = transform.Find("LoginBtn").GetComponent<Button>();
    }
    
    private void Start()
    {
        loginBtn.onClick.AddListener(OnStartClick);
    }
    
    private void OnStartClick()
    {
        UIManager.PushPanel(PanelType.Logon);
    }
}
