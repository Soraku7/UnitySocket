using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Manager
{
    public class UIManager : BaseManager
    {
        private Dictionary<PanelType , BasePanel> _panels = new Dictionary<PanelType, BasePanel>();
        private Stack<BasePanel> _panelStack = new Stack<BasePanel>();
        
        private Transform _canvasTransform;
        private UIMessage _message;
        
        public UIManager(GameFace face) : base(face)
        {
        }
        
        public override void OnInit()
        {
            base.OnInit();
            _canvasTransform = GameObject.Find("Canvas").transform;
            
            InitPanel();
            PushPanel(PanelType.Message);
            PushPanel(PanelType.Start);
        }
        
        /// <summary>
        /// 显示UI
        /// </summary>
        /// <param name="panelType"></param>
        /// <returns></returns>
        public BasePanel PushPanel(PanelType panelType)
        {
            if(_panels.TryGetValue(panelType , out var panel))
            {
                if(_panelStack.Count > 0)
                {
                    BasePanel topPanel = _panelStack.Peek();
                    
                    topPanel.OnPause();
                }
                
                _panelStack.Push(panel);
                panel.OnEnter();
                return panel;
            }

            BasePanel newPanel = SpawnPanel(panelType);
            Debug.Log("显示" + newPanel.name);
            if (_panelStack.Count > 0)
            {
                BasePanel topPanel = _panelStack.Peek();
                topPanel.OnPause();
            }
                
            _panelStack.Push(newPanel);
            newPanel.OnEnter(); 
            return newPanel;
        }
        
        /// <summary>
        /// 关闭UI
        /// </summary>
        public void PopPanel()
        {
            if(_panelStack.Count <= 0) return;
            
            BasePanel topPanel = _panelStack.Pop();
            topPanel.OnExit();
            
            if(_panelStack.Count <= 0) return;
            
            BasePanel nextPanel = _panelStack.Peek();
            nextPanel.OnRecovery();
        }
        
        /// <summary>
        /// 实例化UI
        /// </summary>
        /// <param name="panelType"></param>
        /// <returns></returns>
        private BasePanel SpawnPanel(PanelType panelType)
        {
            if (_panels.TryGetValue(panelType, out var panel)) return panel;
            
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/UI{panelType}");

            if (prefab == null)
            {
                Debug.LogWarning($"未找到UI Prefab: Prefabs/UI/UI{panelType}");
                return null;
            }
            
            GameObject panelObj = Object.Instantiate(prefab, _canvasTransform);
            panelObj.transform.SetParent(_canvasTransform);

            BasePanel newPanel = panelObj.GetComponent<BasePanel>();
            _panels.Add(panelType , newPanel);
            newPanel.SetUIManager = this;
            
            return newPanel;
        }

        private void InitPanel()
        {
            foreach (PanelType panelType in System.Enum.GetValues(typeof(PanelType)))
            {
                // 加载Prefab
                GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/UI{panelType}");
                if (prefab != null)
                {
                    if (prefab.TryGetComponent<BasePanel>(out var component))
                    {
                    }
                    else
                    {
                        Debug.LogWarning($"当前{prefab.name}缺少BasePanel组件");
                    }
                }
                else
                {
                    Debug.LogWarning($"未找到UI Prefab: Prefabs/UI/UI{panelType}");
                }
            }
        }

        public void SetUIMessage(UIMessage message)
        {
            _message = message;
        }

        public void ShowMessage(string str , bool sync = false)
        {
            _message.ShowMessage(str , sync);
        }
    }
}