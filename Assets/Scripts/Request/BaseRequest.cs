using System;
using SocketGameProtocol;
using UnityEngine;

namespace Request
{
    public class BaseRequest : MonoBehaviour
    {
        protected RequestCode requestCode;
        protected ActionCode actionCode;

        public ActionCode GetActionCode => actionCode;

        public virtual void Awake()
        {
        }
        
        public virtual void Start()
        {
            GameFace.Instance.AddRequest(this);
        }
        
        public virtual void OnDestroy()
        {
            GameFace.Instance.RemoveRequest(actionCode);
        }

        public virtual void OnResponse(MainPack pack)
        {
             
        }

        public virtual void SendRequest(MainPack pack)
        {
            GameFace.Instance.Send(pack);
        }
    }
}