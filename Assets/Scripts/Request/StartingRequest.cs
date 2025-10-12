using System;
using System.Linq;
using SocketGameProtocol;
using UI;
using UnityEngine;

namespace Request
{
    public class StartingRequest : BaseRequest
    {
        private MainPack isStart = null;

        private UIRoom _uiRoom;
        public override void Awake()
        {
            actionCode = ActionCode.Starting;
            
            _uiRoom = transform.GetComponent<UIRoom>();
            
            base.Awake();
        }

        private void Update()
        {
            if (isStart != null)
            {
                Debug.Log("游戏正式开始");
                GameFace.Instance.AddPlayer(isStart);
                _uiRoom.GameStarting(isStart);
                isStart = null;
            }
        }

        public override void OnResponse(MainPack pack)
        {
            Debug.Log("收到游戏开始的回应");
            isStart = pack;
        }
        
    }
}