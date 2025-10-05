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
    private PlayerManager _playerManager;
    
    public static GameFace Instance;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        
        _uiManager = new UIManager(this);
        _clientManager = new ClientManager(this);
        _requestManager = new RequestManager(this);
        _playerManager = new PlayerManager(this);
        
        
        _uiManager.OnInit();
        _clientManager.OnInit();
        _requestManager.OnInit();
        
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
        Debug.Log("Adding request" + request.GetActionCode);
        _requestManager.AddRequest(request);
    }
    
    public void RemoveRequest(ActionCode actionCode)
    {
        _requestManager.RemoveRequest(actionCode);
    }

    public void ShowMessage(string str , bool sync = false)
    {
        _uiManager.ShowMessage(str , sync);
    }

    public void SetSelfId(string id)
    {
        _playerManager.CurPlayerId = id;
    }

    public void AddPlayer(List<PlayerPack> playerPacks)
    {
        _playerManager.AddPlayer(playerPacks);
    }

    public void RemovePlayer(string id)
    {
        _playerManager.RemovePlayer(id);
    }
}
