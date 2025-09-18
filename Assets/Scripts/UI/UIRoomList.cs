using System;
using System.Collections.Generic;
using Request;
using SocketGameProtocol;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIRoomList : BasePanel
    {
        
        private Button _backBtn;
        private Button _findRoomBtn;
        private Button _createRoomBtn;
        
        private InputField _roomName;

        private Slider _roomPlayerNum;
        
        private Transform _roomListContent;
        public GameObject roomItem;
        
        private CreateRoomRequest _createRoomRequest;
        private FindRoomRequest _findRoomRequest;

        private void Awake()
        {
            _backBtn = transform.Find("ButtonList/BackBtn").GetComponent<Button>();
            _findRoomBtn = transform.Find("ButtonList/FindRoomBtn").GetComponent<Button>();
            _createRoomBtn = transform.Find("ButtonList/CreateRoomBtn").GetComponent<Button>();
            
            _roomName = transform.Find("ButtonList/RoomName").GetComponent<InputField>();
            
            _roomPlayerNum = transform.Find("ButtonList/RoomPlayerNum").GetComponent<Slider>();
            
            _roomListContent = transform.Find("RoomListContent");
            
            _createRoomRequest = transform.GetComponent<CreateRoomRequest>();
            _findRoomRequest = transform.GetComponent<FindRoomRequest>();
        }

        private void Start()
        {
            _backBtn.onClick.AddListener(OnBackClick);
            _findRoomBtn.onClick.AddListener(OnFindRoomClick);
            _createRoomBtn.onClick.AddListener(OnCreateRoomClick);
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        private void OnBackClick()
        {
            UIManager.PopPanel();
        }
        
        private void OnFindRoomClick()
        {
            _findRoomRequest.SendRequest();
        }
        
        private void OnCreateRoomClick()
        {
            if (_roomName.text == null)
            {
                UIManager.ShowMessage("房间名不能为空");
                return;
            }
            _createRoomRequest.SendRequest(_roomName.text  , (int)_roomPlayerNum.value);
        }
        
        public void CreateRoomResponse(MainPack pack)
        {
            switch (pack.Returncode)
            {
                case ReturnCode.Succeed:
                    UIManager.ShowMessage("创建成功");
                    // UIManager.PushPanel(PanelType.Room);
                    break;
                case ReturnCode.Fail:
                    UIManager.ShowMessage("创建失败");
                    break;
            }
        }

        public void FindRoomResponse(MainPack pack)
        {
            switch (pack.Returncode)
            {
                case ReturnCode.Succeed:
                    UIManager.ShowMessage("查询成功  一共有" + pack.Roompack.Count + "个房间");
                    UpdateRoomList(pack);
                    break;
                case ReturnCode.Fail:
                    UIManager.ShowMessage("查询失败");
                    break;
                case ReturnCode.NotRoom:
                    UIManager.ShowMessage("没有房间");
                    break;
            }
        }
        
        private void UpdateRoomList(MainPack pack)
        {
            for(int i = 0 ; i < _roomListContent.childCount; i++)
            {
                Destroy(_roomListContent.GetChild(i).gameObject);
            }

            foreach (var roomPack in pack.Roompack)
            {
                RoomItem item = Instantiate(roomItem , _roomListContent).GetComponent<RoomItem>();
                item.SetRoomInfo(roomPack.Roomname , roomPack.Curnum , roomPack.Maxnum , roomPack.Statc);
            }
        }
    }
}