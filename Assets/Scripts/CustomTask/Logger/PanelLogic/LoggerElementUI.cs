using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TaskElementControllerUIZero))]
public class LoggerElementUI : NewIntrefaseControlUITE,ISetterData<LoggerPanel>
{

    private TaskElementControllerUIZero _data;
    
    public IReadOnlyList<LoaderStatuse> Statuses => _listStatuse; 
    
    private LoggerPanel _panelInfoStatuseUI;
    private List<LoaderStatuse> _listStatuse = new List<LoaderStatuse>();
    private bool _select;

    private void Awake()
    {
        _data = GetComponent<TaskElementControllerUIZero>();
        _data.OnUpdateStatuse += UpdateData;
        _data.OnClearData += ClearListStatuse;
        _data.OnClose += Close;
    }

    public override void Open(bool clearData = false)
    {
        if (clearData==true)
        {
            ClearData();
        }
        
        _select = true;
        _panelInfoStatuseUI.SetData(Statuses);
        _panelInfoStatuseUI.Open(clearData);
        _panelInfoStatuseUI.ClosePanel += CloseLogPanel;
    }
    
    public override void UpdateData(LoaderStatuse statuse)
    {
        _listStatuse.Add(statuse);
        if (_select == true)
        {
            if (_panelInfoStatuseUI.IsOpen)
            {
                _panelInfoStatuseUI.UpdateData(statuse);
            }    
        }
    }

    public override void ClearData()
    {
        
        _panelInfoStatuseUI.ClearData();
    }
    
//по сути эта очистка данных должна происходить только когда происходит загрузка новых Tusk
    private void ClearListStatuse()
    {
        _listStatuse = new List<LoaderStatuse>();
        _panelInfoStatuseUI.ClearData();
    }

    public override void Close()
    {
        _panelInfoStatuseUI.Close();
    }

    private void CloseLogPanel()
    {
        _select = false;
        _panelInfoStatuseUI.ClosePanel -= CloseLogPanel;
    }

    public void SetData(LoggerPanel data)
    {
        _panelInfoStatuseUI = data;
    }
}
