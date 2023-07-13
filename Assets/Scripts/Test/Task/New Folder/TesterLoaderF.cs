using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterLoaderF : MonoBehaviour
{

    public static TesterLoaderF statikLoad;
    public static Action OnInit;

[SerializeField]
    private bool _useExternalTaskPanel = false;
    [SerializeField] 
    private NewIntrefaseControlUITE _intrefaseControlUite;
    [SerializeField] 
    private Transform _parentTask;
    private Dictionary<Interfasda, float> _dictionary;
    public Action<LoaderStatuse> GeneralTask; 







    public void OpenExternalPanel()
    {
        if (_useExternalTaskPanel == false)
        {
            Debug.LogError("Ошибка, общая панель для разных типов Task не была включена");
            return;
        }
        
        _intrefaseControlUite.Open();
    }
    
    
    public void CloseExternalPanel()
    {
        if (_useExternalTaskPanel == false)
        {
            Debug.LogError("Ошибка, общая панель для разных типов Task не была включена");
            return;
        }
        
        _intrefaseControlUite.Close();
    }
    
    
    private void Awake()
    {
        if (_useExternalTaskPanel == true)
        {
            GeneralTask += _intrefaseControlUite.UpdateData;

        }
        
        
        statikLoad = this;
        OnInit?.Invoke();
        
        DontDestroyOnLoad(gameObject);
    }


    //(Type)type - тип ключа
    //(int) ref type - ссылка на экз ключа(нужен хэш) (в случ с enum нужно будет просто обор. в класс FacT ) ( В случ Tlist у обьектов ScrOjb хран. ключ знач будут разные хэши )
    //(Interfasda) - Метод для добавления Task
    /// <summary>
    /// я тут подумал, если буду исп. Tlist в качестве типа или буду исп всего лишь 1 Enum, то и экземпляров Bank тода будет всего лишь 1
    /// хотя нет, стоп. хэш ключа та будет разный (enum  будет упакован в класс и по этому хэши даже  у него будут разные, тут все окейы)
    /// </summary>
    private Dictionary<Type, Dictionary<int, Interfasda>> _dictionaryBank = new Dictionary<Type, Dictionary<int, Interfasda>>();

    public void AddBank(Type typeKey,int hashKey,Interfasda interfaceAdd)
    {
        Debug.Log("ADD Bank");
        if (_dictionaryBank.ContainsKey(typeKey) == false)
        {
            _dictionaryBank.Add(typeKey,new Dictionary<int, Interfasda>());
        }

        if (_dictionaryBank[typeKey].ContainsKey(hashKey) == false)
        {
            _dictionaryBank[typeKey].Add(hashKey,interfaceAdd);
            
            return;
        }
        
        Debug.LogError("Ошибка, экземпляр хранилища с Task с таким ключем уже установлен");
        
    }
    
    public void RemoveBank(Type typeKey,int hashKey, Interfasda interfaceAdd)
    {
        
    }
    
    public void AddTaskType(Type typeKey,int hashKey, ILoaderTask task)
    {
       
        
        if (_dictionaryBank.ContainsKey(typeKey) == false)
        {
            Debug.LogError("Ошибка ключ " + typeKey + " не был найден");
            return;
        }
        
        if (_dictionaryBank[typeKey].ContainsKey(hashKey) == false)
        {
            Debug.LogError("Ошибка хэш ключа " + hashKey + " не был найден");
            return;
        }
        _dictionaryBank[typeKey][hashKey].AddtTT(task);
        Debug.Log("Task Добавлена");
    }

    public void RemoveTaskType(Type typeKey,int hashKey, ILoaderTask task)
    {
        
    }




    public void StartLoadBankGeneral()
    {


        
            
       
        
        foreach (var VARIABLE in _dictionaryBank.Keys)
        {
            foreach (var VARIABLE2 in _dictionaryBank[VARIABLE].Keys)
            {
                if (_useExternalTaskPanel == true)
                {
                    _dictionaryBank[VARIABLE][VARIABLE2].OnUpdateGeneralStatuseDef += dfadfadfas;
                    _dictionaryBank[VARIABLE][VARIABLE2].AddParentUITask(new ParentDataSet(_parentTask)); 
                }
                
                _dictionaryBank[VARIABLE][VARIABLE2].StartLoad();
            }
        }
    }

    private void dfadfadfas(LoaderStatuse obj)
    {
       
            float comlite = 0;
            int value = 0;
            foreach (var VARIABLE in _dictionaryBank.Keys)
            {
                foreach (var VARIABLE2 in _dictionaryBank[VARIABLE].Keys)
                {
                    value++;
                    comlite+= _dictionaryBank[VARIABLE][VARIABLE2].GeneralStatusComlite;
                }
            }

            comlite /= value;

            if (comlite != 1f)
            {
                GeneralTask?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Load, this.GetHashCode(), "Общая загрузка", comlite));
                return;
            }
            
            GeneralTask?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Complite, this.GetHashCode(), "Общая загрузка", comlite));
    }

    public void StartLoadBank(Type typeKey,int hashKey)
    {
        _dictionaryBank[typeKey][hashKey].StartLoad();
    }

    public void StartLoadBanKScene(Type typeKey,int hashKey,int idScene, bool executeAfterLoading)
    {
        
    }

    public void ActiveUILoaderBank(Type typeKey, int hashKey, bool clear = false)
    {
        
    }

    public void DisactiveUiLoaderBank(Type typeKey, int hashKey)
    {
        
    }

    public void AddParentUITask(Type typeKey, int hashKey, ParentDataSet parentDataSet)
    {
        _dictionaryBank[typeKey][hashKey].AddParentUITask(parentDataSet);
    }

    public void AddParentUITaskTypeTT(Type typeKey, int hashKey, ParentDataSet parentDataSet)
    {
        _dictionaryBank[typeKey][hashKey].AddParentUITaskTypeTT(parentDataSet);
    }
    
    
    
    
}
