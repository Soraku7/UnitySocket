using SocketGameProtocol;
using UI;
using UnityEngine;

namespace Request
{
    public class RoomExitRequest : BaseRequest
    {
        private bool _isExit = false;

        private UIRoom _uiRoom;
        
        public override void Awake()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.Exit;
            
            _uiRoom = GetComponent<UIRoom>();
            
            base.Awake();
        }

        private void Update()
        {
            if(_isExit)
            {
                _uiRoom.ExitRoomResponse();
                _isExit = false;
            }
        }

        public void SendRequest()
        {
            MainPack pack = new MainPack();
            pack.Requestcode = requestCode;
            pack.Actioncode = actionCode;
            pack.Str = "r";
            
            base.SendRequest(pack);
        }

        public override void OnResponse(MainPack pack)
        {
            _isExit = true;
        }
    }
}