using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Отвечает за загрузку задач
[System.Serializable]
public class LoaderTask:AbstractCalcalcal
{
    //Сингалтон 
    public static LoaderTask Task;
    //Вернет кол-во задач в списке
    public int CountElement => _loadData.Count;
    
    
    //Статус конкретной задачи
    public override Action<LoaderStatuse> OnUpdateElementStatuseDef
    {
        get => OnUpdateElementStatuse; 
        set=>OnUpdateElementStatuse=value; 
    }
    //это общий статус загрузки задач
    public override Action<LoaderStatuse> OnUpdateGeneralStatuseDef
    {
        get => OnUpdateGeneralStatuse;
        set => OnUpdateGeneralStatuse = value;
    }
    
    private Action<LoaderStatuse> OnUpdateElementStatuse;
    private Action<LoaderStatuse> OnUpdateGeneralStatuse;

    [SerializeField]
    private bool _disactiveStartUI;
    [SerializeField]
    private CreateTaskAndControlTaskUI _UIload; 
    private Dictionary<int, ILoaderTask> _loadData = new Dictionary<int, ILoaderTask>();
    
    private int _countTasks=default;
    //нужен только для подсчета итогового кол-во выполнения задач
    private Dictionary<int, float> _percentageTaskCompletion = new Dictionary<int, float>();
    
    /// <summary>
    /// Запустит загрузку у всех задач из списка
    ///  и включит UI очистив его
    /// </summary>
    public void StartLoadResourse()
    {
        UpdateInfoUI();
        Startload();
    }

    /// <summary>
    /// Загружает сцену и выполняет списко задач до или после загрузки сцены
    ///  если после загрузки сцены тогда true
    ///  если до перехода на сцену тогда false
    /// </summary>
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
    /// Добавит задачу в список
    /// </summary>
    public void AddLoadData(ILoaderTask loaderTask)
    {
        _loadData.Add(loaderTask.LoaderHash,loaderTask);

        SubscribeEventElement(loaderTask);
    }
    
    /// <summary>
    /// Уберет задачу из список
    /// </summary>
    public void RemoveLoadData(ILoaderTask loaderTask)
    {
        _loadData.Remove(loaderTask.LoaderHash);
        
        UnsubscribeEventElement(loaderTask);
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

    private void Awake()
    {
        if (Task == null)
        {
            Task = this;
        }
        DontDestroyOnLoad(gameObject);

        OnUpdateGeneralStatuse += OnDisactiveLoaderComplite;

        if (_disactiveStartUI == true)
        {
            DisactiveUiLoader();
        }
    }
    
    private void Startload()
    {
        ActiveUILoader(true);
        _countTasks = _loadData.Count;
        _percentageTaskCompletion = new Dictionary<int, float>();
        
        foreach (var VARIABLE in _loadData.Values)
        {
            VARIABLE.StartLoad();
        }
    }

    private void UpdateInfoUI()
    {
        List<int> hashList = new List<int>();
        foreach (var VARIABLE in _loadData.Values)
        {
            hashList.Add(VARIABLE.LoaderHash);
        }
        _UIload.UpdateInform(hashList);
    }

    private void SubscribeEventElement(ILoaderTask loaderTask)
    {
        loaderTask.OnStatus += OnRemoveLoadDataComlite;
        loaderTask.OnStatus += OnRemoveLoadDataError;
            
        loaderTask.OnStatus += OnElementUpdateStatus;
        loaderTask.OnStatus += OnUpdateGeneralStatus;
    }
    
    private void UnsubscribeEventElement(ILoaderTask loaderTask)
    {
        loaderTask.OnStatus -= OnRemoveLoadDataComlite;
        loaderTask.OnStatus -= OnRemoveLoadDataError;
            
        loaderTask.OnStatus -= OnElementUpdateStatus;
        loaderTask.OnStatus -= OnUpdateGeneralStatus;
        
    }
    
    private void OnRemoveLoadDataComlite(LoaderStatuse arg1)
    {
        if (arg1.Statuse == LoaderStatuse.StatusLoad.Complite)
        {
            UnsubscribeEventElement(_loadData[arg1.Hash]);
            
            _loadData.Remove(arg1.Hash);
        }
    }
    
    
    private void OnRemoveLoadDataError(LoaderStatuse arg1)
    {
        if (arg1.Statuse == LoaderStatuse.StatusLoad.Error)
        {
            UnsubscribeEventElement(_loadData[arg1.Hash]);
            
            _loadData.Remove(arg1.Hash);
        }
    }
    
    private void OnElementUpdateStatus(LoaderStatuse arg1)
    {
        OnUpdateElementStatuse.Invoke(arg1);
    }

    private void OnUpdateGeneralStatus(LoaderStatuse arg1)
    {
        if (_percentageTaskCompletion.ContainsKey(arg1.Hash) == false)
        {
            _percentageTaskCompletion.Add(arg1.Hash,arg1.Comlite);
        }
        _percentageTaskCompletion[arg1.Hash] = arg1.Comlite;
        
        float d = 1f / _countTasks;

        float comlite = 0;
        foreach (var VARIABLE in _percentageTaskCompletion.Values)
        {
            comlite += d * VARIABLE;
        }

        if (comlite != 1f)
        {
            OnUpdateGeneralStatuse?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Load, arg1.Hash, "Общая загрузка", comlite));
            return;
        }
        
        OnUpdateGeneralStatuse?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Complite, arg1.Hash, "Общая загрузка", comlite));
        
    }

    private void OnDisactiveLoaderComplite(LoaderStatuse arg1)
    {
        if (arg1.Statuse == LoaderStatuse.StatusLoad.Complite)
        {
            DisactiveUiLoader();
        }
    }


}
