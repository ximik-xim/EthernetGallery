using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum adflajf
{
    F,
    A
}


public class ExTask2 : MonoBehaviour, ItesTaskType<adflajf>
{
    private void Start()
    {
        TesterLoaderF.statikLoad.AddTaskType(typeof(adflajf),_HashTypeKey,this);
    }

    [SerializeField]
    private adflajf keyValue;

    public int LoaderHash { get; }
    public string LoaderName { get; }
    public event Action<LoaderStatuse> OnStatus;
    public void StartLoad()
    {
        throw new NotImplementedException();
    }

    public adflajf GetKey()
    {
        return keyValue;
    }

    public int _HashTypeKey { get=>typeof(adflajf).GetHashCode(); }
}
