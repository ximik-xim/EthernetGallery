using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskElementControllerUIZero : NewIntrefaseControlUITE
{
    public event Action<LoaderStatuse> OnUpdateStatuse;
    public event Action<bool> OnOpen;
    public event Action OnClearData;
    public event Action OnClose;

    protected void InvokeOnUpdateStatuse(LoaderStatuse loaderStatuse)
    {
        OnUpdateStatuse?.Invoke(loaderStatuse);
    }
    
    protected void InvokeOnOpen(bool clearData = false)
    {
        OnOpen?.Invoke(clearData);
    }
    
    protected void InvokeOnClearData()
    {
        OnClearData?.Invoke();
    }
    
    protected void InvokeOnClose()
    {
        OnClose?.Invoke();
    }
    
    public override void Open(bool clearData = false)
    {
        InvokeOnOpen(clearData);
    }

    public override void UpdateData(LoaderStatuse statuse)
    {
        InvokeOnUpdateStatuse(statuse);
    }

    public override void ClearData()
    {
        InvokeOnClearData();

    }

    public override void Close()
    {
        InvokeOnClose();
    }
}
