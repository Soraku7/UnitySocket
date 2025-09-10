using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected UIManager UIManager;
    
    
    public UIManager SetUIManager
    {
        set
        {
            UIManager = value;
        }
    }

    public virtual void OnEnter()
    {
        Enter();
    }

    public virtual void OnPause()
    {
        Exit();
    }

    public virtual void OnRecovery()
    {
        Enter();
    }

    public virtual void OnExit()
    {
        Exit();
    }
    
    private void Enter()
    {
        gameObject.SetActive(true);
    }

    private void Exit()
    {
        gameObject.SetActive(false);
    }
}