using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewLogicPanel : MonoBehaviour
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
            ClearText();
        }
        
        Debug.Log("Count element = "+_statuses.Count);
        Debug.Log(_filterLogicDebug);
        foreach (var VARIABLE in _statuses)
        {
            string text = _filterLogicDebug.DataSuitable(VARIABLE);
          
            if ( text!=String.Empty)
            {
                _text.text += "\n" + text;

            }
        }
       
    }
    
    public void ClearText()
    {
        _text.text = "";
    }

    public void ClosPanel()
    {
        _panel.gameObject.SetActive(false);
        _isOpen = false;
        
        ClosePanel?.Invoke();
    }

    /// <summary>
    /// Открывает панель с логоми статусов
    /// </summary>
    public void OpenPanel(IReadOnlyList<LoaderStatuse> list, bool clearLastData)
    {
        _panel.gameObject.SetActive(true);
        _isOpen = true;

        _statuses = list;
        LoadData(clearLastData);
    }

    public void AddInfo(LoaderStatuse data)
    {
        string text = _filterLogicDebug.DataSuitable(data);
          
        if ( text!=String.Empty)
        {
            _text.text += "\n" + text;

        }
    }
    
}
