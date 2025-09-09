using System.Collections.Generic;
using Request;
using SocketGameProtocol;
using UnityEngine;

namespace Manager
{
    public class RequestManager : BaseManager
    {
        private Dictionary<ActionCode, BaseRequest> _requests = new Dictionary<ActionCode, BaseRequest>();
        
        public RequestManager(GameFace face) : base(face)
        {
            
        }
        
        public void AddRequest(BaseRequest request)
        {
            _requests.Add(request.GetActionCode, request);
        }

        public void RemoveRequest(ActionCode actionCode)
        {
            _requests.Remove(actionCode);
        }
        
        public void HandleResponse(MainPack pack)
        {
            if (_requests.TryGetValue(pack.Actioncode, out var request))
            {
                request.OnResponse(pack);
            }
            else
            {
                Debug.LogWarning("没有对应的请求处理类");
            }
        }
    }
}