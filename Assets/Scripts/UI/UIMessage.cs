using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIMessage : BasePanel
    {
        private Text _text;
        private string _messageTex = null;

        private void Awake()
        {
            _text = transform.Find("Text").GetComponent<Text>();
        }

        private void Update()
        {
            if(_messageTex != null)
            {
                ShowText(_messageTex);
                _messageTex = null;
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _text.CrossFadeAlpha(0 , 0 , false);
            UIManager.SetUIMessage(this);
        }

        public override void OnPause()
        {
        }

        public void ShowMessage(string str , bool issync = false)
        {
            if (issync)
            {
                //异步显示
                _messageTex = str;
            }
            else
            {
                ShowText(str);
            }
        }

        private void ShowText(string str)
        {
            _text.text = str;
            _text.CrossFadeAlpha(1 , 0.1f , false);
            Invoke(nameof(HideMessage) , 1f);
        }

        private void HideMessage()
        {
            _text.CrossFadeAlpha(0 , 0.5f , false);
        }
        
    }
     
    
}