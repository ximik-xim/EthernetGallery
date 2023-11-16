using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAddSetterTList : MonoBehaviour
{
    [SerializeField] 
    private TListType _listType;
    [SerializeField] 
    private TGetLIstType _getLIstType;
    
    [SerializeField] 
    private LoggerElementUI _panel;
    [SerializeField] 
    private ExampleSetterDataTypeTList exampleSetterDataTypeTList;
    private void Start()
    { 
        _listType.GetElementName(_getLIstType);
        
        exampleSetterDataTypeTList.AddElementSetData(_getLIstType.GetElement(),_panel);
    }
}
