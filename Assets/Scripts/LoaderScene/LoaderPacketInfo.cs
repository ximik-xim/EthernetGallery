using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LoaderPacketInfo:MonoBehaviour
{
    public static LoaderPacketInfo PacketInfo;

    private Dictionary<int, ILoaderDataScene> _loadData = new Dictionary<int, ILoaderDataScene>();
    public int CountElement => _loadData.Count;

    //Статус конкретного элемента
    public Action<LoaderStatuse> OnUpdateElementStatuse;
    //это общий статус загрузки
    public Action<LoaderStatuse> OnUpdateGeneralStatuse; 

    [SerializeField]
    private UIScneteLoad _UIload; 

    private void Awake()
    {
        if (PacketInfo == null)
        {
            PacketInfo = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    

    public void AddLoadData(ILoaderDataScene loaderDataScene)
    {
        _loadData.Add(loaderDataScene.LoaderHash,loaderDataScene);

        loaderDataScene.OnStatus += RemoveLoadDataComlite;
        loaderDataScene.OnStatus += RemoveLoadDataError;

        loaderDataScene.OnStatus += ElementUpdateStatus;
    }

    private void RemoveLoadDataComlite(LoaderStatuse arg1)
    {
        if (arg1.Statuse == LoaderStatuse.Type.Complite)
        {
            _loadData[arg1.Hash].OnStatus -= RemoveLoadDataComlite;
            _loadData[arg1.Hash].OnStatus -= RemoveLoadDataError;
            
            _loadData[arg1.Hash].OnStatus -= ElementUpdateStatus;
            
            _loadData.Remove(arg1.Hash);
        }
    }
    
    
    private void RemoveLoadDataError(LoaderStatuse arg1)
    {
        if (arg1.Statuse == LoaderStatuse.Type.Error)
        {
            _loadData[arg1.Hash].OnStatus -= RemoveLoadDataComlite;
            _loadData[arg1.Hash].OnStatus -= RemoveLoadDataError;
            
            _loadData[arg1.Hash].OnStatus -= ElementUpdateStatus;
            
            _loadData.Remove(arg1.Hash);
        }
    }

    public void RemoveLoadData(ILoaderDataScene loaderDataScene)
    {
        _loadData.Remove(loaderDataScene.LoaderHash);
        
        loaderDataScene.OnStatus -= RemoveLoadDataComlite;
        loaderDataScene.OnStatus -= RemoveLoadDataError;
        
        loaderDataScene.OnStatus -= ElementUpdateStatus;
    }


    private void Startload()
    {
        foreach (var VARIABLE in _loadData.Values)
        {
            VARIABLE.StartLoad();
        }
    }

    private void ElementUpdateStatus(LoaderStatuse arg1)
    {
        OnUpdateElementStatuse.Invoke(arg1);
    }


//переходим на сцену и вибираем выпалнять действия будет до перехода на сцену или после
    // если после загрузки сцены тогда true
    // если до перехода на сцену тогда false
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
    
    private void UpdateInfoUI()
    {
        List<int> hashList = new List<int>();
        foreach (var VARIABLE in _loadData.Values)
        {
            hashList.Add(VARIABLE.LoaderHash);
        }
        _UIload.UpdateInform(hashList);
    }
    
    public void StartLoadResourse()
    {
        UpdateInfoUI();
        Startload();
    }
}
