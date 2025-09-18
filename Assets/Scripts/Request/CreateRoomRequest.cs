using System;
using SocketGameProtocol;
using UI;

namespace Request
{
    public class CreateRoomRequest : BaseRequest
    {
        private MainPack _pack;
        
        private UIRoomList _uiRoomList;
        
        public override void Awake()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.CreateRoom;
            
            _uiRoomList = transform.GetComponent<UIRoomList>();
            
            base.Awake();
        }

        private void Update()
        {
            if(_pack != null)
            {
                _uiRoomList.CreateRoomResponse(_pack);
                _pack = null;
            }
        }

        public void SendRequest(string roomName , int maxnum)
        {
            MainPack mainPack = new MainPack();
            mainPack.Requestcode = requestCode;
            mainPack.Actioncode = actionCode;
            
            RoomPack roomPack = new RoomPack();
            roomPack.Roomname = roomName;
            roomPack.Maxnum = maxnum;
            
            mainPack.Roompack.Add(roomPack);
            
            base.SendRequest(mainPack);
        }

        public override void OnResponse(MainPack pack)
        {
            this._pack = pack;
        }
    }
}