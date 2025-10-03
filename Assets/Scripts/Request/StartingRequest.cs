using System;
using SocketGameProtocol;
using UnityEngine;

namespace Request
{
    public class StartingRequest : BaseRequest
    {
        private bool isStart = false;
        public override void Awake()
        {
            actionCode = ActionCode.Starting;
            base.Awake();
        }

        private void Update()
        {
            if (isStart)
            {
                Debug.Log("游戏正式开始");
                isStart = false;
            }
        }

        public override void OnResponse(MainPack pack)
        {
            isStart = true;
        }
        
    }
}