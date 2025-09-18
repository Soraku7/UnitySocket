using SocketGameProtocol;
using UI;

namespace Request
{
    public class FindRoomRequest : BaseRequest
    {
        private MainPack _pack;
        private UIRoomList _uiRoomList;
        public override void Awake()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.FindRoom;
            
            _uiRoomList = transform.GetComponent<UIRoomList>();

            base.Awake();
        }
        
        
        private void Update()
        {
            if(_pack != null)
            {
                _uiRoomList.FindRoomResponse(_pack);
                _pack = null;
            }
        }

        public void SendRequest()
        {
            MainPack mainPack = new MainPack();
            mainPack.Requestcode = requestCode;
            mainPack.Actioncode = actionCode;
            mainPack.Str = "r";
            base.SendRequest(mainPack);
        }

        public override void OnResponse(MainPack pack)
        {
            this._pack = pack;
        }
    }
}