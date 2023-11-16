using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExampleEnumStateMachineType
{
    F,
    A
}
public class ExampleStateMachineType : StateMachineType<ExampleEnumStateMachineType,AbstrackState>
{
    private void Awake()
    {
        Init();
    }
}
