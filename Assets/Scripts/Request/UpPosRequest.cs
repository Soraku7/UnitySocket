using System;
using SocketGameProtocol;
using UnityEngine;

namespace Request
{
    public class UpPosRequest : BaseRequest
    {
        private MainPack _pack = null;
        
        public override void Awake()
        {
            requestCode = RequestCode.Game;
            actionCode = ActionCode.UpPos;
            
            base.Awake();
        }

        private void Update()
        {
            if (_pack != null)
            {
                GameFace.Instance.UpPos(_pack);
                _pack = null;
            }
        }

        public void SendRequest(Vector2 pos, float characterRot, float gunRot)
        {
            MainPack pack = new MainPack();
            PosPack posPack = new PosPack();
            PlayerPack playerPack = new PlayerPack();
            posPack.PosX = pos.x;
            posPack.PosY = pos.y;
            posPack.RotZ = characterRot;
            posPack.GunRotZ = gunRot;
            playerPack.Playername = GameFace.Instance.userName;

            playerPack.Pospack = posPack;
            
            pack.Playerpack.Add(playerPack);
            
            pack.Requestcode = requestCode;
            pack.Actioncode = actionCode;
            
            base.SendRequestUDP(pack);
        }
        
        public override void OnResponse(MainPack pack)
        {
            _pack = pack;
        }
    }
}