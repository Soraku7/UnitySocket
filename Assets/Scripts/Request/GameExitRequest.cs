using System;
using SocketGameProtocol;

namespace Request
{
    public class GameExitRequest : BaseRequest
    {
        private MainPack _pack;
        
        public override void Awake()
        {
            requestCode = RequestCode.Game;
            actionCode = ActionCode.ExitGame;
            base.Awake();
        }

        private void Update()
        {
            if (_pack != null)
            {
                GameFace.Instance.GameExit();
                _pack = null;
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
            _pack = pack;
        }
    }
}