using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStatePanelSetData : TestAbsState
{
    [SerializeField] 
    private NewLogicPanel _panel;
    
    private IFilterLogicDebug _filter;

    private void Awake()
    {
        _filter = GetComponent<IFilterLogicDebug>();
    }

    public override void SelectState()
    {
        _panel.SetStatuseLogPanel(_filter);
    }

    public override void DiselectState()
    {
        throw new System.NotImplementedException();
    }
}
