using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum fafasdfdaga
{
    dadas
}
/// <summary>
/// Все хранилища должны быть Singlton
/// А хотя нет
/// </summary>
/// <typeparam name="ItesTaskType"></typeparam>
/// <typeparam name="GetType"></typeparam>
/// <typeparam name="Status"></typeparam>
public abstract class BankTaskDef<ItesTaskType,GetType,ElementType,Status> : AbstrCal<GetType,Status>,Interfasda where ItesTaskType:ItesTaskType<GetType> where  Status : TesStatType<GetType> where ElementType : IGetKey<GetType>
{

    private TesterLoaderF _loaderF;

    [SerializeField]
    private TaskUIControleTe<GetType,ElementType,Status> _UIload;

    private bool _isLoad = false;



    private Dictionary<GetType, Dictionary<int, float>> _percentageTaskCompletion = new Dictionary<GetType, Dictionary<int, float>>();

    private List<ParentDataSet> _parentDataSet =new List<ParentDataSet>();
    private List<ParentDataSetType<GetType>> _parentDataSetType = new List<ParentDataSetType<GetType>>();


    /// <summary>
    /// Обновление статуса у Task с типом
    /// </summary>
    public override Action<GetType, Status> OnUpdateElementStatuseType 
    {   
        get => OnUpdateElementStatuse;
        set => OnUpdateElementStatuse = value;
    }

    /// <summary>
    /// Обновление статуса у Task 
    /// </summary>
    public override Action<LoaderStatuse> OnUpdateElementStatuseDef 
    { 
        get => OnUpdateElementStatuseDefault;
        set => OnUpdateElementStatuseDefault = value;
    }
    
    /// <summary>
    /// Обновление общего статуса у Task 
    /// </summary>
    public override Action<LoaderStatuse> OnUpdateGeneralStatuseDef 
    { 
        get => OnUpdateGeneralStatuseDefault;
        set => OnUpdateGeneralStatuseDefault = value;
    }

    /// <summary>
    /// Обновление общего статуса типа у Task с типом 
    /// </summary>
    public override Action<GetType, Status> OnUpdateGeneralTypeStatus
    {
        get => OnUpdateGeneralTypeStatuse;
        set => OnUpdateGeneralTypeStatuse = value;

    }

    private Action<GetType, Status> OnUpdateElementStatuse;
    private Action<GetType, Status> OnUpdateGeneralTypeStatuse;
    
    private Action<LoaderStatuse> OnUpdateElementStatuseDefault;
    private Action<LoaderStatuse> OnUpdateGeneralStatuseDefault;

    /// <summary>
    /// хринит список Task по их типу
    /// В данном случае 2 словарь нужен для того, что бы убедиться что Task не будет записанна 2 раз
    ///
    /// GetType - тип ключа
    /// int - хэш Task(для уникальности)
    /// ItesTaskType - Сама TypeTask
    /// </summary>
    private Dictionary<GetType,Dictionary<int,ItesTaskType>> _dictionary =new Dictionary<GetType, Dictionary<int, ItesTaskType>>();

    [SerializeField]
    private bool _disactiveStartUI;
    
    //Пока не буду добавлять словарь для определения общего процента загрузки

    protected void Init()
    {
        if (TesterLoaderF.statikLoad == null)
        {
            TesterLoaderF.OnInit += asfa;
        }
        else
        {
            _loaderF = TesterLoaderF.statikLoad;
            _loaderF.AddBank(typeof(GetType), GetHashKey(), this);
        }




//        DontDestroyOnLoad(gameObject);

        //OnUpdateGeneralStatuse += OnDisactiveLoaderComplite;

        if (_disactiveStartUI == true)
        {
            DisactiveUiLoader();
        }
    }

    private void asfa()
    {
        _loaderF = TesterLoaderF.statikLoad;
        _loaderF.AddBank(typeof(GetType), GetHashKey(), this);
        
        TesterLoaderF.OnInit -= asfa;
    }
    
    private void SubscribeEventElement(ItesTaskType loaderTask)
    {
         loaderTask.OnStatus += OnRemoveLoadDataComlite;
         loaderTask.OnStatus += OnRemoveLoadDataError;
             
         loaderTask.OnStatus += OnElementUpdateStatus;
         loaderTask.OnStatus += OnUpdateGeneralStatus;
         
    }
    
    private void UnsubscribeEventElement(ItesTaskType loaderTask)
    {
        loaderTask.OnStatus -= OnRemoveLoadDataComlite;
        loaderTask.OnStatus -= OnRemoveLoadDataError;
            
        loaderTask.OnStatus -= OnElementUpdateStatus;
        loaderTask.OnStatus -= OnUpdateGeneralStatus;
        
    }

