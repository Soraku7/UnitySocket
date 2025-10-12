using SocketGameProtocol;
using UI;

namespace Request
{
    public class UpCharacterListRequest : BaseRequest
    {
        MainPack _pack = null;

        private UIGame _uiGame;
        public override void Awake()
        {
            requestCode = RequestCode.Game;
            actionCode = ActionCode.UpCharacterList;

            _uiGame = GetComponent<UIGame>();
            
            base.Awake();
        }

        private void Update()
        {
            if (_pack != null)
            {
                _uiGame.UpdateList(_pack);
                GameFace.Instance.RemovePlayer(_pack.Str);
                _pack = null;
            }
        }

        public override void OnResponse(MainPack pack)
        {
            _pack = pack;
        }
    }
}