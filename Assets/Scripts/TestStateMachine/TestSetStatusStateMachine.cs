using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetStatusStateMachine : MonoBehaviour
{
    [SerializeField] 
    private TestStateMachine _stateMachine;
    [SerializeField] 
    private TListType _listType;
    [SerializeField]
    private TGetLIstType CurrentState;

    private void Start()
    {
        
    }

    public void SetStatuseStateMachine()
    {
        _stateMachine.SetState(CurrentState);
    }
}
