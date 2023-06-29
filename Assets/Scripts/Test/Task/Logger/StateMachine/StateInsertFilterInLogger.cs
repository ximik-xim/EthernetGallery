using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//При выборе данного состояниея, установит филтр логов в панель отвеч. за логи
public class StateInsertFilterInLogger : TestAbsState
{
    [SerializeField] 
    private LoggerPanel _panel;
    
    private IFilterLogicDebug _filter;

    private bool _init = false;
    
    private void Awake()
    {
        Init();
        
    }

    public override void SelectState()
    {
        Init();
        _panel.SetStatuseLogPanel(_filter);
    }

    public override void DiselectState()
    {
        
    }

    private void Init()
    {
        if (_init == false)
        {
            _filter = GetComponent<IFilterLogicDebug>();
            _init = true;
        }
    }
}
