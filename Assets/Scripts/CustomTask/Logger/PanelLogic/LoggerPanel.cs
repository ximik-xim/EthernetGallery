using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Отвчечает за отображение логов 
public class LoggerPanel : NewIntrefaseControlUITE
{
    
    public event Action ClosePanel;
    [SerializeField]
    private Text _text;

    private IFilterLogicDebug _filterLogicDebug;
    private IReadOnlyList<LoaderStatuse> _statuses;
[SerializeField]
    private Transform _panel;
    private bool _isOpen = false;

    public bool IsOpen => _isOpen;

    private void Awake()
    {
        _panel.gameObject.SetActive(false);
        _isOpen = false;
    }

    //Устанавливает филтр для проверки статусов и загружает статусы
    //А зничит переключение между типами логов, а значит очистка и загрузка логов заново
    //Обязательно должен быть установлен хоть какой то фильтр.(первый фильтр должен быть устновлен при выключенной панели до ее открытия) 
    public void SetStatuseLogPanel(IFilterLogicDebug filter)
    {
        
        Debug.Log("SET FILTER = "+ filter);
        _filterLogicDebug = filter;

        if (_isOpen == true) 
        {
            LoadData(true);
        }
            
            
    }
    
    private void LoadData(bool clearLastText = false)
    {
        if (clearLastText == true)
        {
            ClearData();
        }
        
        foreach (var VARIABLE in _statuses)
        {
            string text = _filterLogicDebug.DataSuitable(VARIABLE);
          
            if ( text!=String.Empty)
            {
                _text.text += "\n" + text;

            }
        }
       
    }



    public void SetData(IReadOnlyList<LoaderStatuse> list)
    {
        Debug.Log("SET LIST = "+ list.Count);
        _statuses = list;
    }
    
    /// <summary>
    /// Открывает панель с логоми статусов
    /// </summary>

    public override void Open(bool clearData = false)
    {
        Debug.Log("OPEN LOGGER PANEL");
        _panel.gameObject.SetActive(true);
        _isOpen = true;

        LoadData(clearData);
    }

    public override void UpdateData(LoaderStatuse statuse)
    {
        string text = _filterLogicDebug.DataSuitable(statuse);
          
        if ( text!=String.Empty)
        {
            _text.text += "\n" + text;

        }
    }

    public override void ClearData()
    {
        _text.text = "";
    }

    public override void Close()
    {
        _panel.gameObject.SetActive(false);
        _isOpen = false;
        
        ClosePanel?.Invoke();
    }
}
