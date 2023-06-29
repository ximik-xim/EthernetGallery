using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Отвечает за установку экземпляра панели с логами в новосозданую задачу
public class SetDataLoggerPanelNewTusk : MonoBehaviour
{

    [SerializeField] 
    private LoggerPanel _panelInfoStatuseUI;
    
    [SerializeField] 
    private PrototypeFabric _fabric;

    private void Awake()
    {
        _fabric.OnCreateObject += CreateElement;
    }

    private void CreateElement(Transform element)
    {
        var obj = element.GetComponent<LoggerElementUI>();

        obj.SetPanelUI(_panelInfoStatuseUI);
    }

    private void OnDestroy()
    {
        _fabric.OnCreateObject -= CreateElement;
    }
}
