using System;
using SocketGameProtocol;
using UI;

namespace Request
{
    public class StartGameRequest : BaseRequest
    {
        private MainPack _mainpack = null;
        private UIRoom _uiRoom;
        
        public override void Awake()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.StartGame;
            
            _uiRoom = GetComponent<UIRoom>();
            
            base.Awake();
        }

        private void Update()
        {
            if (_mainpack != null)
            {
                _uiRoom.StartGameResponse(_mainpack);
                _mainpack = null;
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
            _mainpack = pack;
        }
    }
}