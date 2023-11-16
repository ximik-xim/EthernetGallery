using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Потом заменю LoggerPanel, LoggerElementUI на обобщение
public class ExampleSetterDataTypeTaskType : SetterDataTypeTList<TElelementType,TGetLIstType, LoggerPanel, LoggerElementUI >
{
    [SerializeField] 
    private TListType _listType;
    private void Awake()
    {
        foreach (var VARIABLE in _listPrefab)
        {
            _listType.GetElementName( VARIABLE._key);  
        }

        foreach (var VARIABLE in _listInstObj) 
        {
            _listType.GetElementName( VARIABLE._key); 
        }

        foreach (var VARIABLE in _listSetDataInstObj) 
        {
            _listType.GetElementName( VARIABLE._key);  
        }

        
        Init();
    }
}
