using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoaderDataScene 
{

    public int LoaderHash{ get; }
    public string LoaderName { get;  }

    public event Action<LoaderStatuse> OnStatus;

    public void StartLoad();


}
