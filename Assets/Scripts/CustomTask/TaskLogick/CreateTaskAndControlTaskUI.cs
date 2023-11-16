using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Отвечает за UI задач, их создание и обнолвение в них данных
public class CreateTaskAndControlTaskUI : MonoBehaviour
{

    [SerializeField] 
    private NewIntrefaseControlUITE _generalTuskPanelUI;

    [SerializeField] 
    private FabricTaskUI fabricTaskUI;

    private Dictionary<int, TaskElementControllerUI> _infoElement = new Dictionary<int, TaskElementControllerUI>();
    private List<TaskElementControllerUI> _buffer = new List<TaskElementControllerUI>();
    [SerializeField]
    private AbstractCalcalcal _infoLoad;

    /// <summary>
    /// Очистит UI и создаст нужное кол-во сообщений 
    /// </summary>
    public void UpdateInform(List<int> listHash)
    {
        CheckCountElement(listHash.Count);

        _infoElement = new Dictionary<int, TaskElementControllerUI>();
        for (int i = 0; i < listHash.Count; i++)
        {
            _infoElement.Add(listHash[i], _buffer[i]);
        }
        
        for (int i = _infoElement.Count; i < _buffer.Count; i++)
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
        
        
        _generalTuskPanelUI.Close();
        
    }
    /// Включит UI 
    public  void Open(bool clearData = false)
    {
        _generalTuskPanelUI.Open();
        
        
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
      
        _infoLoad.OnUpdateElementStatuseDef += UpdateUiStatusElement;
        _infoLoad.OnUpdateGeneralStatuseDef += _generalTuskPanelUI.UpdateData;
    }
    
    private void UpdateUiStatusElement(LoaderStatuse arg1)
    {
  
        _infoElement[arg1.Hash].UpdateData(arg1);
    }

    private void CheckCountElement(int targetCount)
    {
        if (targetCount > _buffer.Count)
        {
            int difference = targetCount - _buffer.Count;
            CreateElement(difference);
        }
    }

    private void CreateElement(int count)
    {
        fabricTaskUI.Create(count,CreateElement);
    }

    private void CreateElement(TaskElementControllerUI element)
    {
        _buffer.Add(element);
    }
    
    private void ClearData()
    {
        foreach (var VARIABLE in _buffer)
        {
            VARIABLE.ClearData();
        }

        _generalTuskPanelUI.ClearData();
    }
    
}
