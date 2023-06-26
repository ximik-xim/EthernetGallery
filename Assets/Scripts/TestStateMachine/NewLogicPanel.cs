using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewLogicPanel : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    private IFilterLogicDebug _filterLogicDebug;
    private IReadOnlyList<LoaderStatuse> _statuses;
[SerializeField]
    private Transform _panel;
    private bool _isOpen = false;

    private void Awake()
    {
        _panel.gameObject.SetActive(false);
        _isOpen = false;
    }

    //Устанавливает филтр для проверки статусов и загружает статусы
    public void SetStatuseLogPanel(IFilterLogicDebug filter)
    {
        _filterLogicDebug = filter;
      
        LoadData();
    }
    
    //Или при открытии предедовать список или в отдельном методе....... хммммм
    //А может вообще экземпляр класса Tusk
    //public void


    private void LoadData(bool clearLastText = false)
    {
        if (clearLastText == true)
        {
            ClearText();
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

    private void SetColorTextTypeStatus()
    {
        
    }
    private void ClearText()
    {
        
    }


    /// <summary>
    /// Открывает панель с логоми статусов
    /// </summary>
    ///Если все же собираюсь перевести на event добавление новых элементов, то нужно подумать а где, как и кто будет подписывать события на
    /// методы(просто можно сюда экземпляр класса передовать или наоборо экземпляром класса подписывать методы панели 
    /// 
    public void OpenPanel(IReadOnlyList<LoaderStatuse> list, bool clearLastData)
    {
        _panel.gameObject.SetActive(true);
        _isOpen = true;

        _statuses = list;

        if (clearLastData == true)
        {
            ClearText();
        }
    }
    
}
