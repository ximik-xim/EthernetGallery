using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabrTaskEx1 : FabricTList<TElelementType, TGetLIstType, TaskElementControllerUIType<TElelementType,TesStatType<TElelementType>>>
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
