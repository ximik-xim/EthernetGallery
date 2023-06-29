using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NewControllUITe : NewIntrefaseControlUITE
{
    public event Action<LoaderStatuse> OnUpdateStatuse;
    public event Action<bool> OnOpen;
    public event Action OnClearData;
    public event Action OnClose;

    [SerializeField]
    private NewIntrefaseControlUITE _TuskUI;

    public override void Open(bool clearData = false)
    {
        OnOpen?.Invoke(clearData);
        _TuskUI.Open();
    }

    public override void UpdateData(LoaderStatuse statuse)
    {
        OnUpdateStatuse?.Invoke(statuse);
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
