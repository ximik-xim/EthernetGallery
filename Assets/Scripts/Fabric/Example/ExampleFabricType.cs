using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExampleEnumFabricType
{
    F,
    A
}
public class ExampleFabricType : FabricType<ExampleEnumFabricType,TaskElementControllerUI>
{
    private void Awake()
    {
        Init();
    }
    
}
