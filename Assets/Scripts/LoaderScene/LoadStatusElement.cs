using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отвечает за вывод логов статуса на панель
/// И оповещение при выборе типов логов
/// </summary>
[RequireComponent(typeof(Toggle))]
public class LoadStatusElement : MonoBehaviour
{
    public enum TypeElement
    {
        None,
        Error,
        FatalError,
        Load,
        GeneralStatus
        
    }
    
    [SerializeField] 
    private Text _text;
    [SerializeField]
    private TypeElement _typeElement;
    
    private bool _init = false;
    private Toggle _toggle;
    
    /// <summary>
    /// очистит текст логов статусов
    /// </summary>
    public void ClearText()
    {
        _text.text = "";
    }

    /// <summary>
    /// Метод указывающий на выбор типа логов
    /// </summary>
    public void OnSelect()
    {
        _toggle.isOn = true;
    }
    
    /// <summary>
    /// Добавляет текст лога статуса к уже загруженным логам статусов 
    /// </summary>
    public void AddData(LoaderStatuse data)
    {
        SetColorText(data);
    }
    
    /// <summary>
    /// Добавляет текст логов статуса к уже загруженным логам статусов, если таковые были 
    /// </summary>
    public void SetData(IReadOnlyList<LoaderStatuse> listData)
    {
        foreach (var VARIABLE in listData)
        {
            SetColorText(VARIABLE);
        }
    }
    
    /// <summary>
    ///Подписка на событие, срабатывающее при выборе типа логов
    /// </summary>
    public void OnSubSelect( Action<TypeElement> action)
    {
        Init();
        _toggle.onValueChanged.AddListener(delegate { action?.Invoke(_typeElement); });
    }
    
    /// <summary>
    /// Отписка на событие, срабатывающее при выборе типа логов
    /// </summary>
    public void OnDesSubSelect( Action<TypeElement> action)
    {
        _toggle.onValueChanged.RemoveListener(delegate { action.Invoke(_typeElement); });
    }
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_init == false)
        {
            _toggle = GetComponent<Toggle>();
        }
    }


    private void SetColorText(LoaderStatuse data)
    {
        if (data.Statuse == LoaderStatuse.StatusLoad.Start)
        {
            _text.text += "\n" + "<color=blue>" + data.StartInfo.Text + "</color>";
        }
        
        if (data.Statuse == LoaderStatuse.StatusLoad.Error)
        {
            if (data.ErrorInfo.Type == LoaderStatuse.Error.TypeError.Error)
            {
                _text.text += "\n" + "<color=yellow>" + data.ErrorInfo.Text + "</color>";
            }
            else if (data.ErrorInfo.Type == LoaderStatuse.Error.TypeError.FatalError)
            {
                _text.text += "\n" + "<color=red>" + data.ErrorInfo.Text + "</color>";
            }
        }

        if (data.Statuse == LoaderStatuse.StatusLoad.Load)
        {
            _text.text += "\n" + "<color=white>" + data.LoadInfo.Text + "</color>";
        }
        
        if (data.Statuse == LoaderStatuse.StatusLoad.Complite)
        {
            _text.text += "\n" + "<color=green>" + data.CompliteInfo.Text + "</color>";
        }
    }
    
   
}
