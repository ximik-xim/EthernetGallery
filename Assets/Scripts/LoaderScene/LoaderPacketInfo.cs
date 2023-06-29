using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Отвечает за загрузку задач
[System.Serializable]
public class LoaderPacketInfo:MonoBehaviour
{
    //Сингалтон 
    public static LoaderPacketInfo PacketInfo;
    //Вернет кол-во задач в списке
    public int CountElement => _loadData.Count;
    //Статус конкретной задачи
    public Action<LoaderStatuse> OnUpdateElementStatuse;
    //это общий статус загрузки задач
    public Action<LoaderStatuse> OnUpdateGeneralStatuse;

    [SerializeField]
    private bool _disactiveStartUI;
    [SerializeField]
    private UIScneteLoad _UIload; 
    private Dictionary<int, ILoaderDataScene> _loadData = new Dictionary<int, ILoaderDataScene>();
    
    private int _countTasks=default;
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
    public void AddLoadData(ILoaderDataScene loaderDataScene)
    {
        _loadData.Add(loaderDataScene.LoaderHash,loaderDataScene);

        SubscribeEventElement(loaderDataScene);
    }
    
    /// <summary>
    /// Уберет задачу из список
    /// </summary>
    public void RemoveLoadData(ILoaderDataScene loaderDataScene)
    {
        _loadData.Remove(loaderDataScene.LoaderHash);
        
        UnsubscribeEventElement(loaderDataScene);
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
        if (PacketInfo == null)
        {
            PacketInfo = this;
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

    private void SubscribeEventElement(ILoaderDataScene loaderDataScene)
    {
        loaderDataScene.OnStatus += OnRemoveLoadDataComlite;
        loaderDataScene.OnStatus += OnRemoveLoadDataError;
            
        loaderDataScene.OnStatus += OnElementUpdateStatus;
        loaderDataScene.OnStatus += OnUpdateGeneralStatus;
    }
    
    private void UnsubscribeEventElement(ILoaderDataScene loaderDataScene)
    {
        loaderDataScene.OnStatus -= OnRemoveLoadDataComlite;
        loaderDataScene.OnStatus -= OnRemoveLoadDataError;
            
        loaderDataScene.OnStatus -= OnElementUpdateStatus;
        loaderDataScene.OnStatus -= OnUpdateGeneralStatus;
        
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
