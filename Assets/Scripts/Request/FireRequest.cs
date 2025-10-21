using System;
using SocketGameProtocol;
using UnityEngine;

namespace Request
{
    public class FireRequest : BaseRequest
    {
        private MainPack _pack;
        public override void Awake()
        {
            requestCode = RequestCode.Game;
            actionCode = ActionCode.Fire;
            
            base.Awake();
        }
        
        private void Update()
        {
            if (_pack != null)
            {
                GameFace.Instance.SpawnBullet(_pack);
                _pack = null;
            }
        }

        public void SendRequest(Vector2 pos , float rat , Vector2 mousePos)
        {
            MainPack pack = new MainPack();
            BulletPack bulletPack = new BulletPack();
            bulletPack.PosX = pos.x;
            bulletPack.PosY = pos.y;
            bulletPack.MousePosX = mousePos.x;
            bulletPack.MousePosY = mousePos.y;
            bulletPack.RotZ = rat;

            pack.Bulletpack = bulletPack;
            pack.Actioncode = actionCode;
            pack.Requestcode = requestCode;
            base.SendRequestUDP(pack);
        }

        public override void OnResponse(MainPack pack)
        {
            _pack = pack;
        }
    }
}