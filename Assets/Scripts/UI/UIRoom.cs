using System;
using System.Collections.Generic;
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

        private Text _chatText;

        private Transform _userListcontent;

        public GameObject userItem;

        private RoomExitRequest _roomExitRequest;
        private ChatRequest _chatRequest;
        private StartGameRequest _startGameRequest;

        private void Awake()
        {
            _backBtn = transform.Find("OtherList/BackBtn").GetComponent<Button>();
            _sendBtn = transform.Find("OtherList/SendBtn").GetComponent<Button>();
            _startBtn = transform.Find("OtherList/StartBtn").GetComponent<Button>();

            _inputText = transform.Find("OtherList/InputField").GetComponent<InputField>();
            _scrollbar = transform.Find("OtherList/Scrollbar").GetComponent<Scrollbar>();
            
            _chatText = transform.Find("OtherList/MessageList/Text").GetComponent<Text>();

            _userListcontent = transform.Find("UserList");

            _roomExitRequest = transform.GetComponent<RoomExitRequest>();
            _chatRequest = transform.GetComponent<ChatRequest>();
            _startGameRequest = transform.GetComponent<StartGameRequest>();
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
            if (_inputText.text == "")
            {
                UIManager.ShowMessage("发送内容不能为空");
                return;
            }
            _chatRequest.SendRequest(_inputText.text);
            _chatText.text += "我:" + _inputText.text + "\n";
            _inputText.text = "";
        }

        private void OnStartClick()
        {
            _startGameRequest.SendRequest();
        }

        public void ExitRoomResponse()
        {
            UIManager.PopPanel();
        }

        public void ChatResponse(string str)
        {
            Debug.Log(str);
            _chatText.text += str + "\n";
        }

        public void StartGameResponse(MainPack pack)
        {
            switch (pack.Returncode)
            {
                case ReturnCode.Fail:
                    UIManager.ShowMessage("游戏开始失败,您不是房主");
                    break;
                
                case ReturnCode.Succeed:
                    UIManager.ShowMessage("游戏开始");
                    break;
            }
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

        public void GameStarting(MainPack packs)
        {
            UIGame uiGame = UIManager.PushPanel(PanelType.Game).GetComponent<UIGame>();
            uiGame.UpdateList(packs);
        }
    }
}