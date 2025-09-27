using SocketGameProtocol;
using UI;

namespace Request
{
    public class PlayersRequest : BaseRequest
    {
        private MainPack _mainPack;
        
        private UIRoom _uiRoom;
        
        public override void Awake()
        {
            actionCode = ActionCode.PlayerList;
            
            _uiRoom = transform.GetComponent<UIRoom>();
            
            base.Awake();
        }
        
        private void Update()
        {
            if(_mainPack != null)
            {
                _uiRoom.UpdatePlayerList(_mainPack);
                _mainPack = null;
            }
        }

        public override void OnResponse(MainPack pack)
        {
            _mainPack = pack;
        }
    }
}