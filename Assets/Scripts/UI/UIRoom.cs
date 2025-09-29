using System;
using Request;
using SocketGameProtocol;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class UIRoom : BasePanel
    {
        private Button _backBtn;
        private Button _sendBtn;
        private Button _startBtn;

        private InputField _inputText;
        private Scrollbar _scrollbar;

        private Transform _userListcontent;

        public GameObject userItem;

        private RoomExitRequest _roomExitRequest;

        private void Awake()
        {
            _backBtn = transform.Find("OtherList/BackBtn").GetComponent<Button>();
            _sendBtn = transform.Find("OtherList/SendBtn").GetComponent<Button>();
            _startBtn = transform.Find("OtherList/StartBtn").GetComponent<Button>();

            _inputText = transform.Find("OtherList/InputField").GetComponent<InputField>();
            _scrollbar = transform.Find("OtherList/Scrollbar").GetComponent<Scrollbar>();

            _userListcontent = transform.Find("UserList");

            _roomExitRequest = transform.GetComponent<RoomExitRequest>();
        }

        private void Start()
        {
            _backBtn.onClick.AddListener(OnBackClick);
            _sendBtn.onClick.AddListener(OnSendClick);
            _startBtn.onClick.AddListener(OnStartClick);
        }

        private void OnBackClick()
        {
            _roomExitRequest.SendRequest();
        }

        private void OnSendClick()
        {
        }

        private void OnStartClick()
        {
        }

        public void ExitRoomResponse()
        {
            UIManager.PopPanel();
        }

        public void UpdatePlayerList(MainPack pack)
        {
            for (int i = 0; i < _userListcontent.childCount; i++)
            {
                Destroy(_userListcontent.GetChild(i).gameObject);
            }

            Debug.Log("更新人数");
            foreach (PlayerPack playerPack in pack.Playerpack)
            {
                UserItem item = Instantiate(userItem, _userListcontent).GetComponent<UserItem>();
                item.SetUserInfo(playerPack.Playername);
            }
        }
    }
}