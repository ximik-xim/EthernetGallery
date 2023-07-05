using System;
using System.Collections.Generic;
using UnityEngine;
//Отвечает за выбр текущего типа логов  
public class LoggerStateMachineTList : StateMachineTList<TElelementType,TGetLIstType,AbstrackState>
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
    