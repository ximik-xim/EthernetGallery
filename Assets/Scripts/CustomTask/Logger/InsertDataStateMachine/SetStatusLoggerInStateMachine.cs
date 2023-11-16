using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Указываает состоние у машины состояния в логгере
public class SetStatusLoggerInStateMachine : MonoBehaviour
{
    [SerializeField] 
    private LoggerStateMachineTList stateMachineTList;
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
        stateMachineTList.SetState(CurrentState.GetKey());
    }
}
