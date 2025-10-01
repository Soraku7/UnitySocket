using System;
using SocketGameProtocol;
using UI;
using UnityEngine;

namespace Request
{
    public class ChatRequest : BaseRequest
    {
        private string _chatStr;

        private UIRoom _uiRoom;
        
        public override void Awake()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.Chat;

            _uiRoom = transform.GetComponent<UIRoom>();

            base.Awake();
        }

        private void Update()
        {
            if (_chatStr != null)
            {
                _uiRoom.ChatResponse(_chatStr);
                _chatStr = null;
            }
        }

        public void SendRequest(string str)
        {
            MainPack pack = new MainPack();
            pack.Actioncode = actionCode;
            pack.Requestcode = requestCode;

            pack.Str = str;
            base.SendRequest(pack);
        }
        
        public override void OnResponse(MainPack pack)
        {
            _chatStr = pack.Str;
        }
    }
}