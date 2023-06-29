using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetDataLogPane : MonoBehaviour
{

    [SerializeField] 
    private NewLogicPanel _panelInfoStatuseUI;
    
    [SerializeField] 
    private TestFabric _fabric;

    private void Awake()
    {
        _fabric.OnCreateObject += CreateElement;
    }

    private void CreateElement(Transform element)
    {
        var obj = element.GetComponent<NewLogicLoggerData>();

        obj.SetPanelUI(_panelInfoStatuseUI);
    }

    private void OnDestroy()
    {
        _fabric.OnCreateObject -= CreateElement;
    }
}
