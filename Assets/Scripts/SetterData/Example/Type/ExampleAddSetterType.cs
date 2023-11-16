using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAddSetterType : MonoBehaviour
{
    [SerializeField] 
    private LoggerElementUI _panel;
    [SerializeField] 
    private SetterDataType _SetterType;
    [SerializeField]
    private ExampleEnumSetterDataType _type;

    private void Start()
    {
        _SetterType.AddElementSetData(_type,_panel);
    }
}
