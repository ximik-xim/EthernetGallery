using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleFabricTList : FabricTList<TElelementType,TaskElementControllerUI,TGetLIstType>
{
    [SerializeField] 
    private TListType _listType;
    private void Awake()
    {
        foreach (var VARIABLE in _list)
        {
            _listType.GetElementName(VARIABLE.key);
        }
        
        Init();
    }
}
