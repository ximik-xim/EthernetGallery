using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FabricInterActDef : MonoBehaviour
{
    public  event Action<Transform> OnCreateObject;

    protected void InvokeOnCreateObject(Transform transform)
    {
        OnCreateObject?.Invoke(transform);
    }
}
