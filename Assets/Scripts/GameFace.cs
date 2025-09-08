using System;
using System.Collections.Generic;
using Manager;
using SocketGameProtocol;
using UnityEngine;

public class GameFace : MonoBehaviour
{
    private ClientManager _clientManager;

    private void Start()
    {
        _clientManager = new ClientManager(this);
        _clientManager.OnInit();
    }

    private void OnDestroy()
    {
        _clientManager.OnDestroy();
    }

    public void Send(MainPack pack)
    {
        _clientManager.Send(pack);
    }

    private void HandleResponse(MainPack pack)
    {
        
    }
}
