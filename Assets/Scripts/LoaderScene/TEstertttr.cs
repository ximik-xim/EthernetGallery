using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UIScneteLoad))]
public class TEstertttr : MonoBehaviour,testISetData<LoaderPanelInfoStatuseUI>
{
    private UIScneteLoad _scneteLoad;
    private List<LoaderStatuse> _list = new List<LoaderStatuse>();
    private LoaderPanelInfoStatuseUI _panelInfoStatuseUI;

    private bool _currentOpenPalnel = false;
    
    public void SetData(LoaderPanelInfoStatuseUI data)
    {
        _panelInfoStatuseUI = data;
    }
    
    public void Awake()
    {
        _scneteLoad = GetComponent<UIScneteLoad>();
        
        _scneteLoad.testStat += MassageStatus;
    }

    private void MassageStatus(LoaderStatuse obj)
    {
        _list.Add(obj);
        
        if (_currentOpenPalnel == true)
        {
            //можно конечно снова тупо добавлять данные вручную, но может лучше сделать event на добавление и очиску элементов   
          //  _panelInfoStatuseUI.AddData();
        }   
    }

    public void OpenPanel()
    {
        if (_panelInfoStatuseUI.IsOpen == false)
        {
            _currentOpenPalnel = true;
            //_panelInfoStatuseUI.OpenPanel();
        }
    }

    public void ClosePanel()
    {
        if (_currentOpenPalnel == true)
        {
            //_panelInfoStatuseUI.ClosPanel();
        }   
    }

}
