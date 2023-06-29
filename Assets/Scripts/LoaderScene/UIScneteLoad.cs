using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Отвечает за обновления UI у загрузчика данных
//нужно что бы всегда был включен
public class UIScneteLoad : MonoBehaviour
{

    [SerializeField] 
    private NewIntrefaseControlUITE _test;

    [SerializeField] 
    private TestFabric _fabric;

    private Dictionary<int, NewControllUITe> _infoElement = new Dictionary<int, NewControllUITe>();
    private List<NewControllUITe> _buffer = new List<NewControllUITe>();
    private LoaderPacketInfo _infoLoad;

    /// <summary>
    /// Очистит UI и создаст нужное кол-во сообщений 
    /// </summary>
    public void UpdateInform(List<int> listHash)
    {
        CheckCountElement(listHash.Count);

        _infoElement = new Dictionary<int, NewControllUITe>();
        for (int i = 0; i < _infoLoad.CountElement; i++)
        {
            _infoElement.Add(listHash[i], _buffer[i]);
        }
        
        for (int i = _infoElement.Count; i < _infoLoad.CountElement; i++)
        {
         
            _buffer[i].Close();
        }
    }
    
    /// Выключит UI 
    public  void Close()
    {

        foreach (var VARIABLE in _buffer)
        {
            VARIABLE.Close();
        }
        
        
        _test.Close();
        
    }
    /// Включит UI 
    public  void Open(bool clearData = false)
    {
        _test.Open();
        
        
        foreach (var VARIABLE in _infoElement.Values)
        {
            VARIABLE.Open(clearData);
        }
        
        if (clearData == true) 
        {
            ClearData();
        }
    }
    
    private void Start()
    {
        _fabric.OnCreateObject += CreateElement;
        
        _infoLoad = LoaderPacketInfo.PacketInfo;
        _infoLoad.OnUpdateElementStatuse += UpdateUiStatusElement;
        _infoLoad.OnUpdateGeneralStatuse += _test.UpdateData;
    }
    
    private void UpdateUiStatusElement(LoaderStatuse arg1)
    {
        _infoElement[arg1.Hash].UpdateData(arg1);
    }

    private void CheckCountElement(int targetCount)
    {
        if (targetCount > _infoElement.Count)
        {
            int difference = targetCount - _infoElement.Count;
            CreateElement(difference);
        }
    }

    private void CreateElement(int count)
    {
        _fabric.Create(count);
    }

    private void CreateElement(Transform element)
    {
        var obj = element.GetComponent<NewControllUITe>();
        _buffer.Add(obj);
    }
    
    private void ClearData()
    {
        foreach (var VARIABLE in _buffer)
        {
            VARIABLE.ClearData();
        }

        _test.ClearData();
    }
    
}
