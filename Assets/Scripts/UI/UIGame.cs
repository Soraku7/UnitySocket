using System;
using System.Collections.Generic;
using SocketGameProtocol;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGame : BasePanel
    {
        public GameObject itemPrefab;
        private Transform _listTransform;
        private Text _timeText;
        private Button _exitButton;

        private float _curTime;

        private Dictionary<string , PlayerInfoItem> _itemList = new Dictionary<string , PlayerInfoItem>();
        private void Awake()
        {
            _listTransform = transform.Find("List");
            _timeText = transform.Find("Time/Text").GetComponent<Text>();
            _exitButton = transform.Find("ExitBtn").GetComponent<Button>();
        }

        private void Start()
        {
            _curTime = Time.time;
            
            _exitButton.onClick.AddListener(OnExitClick);
        }

        private void FixedUpdate()
        { 
            _timeText.text = Mathf.Clamp((int)(Time.time - _curTime), 0, 300).ToString();
        }

        public void UpdateValue(string id , int value)
        {
            if (_itemList.TryGetValue(id, out PlayerInfoItem playerItem))
            {
                playerItem.UpdateHp(value);
            }
            else
            {
                Debug.Log("获取不到对应角色信息");
            }
        }

        private void OnExitClick()
        {
            
        }

        public void UpdateList(List<PlayerPack> packs)
        {
            foreach (var p in packs)
            {
                GameObject item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity , _listTransform);
                PlayerInfoItem playerItem = item.GetComponent<PlayerInfoItem>();
                playerItem.Set(p.Playername , p.Hp);
                _itemList.Add(p.PlayerID , playerItem);
            }
        }
    }
}