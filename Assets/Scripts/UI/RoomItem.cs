using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RoomItem : MonoBehaviour
    {
        private Button _joinBtn;
        private Text _titleTex;
        private Text _numTex;
        private Text _stateTex;

        private void Awake()
        {
            _joinBtn = transform.Find("RoomBtn").GetComponent<Button>();
            _titleTex = transform.Find("RoomBtn/TitleTex").GetComponent<Text>();
            _numTex = transform.Find("RoomBtn/NumTex").GetComponent<Text>();
            _stateTex = transform.Find("RoomBtn/StateTex").GetComponent<Text>();
        }

        private void Start()
        {
            _joinBtn.onClick.AddListener(OnJoinClick);
        }
        
        private void OnJoinClick()
        {
            Debug.Log("Join Room: " + _titleTex.text);
        }
        
        public void SetRoomInfo(string title, int curnum, int maxnum , int state)
        {
            _titleTex.text = title;
            _numTex.text = curnum + "/" + maxnum;
            switch (state)
            {
                case 0:
                    _stateTex.text = "等待加入";
                    break;
                case 1:
                    _stateTex.text = "房间人已满";
                    break;
                case 2:
                    _stateTex.text = "游戏中";
                    break;
            }
        }
    }
}
