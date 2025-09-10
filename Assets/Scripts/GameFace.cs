using System;
using System.Collections.Generic;
using Manager;
using Request;
using SocketGameProtocol;
using UnityEngine;

public class GameFace : MonoBehaviour
{
    private ClientManager _clientManager;
    private RequestManager _requestManager;
    private UIManager _uiManager;
    
    public static GameFace Instance;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        
        _clientManager = new ClientManager(this);
        _requestManager = new RequestManager(this);
        _uiManager = new UIManager(this);
        
        
        _clientManager.OnInit();
        _requestManager.OnInit();
        _uiManager.OnInit();
    }

    private void OnDestroy()
    {
        _clientManager.OnDestroy();
        _requestManager.OnDestroy();
        _uiManager.OnDestroy();
    }

    public void Send(MainPack pack)
    {
        _clientManager.Send(pack);
    }

    public void HandleResponse(MainPack pack)
    {
        _requestManager.HandleResponse(pack);
    }
    
    public void AddRequest(BaseRequest request)
    {
        _requestManager.AddRequest(request);
    }
    
    public void RemoveRequest(ActionCode actionCode)
    {
        _requestManager.RemoveRequest(actionCode);
    }
}
