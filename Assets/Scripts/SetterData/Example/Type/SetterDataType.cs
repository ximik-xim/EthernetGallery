using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExampleEnumSetterDataType
{
    F,
    A
}
public class SetterDataType : SetterDataType<ExampleEnumSetterDataType,LoggerPanel, LoggerElementUI>
{
    private void Awake()
    {
        Init();
    }
}
