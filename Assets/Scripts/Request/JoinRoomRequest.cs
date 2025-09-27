using System;
using SocketGameProtocol;
using UI;

namespace Request
{
    public class JoinRoomRequest : BaseRequest
    {
        private MainPack _mainPack;
        
        private UIRoomList _uiRoomList;
        
        public override void Awake()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.JoinRoom;
            
            _uiRoomList = GetComponent<UIRoomList>();
            
            base.Awake();
        }

        private void Update()
        {
            if(_mainPack != null)
            {
                //处理UI
                _uiRoomList.JoinRoomResponse(_mainPack);
                _mainPack = null;
            }
        }

        public void SendRequest(string roomName)
        {
            MainPack pack = new MainPack();
            pack.Requestcode = requestCode;
            pack.Actioncode = actionCode;

            pack.Str = roomName;
            base.SendRequest(pack);
        }
        
        public override void OnResponse(MainPack pack)
        {
            _mainPack = pack;
        }
    }
}