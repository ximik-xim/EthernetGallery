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
    
    private Dictionary<GetType, List<TaskElementControllerUIType<GetType, Status>>> _buffer = new Dictionary<GetType, List<TaskElementControllerUIType<GetType, Status>>>();

    private Dictionary<GetType, Dictionary<int, List<TaskElementControllerUIType<GetType, Status>>>> _bufferExternalTask = new Dictionary<GetType, Dictionary<int, List<TaskElementControllerUIType<GetType, Status>>>>();
    
    [SerializeField] 
    private AbstrCal<GetType, Status> _bank;

    //GetType - тип ключа
    //int - хэш код Task
    //List - список UI Task по хэш ключу
    private Dictionary<GetType, Dictionary<int,List<TaskElementControllerUIType<GetType,Status>>> > _infoElement;
    //private Dictionary<GetType, Dictionary<int,Dictionary <Transform, TaskElementControllerUIType<GetType,Status>>>> _infoElement;

    
    public void UpdateInform(Dictionary<GetType, List<int>> listHash, List<ParentDataSet> listParentDataSet = null, List<ParentDataSetType<GetType>> listParentDataSetType = null)
    {
        _bufferExternalTask = new Dictionary<GetType, Dictionary<int, List<TaskElementControllerUIType<GetType, Status>>>>();
        if ((listParentDataSet != null || listParentDataSetType != null) && (listParentDataSet.Count > 0 || listParentDataSetType.Count > 0)) 
        {
            foreach (var VARIABLE in listHash.Keys)
            {
                _bufferExternalTask.Add(VARIABLE, new Dictionary<int, List<TaskElementControllerUIType<GetType, Status>>>());
                
                foreach (var VARIABLE2 in listHash[VARIABLE])
                {
                    _bufferExternalTask[VARIABLE].Add(VARIABLE2, new List<TaskElementControllerUIType<GetType, Status>>());
                    
                    foreach (var VARIABLE3 in listParentDataSet)
                    {
                        Debug.Log("Создание префаба у родителя");
                        var obj = fabricTaskUI.Create(VARIABLE, VARIABLE3.Parent);
                        _bufferExternalTask[VARIABLE][VARIABLE2].Add(obj);
                    }
                  
                    
                }
            }


       
            
            foreach (var VARIABLE in listParentDataSetType)
            {

                foreach (var VARIABLE2 in listHash[VARIABLE.GetKey()])
                {
                    var obj = fabricTaskUI.Create(VARIABLE.GetKey(), VARIABLE.Parent);
                    _bufferExternalTask[VARIABLE.GetKey()][VARIABLE2].Add(obj);
                }

            }
            
        }
        
        
        /////////////////////////////
        
        foreach (var VARIABLE in listHash.Keys)
        {
            CheckCountElement(VARIABLE,listHash[VARIABLE].Count);
        }


        _infoElement = new Dictionary<GetType, Dictionary<int, List<TaskElementControllerUIType<GetType, Status>>>>();

        foreach (var VARIABLE in listHash.Keys)
        {
            _infoElement.Add(VARIABLE, new Dictionary<int, List<TaskElementControllerUIType<GetType, Status>>>());
            int i = 0;
            foreach (var VARIABLE2 in listHash[VARIABLE])
            {

                _infoElement[VARIABLE].Add(VARIABLE2,new List<TaskElementControllerUIType<GetType, Status>>());
                
                
                //хммммм вот тут вопрос а что делать с буффером, тип тут логика идет для заполнения _infoElement что бы потом обращаться и заполнять его
                //если оставить как есть будет добавлять лишь 1 элемент, но затем прийдеться добавлять в буффер еще элементы для каждого Parent и снова дозаполнять
                // и в этом случае в буффере будут обьекты как для Transfor которые были указаны в фабрике(те Transform которые заплонированы) и те Transform которые были указаны для внеш. Task
                
                //- Можно сделать отдеьно буффер для внешний Transform - Dictionary<GetType, Dictionary<Transform <TaskElementControllerUIType<GetType, Status>>>> _buffer
                //- Но тогда 
                
                //- Можно сделать Dictionary<GetType, Dictionary<int,Dictionary <Transform, TaskElementControllerUIType<GetType,Status>>>> _infoElement 
                //- Но тогда откуда брать Transform и какая будет в нем логика
                
                _infoElement[VARIABLE][VARIABLE2].Add(_buffer[VARIABLE][i]);

                
                //Избавиться ппотом от If
                if ((listParentDataSet != null || listParentDataSetType != null) && (listParentDataSet.Count > 0 || listParentDataSetType.Count > 0)) 
                {
         
  
               
                    foreach (var VARIABLE33 in _bufferExternalTask[VARIABLE][VARIABLE2])
                    {
                        _infoElement[VARIABLE][VARIABLE2].Add(VARIABLE33);
                    }
                }
                
                
                
                
                i++;
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

        foreach (var VARIABLE in _infoElement[type][arg1.Hash])
        {
            VARIABLE.UpdateStatuseElement(arg1.GetKey(),arg1);
            VARIABLE.UpdateData(arg1);
        }
    }
 
    private void UpdateUiStatusGeneralType(GetType type, Status arg1)
    {

        foreach (var VARIABLE in _infoElement[type].Values)
        {
            foreach (var VARIABLE2 in VARIABLE)
            {
                VARIABLE2.UpdateStatusTypeGeneral(type,arg1);
            }
       
        }
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
                foreach (var VARIABLE3 in VARIABLE2)
                {
                    VARIABLE3.Open(clearData);
                }
               
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
        
         _bank.OnUpdateGeneralTypeStatus += _generalTuskPanelUI.UpdateDataTypeGeneral;
         _bank.OnUpdateGeneralStatuseDef += _generalTuskPanelUI.UpdateData;

        _bank.OnUpdateElementStatuseType += UpdateUiStatusElement;
        _bank.OnUpdateGeneralTypeStatus += UpdateUiStatusGeneralType;
    }
    

    private void CheckCountElement(GetType type, int targetCount)
    {
        if (_buffer.ContainsKey(type) == false)
        {
            _buffer.Add(type,new List<TaskElementControllerUIType<GetType, Status>>());
        }
        
        
        if (targetCount > _buffer[type].Count)
        {
            int difference = targetCount - _buffer[type].Count;
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
