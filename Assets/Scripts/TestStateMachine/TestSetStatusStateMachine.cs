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


    private bool _init=false;
    private void Awake()
    {
        Init();

    }


    private void Init()
    {
        if (_init == false)
        {
            _listType.GetElementName(CurrentState);
            _init = true;
        }
    }
    public void SetStatuseStateMachine()
    {
        Init();
        _stateMachine.SetState(CurrentState);
    }
}
