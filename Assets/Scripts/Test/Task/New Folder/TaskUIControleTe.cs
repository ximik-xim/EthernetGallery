using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //подумать над ошибкой
public class TaskUIControleTe<GetType, ListType,Status> : MonoBehaviour where Status : TesStatType<GetType> where ListType : IGetKey<GetType>
{
    [SerializeField] 
    private NewIntrefaseControlUITEType<GetType,Status> _generalTuskPanelUI;

    //Вслуч Enum тут будет FabricType<GetType, TaskElementControllerUIType<GetType, Status>>
    [SerializeField] 
    private FabricTList<GetType, ListType, TaskElementControllerUIType<GetType, Status>> fabricTaskUI;

    //private List<TaskElementControllerUIType<GetType,Status>> _buffer = new List<TaskElementControllerUIType<GetType,Status>>();
    private Dictionary<GetType, List<TaskElementControllerUIType<GetType, Status>>> _buffer = new Dictionary<GetType, List<TaskElementControllerUIType<GetType, Status>>>();

    [SerializeField] 
    private AbstrCal<GetType, Status> _bank;

    private Dictionary<GetType, Dictionary<int, TaskElementControllerUIType<GetType,Status>>> _infoElement;
    
   
    
    
    public void UpdateInform(Dictionary<GetType,List<int>> listHash)
    {

        foreach (var VARIABLE in listHash.Keys)
        {
            CheckCountElement(VARIABLE,listHash[VARIABLE].Count);
        }
     

        
        
        _infoElement = new Dictionary<GetType, Dictionary<int, TaskElementControllerUIType<GetType,Status>>>();

        foreach (var VARIABLE in listHash.Keys)
        {
            _infoElement.Add(VARIABLE,new Dictionary<int, TaskElementControllerUIType<GetType, Status>>());
            int i = 0;
            foreach (var VARIABLE2 in listHash[VARIABLE])
            {
                i++;
                _infoElement[VARIABLE].Add(VARIABLE2,_buffer[VARIABLE][i]);
            }
        }

        foreach (var VARIABLE in _infoElement.Keys)
        {

            for (int i = _infoElement[VARIABLE].Count; i < _buffer[VARIABLE].Count; i++)
            {
                _buffer[VARIABLE][i].Close();
            }
        }
    }
    
    private void UpdateUiStatusElement(GetType type, Status arg1)
    {
        _infoElement[type][arg1.Hash].UpdateData(arg1.GetKey(),arg1);
    }
    
    
    
    
    /// Выключит UI 
    public  void Close()
    {

        foreach (var VARIABLE in _buffer.Keys)
        {
            foreach (var VARIABLE2 in _buffer[VARIABLE] )
            {
                VARIABLE2.Close();    
            }
            
        }
        _generalTuskPanelUI.Close();
        
    }


    ///Тааааак а вот и проблемы, допустим я хочу сдлеать CloseType - Что бы закрывать TaskUI опр. типа
    /// Добавить этот функционал в Bank

    public void CloseType(GetType type)
    {
        foreach (var VARIABLE in _buffer[type])
        {
            VARIABLE.Close();
        }
    }
    
    
    /// Включит UI 
    public  void Open(bool clearData = false)
    {
        _generalTuskPanelUI.Open();
        
        
        foreach (var VARIABLE in _infoElement.Keys)
        {
            foreach (var VARIABLE2 in _infoElement[VARIABLE].Values)
            {
                VARIABLE2.Open(clearData);
            }
      
        }
        
        if (clearData == true) 
        {
            ClearData();
        }
    }
    
    //Подумать над этим
    private void Start()
    {
        
        //_infoLoad = LoaderTask.Task;
        //_infoLoad.OnUpdateElementStatuse += UpdateUiStatusElement;
        //_infoLoad.OnUpdateGeneralStatuse += _generalTuskPanelUI.UpdateData;

        _bank.OnUpdateElementStatuseType += UpdateUiStatusElement;
        _bank.OnUpdateGeneralStatuseType += _generalTuskPanelUI.UpdateData;
    }
    

    private void CheckCountElement(GetType type, int targetCount)
    {
        if (targetCount > _infoElement.Count)
        {
            int difference = targetCount - _infoElement.Count;
            CreateElement(type, difference);
        }
    }

    private void CreateElement(GetType type, int count)
    {
         fabricTaskUI.Create(type,count,CreateElement2);
    }

    private void CreateElement2(GetType type,TaskElementControllerUIType<GetType, Status> prefab)
    {
         _buffer[type].Add(prefab);
    }
    
    private void ClearData()
    {
        foreach (var VARIABLE in _buffer.Keys)
        {
            ClearDatType(VARIABLE);
        }

        _generalTuskPanelUI.ClearData();
    }
    
    private void ClearDatType(GetType type)
    {
        foreach (var VARIABLE in _buffer[type])
        {
            VARIABLE.ClearData();
        }

        
    }
}
