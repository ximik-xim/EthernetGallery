using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///Отвечет за отправку логов статуса в зависимости от выбранного типа логов 
/// </summary>
public class LoaderPanelInfoStatuseUI : MonoBehaviour
{
    public event Action ClosePanel;
    
    public bool IsOpen { get=>_isOpen; }
    
    [SerializeField]
    private List<TypeStatusAndElementStatus> _listElement;
    [SerializeField]
    private GameObject _panel;
    private Dictionary<LoadStatusElement.TypeElement, LoadStatusElement> _elements = new Dictionary<LoadStatusElement.TypeElement, LoadStatusElement>();
    private LoadStatusElement.TypeElement currentType = LoadStatusElement.TypeElement.None;
    
    private bool _isOpen = false;
    private List<LoaderStatuse> _list;
    
    /// <summary>
    /// Открывает панель с логоми статусов
    /// </summary>
    public void SetLogStatus(List<LoaderStatuse> list)
    {
        _panel.gameObject.SetActive(true);
        _isOpen = true;
        
        _list = list;
        SetText(list);
    }
    
    /// <summary>
    /// По типу логов выбранных логов, добавляет текст лога статуса к уже загруженным логам статусов
    /// </summary>
    public void AddData(LoaderStatuse data)
    {
        _elements[currentType].AddData(data);
    }

    /// <summary>
    /// Очищает список логов
    /// </summary>
    public void ClearData()
    {
        _elements[currentType].ClearText();
    }
    
    //выключает панель с логами статусов
    public void ClosPanel()
    {
        _panel.gameObject.SetActive(false);
        _isOpen = false;
        ClosePanel?.Invoke();
    }
    
    private void Start()
    {
        foreach (var VARIABLE in _listElement)
        {
            _elements.Add(VARIABLE.TypeElement,VARIABLE.Element);
            VARIABLE.Element.OnSubSelect(UpdateType);
        }
        
        if (currentType == LoadStatusElement.TypeElement.None)
        {
            _elements[LoadStatusElement.TypeElement.GeneralStatus].OnSelect();
        }
    }
    
    private void UpdateType(LoadStatusElement.TypeElement typeElement)
    {
        currentType = typeElement;

        if (_list != null)
        {
            SetText(_list);    
        }
    }
    
    private void SetText(List<LoaderStatuse> list)
    {
        ClearData();
        
        switch (currentType)
        {
            case LoadStatusElement.TypeElement.Error:
            {
                foreach (var VARIABLE in list)
                {
                    if (VARIABLE.Statuse == LoaderStatuse.StatusLoad.Error)
                    {
                        if (VARIABLE.ErrorInfo.Type == LoaderStatuse.Error.TypeError.Error)
                        {
                            _elements[currentType].AddData(VARIABLE);
                        }
                    }
                }
            } break;
            
            case LoadStatusElement.TypeElement.Load:
            {
                foreach (var VARIABLE in list)
                {
                    if (VARIABLE.Statuse == LoaderStatuse.StatusLoad.Load)
                    {
                        _elements[currentType].AddData(VARIABLE);
                    }
                }
            } break;
            
            case LoadStatusElement.TypeElement.FatalError:
            {
                foreach (var VARIABLE in list)
                {
                    if (VARIABLE.Statuse == LoaderStatuse.StatusLoad.Error)
                    {
                        if (VARIABLE.ErrorInfo.Type == LoaderStatuse.Error.TypeError.FatalError)
                        {
                            _elements[currentType].AddData(VARIABLE);
                        }
                    }
                }
            } break;
            
            case LoadStatusElement.TypeElement.GeneralStatus:
            {
                _elements[currentType].SetData(list);
            } break;
        }
    }
}

[System.Serializable]
public class TypeStatusAndElementStatus
{

    public LoadStatusElement.TypeElement TypeElement
    {
        get => _typeElement;
    }

    [SerializeField] 
    private LoadStatusElement.TypeElement _typeElement;

    public LoadStatusElement Element
    {
        get => _element;
    }

    [SerializeField] 
    private LoadStatusElement _element;
}
