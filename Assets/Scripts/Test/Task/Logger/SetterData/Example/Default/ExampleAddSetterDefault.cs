using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAddSetterDefault : MonoBehaviour
{
    [SerializeField] 
    private ExampleSetterDataDefault _setterDefault;
    [SerializeField] 
    private LoggerElementUI _panel;
    
    private void Start()
    {
        _setterDefault.AddElementSetData(_panel);
    }
}
