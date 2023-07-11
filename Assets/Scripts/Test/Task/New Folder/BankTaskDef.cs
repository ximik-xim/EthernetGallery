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

    
    //SetМожет сработать как приравнивание значение а не как добавление(Проверить это)
    public override Action<GetType, Status> OnUpdateElementStatuseType 
    {   
        get => OnUpdateElementStatuse;
        set => OnUpdateElementStatuse = value;
    }
    public override Action<GetType, Status> OnUpdateGeneralStatuseType 
    { 
        get => OnUpdateGeneralStatuse;
        set => OnUpdateGeneralStatuse = value;
    }

    public override Action<LoaderStatuse> OnUpdateElementStatuseDef 
    { 
        get => OnUpdateElementStatuseDefault;
        set => OnUpdateElementStatuseDefault = value;
    }
    public override Action<LoaderStatuse> OnUpdateGeneralStatuseDef 
    { 
        get => OnUpdateGeneralStatuseDefault;
        set => OnUpdateGeneralStatuseDefault = value;
    }

    private Action<GetType, Status> OnUpdateElementStatuse;
    private Action<GetType, Status> OnUpdateGeneralStatuse;
    
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

        TesterLoaderF.OnInit += asfa;
 
        
        
        
                
        DontDestroyOnLoad(gameObject);

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
        // loaderTask.OnStatus += OnRemoveLoadDataComlite;
        // loaderTask.OnStatus += OnRemoveLoadDataError;
        //     
         loaderTask.OnStatus += OnElementUpdateStatus;
        // loaderTask.OnStatus += OnUpdateGeneralStatus;
    }
    
    private void UnsubscribeEventElement(ItesTaskType loaderTask)
    {
        //loaderTask.OnStatus -= OnRemoveLoadDataComlite;
        //loaderTask.OnStatus -= OnRemoveLoadDataError;
            
        loaderTask.OnStatus -= OnElementUpdateStatus;
        //loaderTask.OnStatus -= OnUpdateGeneralStatus;
        
    }

    private void OnElementUpdateStatus(LoaderStatuse types)
    {
        Status status = types as Status;
        
        OnUpdateElementStatuse?.Invoke(status.GetKey(),status);
        OnUpdateElementStatuseDefault?.Invoke(types);
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

            
            if (_dictionary.ContainsKey(obj.GetKey()) == false)
            {
                _dictionary.Add(obj.GetKey(),new Dictionary<int, ItesTaskType>());
            }
           
            
            _dictionary[obj.GetKey()].Add(obj.LoaderHash,obj);
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

    
    //Так все подписки для удаленеия элементов и т.д потом напишу, пока гланое нужно написать
    
    //Подумать над этим
    public void StartLoadScene(int idScene,bool executeAfterLoading)
    {

        if (executeAfterLoading == true)
        {
            UpdateInfoUI();
            Startload();
            SceneManager.LoadScene(idScene);
          
            return;
        }

        UpdateInfoUI();
        SceneManager.LoadScene(idScene);
        Startload();
      
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
    

    private void Startload()
    {
        ActiveUILoader(true);
        //_countTasks = _loadData.Count;
        //_percentageTaskCompletion = new Dictionary<int, float>();
        
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
        
        _UIload.UpdateInform(hashList);
    }
    
    protected abstract int GetHashKey();


  
}
