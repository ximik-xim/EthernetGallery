using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskElementControllerUIType<Key,Status> : NewIntrefaseControlUITEType<Key,Status> where Status : TesStatType<Key>
{
    public event Action<Key,Status> OnUpdateStatuse;
    public event Action<bool> OnOpen;
    public event Action OnClearData;
    public event Action OnClose;

    [SerializeField]
    private NewIntrefaseControlUITE _TuskUI;

    public override void Open(bool clearData = false)
    {
        OnOpen?.Invoke(clearData);
        _TuskUI.Open(clearData);
    }

    public override void UpdateData(Key key,Status  statuse)
    {
        OnUpdateStatuse?.Invoke(key, statuse);
        _TuskUI.UpdateData(statuse);
    }

    public override void ClearData()
    {
        OnClearData?.Invoke();
        _TuskUI.ClearData();
    }

    public override void Close()
    {OnClose?.Invoke();
        _TuskUI.Close();
    }
}
