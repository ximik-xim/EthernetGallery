using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleStateMachineTList : StateMachineTList<TElelementType,TGetLIstType,AbstrackState>
{
    [SerializeField] 
    private TListType _listType;
    private void Awake()
    {
        _listType.GetElementName(_startStateKey);

        foreach (var VARIABLE in _elementState)
        {
            _listType.GetElementName(VARIABLE.Key);
        }
        
        Init();
    }
}
