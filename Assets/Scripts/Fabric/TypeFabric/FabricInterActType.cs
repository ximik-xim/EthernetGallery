using System;
using System.Collections.Generic;
using UnityEngine;

public class FabricInterActType<Key, Transform> : FabricInterActDef
{
    public event Action<Key, Transform> OnCreateObjectType;
    
    protected void InvokeOnCreateObjectType(Key key, Transform transform)
    {
        OnCreateObjectType?.Invoke(key, transform);
    }
}