    private void OnElementUpdateStatus(LoaderStatuse types)
    {
        Status status = types as Status;
        
        OnUpdateElementStatuse?.Invoke(status.GetKey(),status);
        OnUpdateElementStatuseDefault?.Invoke(types);
    }
    
    
    private void OnRemoveLoadDataComlite(LoaderStatuse arg1)
    {
        Status status = arg1 as Status;
        
        if (status.Statuse == LoaderStatuse.StatusLoad.Complite)
        {
            UnsubscribeEventElement(_dictionary[status.GetKey()][status.Hash]);

            _dictionary[status.GetKey()].Remove(status.Hash);
        }
    }
    
    
    private void OnRemoveLoadDataError(LoaderStatuse arg1)
    {
        Status status = arg1 as Status;
        if (arg1.Statuse == LoaderStatuse.StatusLoad.Error)
        {
            UnsubscribeEventElement(_dictionary[status.GetKey()][status.Hash]);
            
            _dictionary[status.GetKey()].Remove(status.Hash);
        }
    }




    /// <summary>
    /// Добавить bool
    /// Вынести логику для заполнения списка выполн задач в StartLoad
    /// </summary>
    /// <param name="arg1"></param>
    ///
    ///

    private void InsetrtDataGeneral()
    {
        _percentageTaskCompletion = new Dictionary<GetType, Dictionary<int, float>>();
        foreach (var VARIABLE in _dictionary.Keys)
        {
            if (_percentageTaskCompletion.ContainsKey(VARIABLE) == false)
            {
                _percentageTaskCompletion.Add(VARIABLE, new Dictionary<int, float>());
            }
            
            foreach (var VARIABLE2 in _dictionary[VARIABLE])
            {
                if (_percentageTaskCompletion[VARIABLE].ContainsKey(VARIABLE2.Key) == false)
                {
                    _percentageTaskCompletion[VARIABLE].Add(VARIABLE2.Key, 0);
                }
            }
        }
        
        
        
    }
    private void OnUpdateGeneralStatus(LoaderStatuse arg1)
    {
        Status status = arg1 as Status;
        
        _percentageTaskCompletion[status.GetKey()][arg1.Hash] = arg1.Comlite;


        float fullComlite = 0;
        foreach (var VARIABLE11 in _percentageTaskCompletion.Keys)
        {


            float d = 1f / _percentageTaskCompletion[VARIABLE11].Count;

            float comlite = 0;
            
            foreach (var VARIABLE in _percentageTaskCompletion[VARIABLE11].Values)
            {
                comlite += d * VARIABLE;
            }

            fullComlite += comlite;
            
            if (comlite != 1f)
            {

                //Хммм не могу создать экземпляр типа Status из за обобщения(странно, ведь доступ к конструктору класса имею)
                //TesStatType<GetType> asdas = new TesStatType<GetType>(LoaderStatuse.StatusLoad.Complite,arg1.Hash,"",comlite);
                var stat1 = LoadStatLoad(arg1.Hash, comlite);
                stat1.SetKey(VARIABLE11);
                OnUpdateGeneralTypeStatuse?.Invoke(VARIABLE11,stat1);
                
                continue;
            }

            var stat2 = LoadStatComlite(arg1.Hash, comlite);
            stat2.SetKey(VARIABLE11);
                
            OnUpdateGeneralTypeStatuse?.Invoke(VARIABLE11,stat2);

        }

        fullComlite = fullComlite / _percentageTaskCompletion.Count;
        
        if (fullComlite != 1f)
        {
            Debug.Log("StatuseDefaulLoad");
            OnUpdateGeneralStatuseDefault?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Load, arg1.Hash, "Общая загрузка", fullComlite));
            return;
        }
        
        Debug.Log("StatuseDefaulComplite");
        OnUpdateGeneralStatuseDefault?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Complite, arg1.Hash, "Общая загрузка", fullComlite));

        _isLoad = false;
        _parentDataSet = new List<ParentDataSet>();
        _parentDataSetType = new List<ParentDataSetType<GetType>>();

    }
    
    
    
    
    

    public void Add(ItesTaskType Task)
    {
        SubscribeEventElement(Task);
        
        _dictionary[Task.GetKey()].Add(Task.LoaderHash,Task);

        
    }
    
    public void RemoveLoadData(ItesTaskType Task)
    {


        _dictionary[Task.GetKey()].Remove(Task.LoaderHash);
        UnsubscribeEventElement(Task);
    }

    public void AddtTT(ILoaderTask taskType)
    {
        ItesTaskType obj =(ItesTaskType) taskType;

        if (obj != null)
        {
            SubscribeEventElement(obj);

            Debug.Log("Key Value = "+obj.GetKey());
            if (_dictionary.ContainsKey(obj.GetKey()) == false)
            {
                _dictionary.Add(obj.GetKey(),new Dictionary<int, ItesTaskType>());
            }

            _dictionary[obj.GetKey()].Add(obj.LoaderHash,obj);
            return;
        }
        
        Debug.LogError("Task другого типа");
    }

    public void RemoveTT(ILoaderTask taskType)
    {
        ItesTaskType obj =(ItesTaskType) taskType;
        if (obj != null)
        {
            UnsubscribeEventElement(obj);
            
            if (_dictionary.ContainsKey(obj.GetKey()) == false)
            {
             Debug.LogError("Task с таким ключем не было");
             return;
            }


            if (_dictionary[obj.GetKey()].ContainsKey(obj.LoaderHash) == false)
            {
                Debug.LogError("Task с таким хэшем ключа не было");
                return;
            }
            
            _dictionary[obj.GetKey()].Remove(obj.LoaderHash);
            return;
        }
        
        Debug.LogError("Task другого типа");
    }

    public void RemoveLoadData(ILoaderTask taskType)
    {

        ItesTaskType obj =(ItesTaskType) taskType;

        if (obj != null)
        {
            UnsubscribeEventElement(obj);
            _dictionary[obj.GetKey()].Remove(obj.LoaderHash);
            return;
        }
        
        Debug.LogError("Task другого типа");
        
       
    }


    public void StartLoadScene(int idScene, bool executeAfterLoading)
    {

        if (_isLoad == false)
        {
            _isLoad = true;

            if (executeAfterLoading == true)
            {
                UpdateInfoUI();
                StartLoadeeeee();
                SceneManager.LoadScene(idScene);

                return;
            }

            UpdateInfoUI();
            SceneManager.LoadScene(idScene);
            StartLoadeeeee();
        
            return;
        }
        
        Debug.LogError("ОШИБКА, Загрузка Task уже запущена");
}
    
    
    
    /// <summary>
    /// Включит UI загрузчика
    /// </summary>
    public void ActiveUILoader(bool clear = false)
    {
        _UIload.Open(clear);
    }
    
    /// <summary>
    /// Выключит UI загрузчика
    /// </summary>
    public void DisactiveUiLoader()
    {
        _UIload.Close();
    }

    public void StartLoad()
    {
        if (_isLoad == false)
        {
            _isLoad = true;
            
            UpdateInfoUI();
            StartLoadeeeee();

            return;
        }
        Debug.LogError("ОШИБКА, Загрузка Task уже запущена");
    }

    private void StartLoadeeeee()
    {
        InsetrtDataGeneral();
        
        ActiveUILoader(true);

        foreach (var VARIABLE in _dictionary.Keys)
        {

            foreach (var VARIABLE2 in _dictionary[VARIABLE].Values)
            {
                VARIABLE2.StartLoad();
            }
        }
    }

    private void UpdateInfoUI()
    {
        Dictionary<GetType, List<int>> hashList = new Dictionary<GetType, List<int>>();
        foreach (var VARIABLE in _dictionary.Keys)
        {
            hashList.Add(VARIABLE,new List<int>());

            foreach (var VARIABLE2 in _dictionary[VARIABLE])
            {
                hashList[VARIABLE].Add(VARIABLE2.Key);
            }
        }
        
        _UIload.UpdateInform(hashList,_parentDataSet,_parentDataSetType);
    }
    
    protected abstract int GetHashKey();

    protected abstract Status LoadStatLoad(int hashTask,float comliteTask);
    
    protected abstract Status LoadStatComlite(int hashTask, float comliteTask);



    public void AddParentUITask(ParentDataSet parentDataSet)
    {
        _parentDataSet.Add(parentDataSet);
    }
    
    
    public void AddParentUITaskType(ParentDataSetType<GetType> parentDataSetType)
    {
        _parentDataSetType.Add(parentDataSetType);
    }
    
    
    public void AddParentUITaskTypeTT(ParentDataSet parentDataSet)
    {
        ParentDataSetType<GetType> obj = (ParentDataSetType<GetType>) parentDataSet;
        _parentDataSetType.Add(obj);

        

        
    }

}
