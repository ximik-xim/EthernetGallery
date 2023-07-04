using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSetterDataTypeTList : SetterDataTypeTList<TElelementType,TGetLIstType,LoggerPanel, LoggerElementUI>
{
    [SerializeField] 
    private TListType _listType;
    private void Awake()
    {
        foreach (var VARIABLE in _listPrefab)
        {
            _listType.GetElementName( VARIABLE._key);  
        }

        foreach (var VARIABLE in    _listInstObj)
        {
            _listType.GetElementName( VARIABLE._key); 
        }
        
        foreach (var VARIABLE in             _listSetDataInstObj)
        {
            _listType.GetElementName( VARIABLE._key);  
        }

        
        Init();
    }
}
