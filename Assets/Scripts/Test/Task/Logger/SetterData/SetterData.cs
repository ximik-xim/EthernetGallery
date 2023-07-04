using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Засовывает данные об созданном префабе в скрипт, которому нужны эти данные
//В режиме One -  будет один экземпляр префаба, который и будет передан во все скрипты которым нужны данные
//В режиме Full - будет создан экземпляр префаба для каждого скрипта которому нужны данные
public abstract class SetterData<InstObj, SetDataInObj> : MonoBehaviour where SetDataInObj : ISetterData<InstObj> where InstObj : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] 
    protected Transform _parent;
    [SerializeField] 
    protected InstObj _prefab;
    [SerializeField] 
    protected List<InstObj> _listInstObj;

    [Header("Insert")]
    [SerializeField] 
    //нужно что бы было видно в инспекторе но было не активно, т.к логики в добавлении тут элементов через инсппектор нету
    //А хотя есть(можно засунуть элемент и потом ему задаться значение
    protected List<SetDataInObj> _listSetDataInstObj;
    [SerializeField]
    protected Type _type;
    
    public enum Type
    {
        One,
        Full
    }

    protected virtual void Init()
    {
        CheckNull();
        
        if (_type == Type.One)
        {
            if (_listInstObj.Count == 0)
            {
                var obj = Instantiate(_prefab, _parent);

                _listInstObj.Add(obj);

            }
        }

    
    }

    //Засовывание данных всем элементом в списке для уст. данных
    public virtual void SetDataLenghtList()
    {
        if (_type == Type.One)
        {
            foreach (var VARIABLE in _listSetDataInstObj)
            {
                VARIABLE.SetData(_listInstObj[0]);
            }
        }

        if (_type == Type.Full)
        {
            int targetCount = _listSetDataInstObj.Count;
            targetCount -= _listInstObj.Count;

            for (int i = 0; i < targetCount; i++)
            {
                var obj = Instantiate(_prefab, _parent);;
                
                _listInstObj.Add(obj);
            }

            for (int i = 0; i < _listSetDataInstObj.Count; i++)
            {
                _listSetDataInstObj[i].SetData(_listInstObj[i]);
            }

        }

    }

    public virtual void AddElementSetData(SetDataInObj loggerElementUI )
    {
        if (_type == Type.One)
        {
            _listSetDataInstObj.Add(loggerElementUI);
            loggerElementUI.SetData(_listInstObj[0]);
        }

        if (_type == Type.Full)
        {
            var obj = Instantiate(_prefab, _parent);;
            
            _listInstObj.Add(obj);
            _listSetDataInstObj.Add(loggerElementUI);
            
            
            loggerElementUI.SetData(obj);
        }
    }

    protected void CheckNull()
    {
        int target = _listInstObj.Count;
        for (int i = 0; i < target; i++)
        {
            if (_listInstObj[i] == null)
            {
                _listInstObj.Remove(_listInstObj[i]);
                
                i--;
                target--;
            }
        }

        target = _listSetDataInstObj.Count;
        for (int i = 0; i < target; i++)
        {
            if (_listSetDataInstObj[i] == null)
            {
                _listSetDataInstObj.Remove(_listSetDataInstObj[i]);
                
                i--;
                target--;
            }
        }
    }
}