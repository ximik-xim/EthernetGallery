using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStatePanelSetData : TestAbsState
{
    [SerializeField] 
    private NewLogicPanel _panel;
    
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
