using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



/// <summary>
/// Key - ключ
/// TypeList - класс эелементов списков
/// InstObj-префаб обьекта, который будет создоваться
/// SetDataInObj - класс в который будут переданны данные об созданном префабе, класс должен реализовывать
/// интерфеис ISetterData для передачи в этот класс данных  
/// 
/// Засовывает данные об созданном префабе в скрипт, которому нужны эти данные
/// Этот класс нужен в случае, если ключ(Key) будет доставаться каким либо образом из элементов списка
/// В таком сулчае класс TypeList должен реализовывать интерфеис для получения ключа IGetKey
/// 
/// В режиме One -  будет один экземпляр префаба, который и будет передан во все скрипты которым нужны данные
/// В режиме Full - будет создан экземпляр префаба для каждого скрипта которому нужны данные
///
/// В списоке  _listInstObj храняться все созданные префабы, используемые для передачи их в один из элементов списка _listSetDataInstObj  
/// В список  _listInstObj можно засунуть через инспектор уже создданные на сцене экземпляры префабов и они будут исп. для передачи
/// их один из элементов списка _listSetDataInstObj при вызове моетода SetDataLenghtList
/// 
/// В списоке _listSetDataInstObj храняться все елементы куда были переданны данные об созданном префабе
/// В список _listSetDataInstObj можно засунуть через инспектор элементы которым нужно передать информ. об созданных префабах 
/// </summary>
public class SetterDataTypeTList <Key,TypeList,InstObj, SetDataInObj> : MonoBehaviour where SetDataInObj : ISetterData<InstObj> where InstObj : MonoBehaviour where TypeList: IGetKey<Key> 
{
    [Header("Spawn")]
[SerializeField]
    protected List<SetterPrefabData<TypeList,InstObj>> _listPrefab;
//Список нужен пока только для отображения
[SerializeField]
    protected List<SetterKeyListData<TypeList,InstObj>> _listInstObj;
    protected Dictionary<Key,List<InstObj> > _inst;

    [Header("Insert")] 
    [SerializeField] 
    protected List<SetterKeyListData<TypeList, SetDataInObj>> _listSetDataInstObj;
    protected Dictionary<Key, List<SetDataInObj>> _setData;

    

    /// <summary>
    /// Очистит списки от пустых элементов
    /// И если выбран тип One и не указ. созданный на сцене экземпляр префаба, то создаст этот экземпляр префаба
    /// </summary>
    protected virtual void Init()
    {
        CheckNull();
        
        InsertDataDictinary();

        foreach (var VARIABLE in _listPrefab)
        {
            if (VARIABLE.setterType == SetterTypeSet.One)
            {
                if (_inst[VARIABLE._key.GetKey()].Count==0)
                {
                    var obj = Instantiate(VARIABLE._prefab, VARIABLE._parent);
                    _inst[VARIABLE._key.GetKey()].Add(obj);
                }
            }
            
        }

    }
    
    /// <summary>
    /// Устанавливает всем элементам из списка _listSetDataInstObj информацию об префабах
    /// Если выбран режим One то уст. 1 элемент в списке созданных экземпляров префаба(_listInstObj)
    /// Если выбран режим FUll то проходит по всем имеющимся элементам в списке _listSetDataInstObj и
    /// передает значения элементов из списка _listInstObj (если нехватает элементов в списке _listInstObj,
    /// то создает новые экземпляры префабов)
    /// </summary>
    public virtual void SetDataLenghtList()
    {
        foreach (var VARIABLE in _listPrefab)
        {
            if (VARIABLE.setterType == SetterTypeSet.One)
            {
                foreach (var VARIABLE2 in _setData[VARIABLE._key.GetKey()])
                {
                    VARIABLE2.SetData(_inst[VARIABLE._key.GetKey()][0]);
                }
            }
            
            
            if (VARIABLE.setterType == SetterTypeSet.Full)
            {
                foreach (var VARIABLE2 in _setData[VARIABLE._key.GetKey()])
                {
                    int tar = _setData[VARIABLE._key.GetKey()].Count;
                    tar -= _inst[VARIABLE._key.GetKey()].Count;

                    for (int i = 0; i < tar; i++)
                    {
                        var obj = Instantiate(VARIABLE._prefab, VARIABLE._parent);
                        _inst[VARIABLE._key.GetKey()].Add(obj);
                    }

                    tar = _setData[VARIABLE._key.GetKey()].Count;
                    for (int i = 0; i < tar; i++)
                    {
                        var data = _inst[VARIABLE._key.GetKey()][i];
                        _setData[VARIABLE._key.GetKey()][i].SetData(data);
                    }
                    
                }
            }
        }
    }

    /// <summary>
    /// Заполняет словари данными из списков(ключами и экземплярами префабов)
    /// </summary>
    protected virtual void InsertDataDictinary()
    {
        _inst = new Dictionary<Key, List<InstObj>>();
        foreach (var VARIABLE in _listInstObj)
        {
            _inst.Add(VARIABLE._key.GetKey(),VARIABLE._prefabs);
        }

        _setData = new Dictionary<Key, List<SetDataInObj>>();
        foreach (var VARIABLE in _listSetDataInstObj)
        {
            _setData.Add(VARIABLE._key.GetKey(),VARIABLE._prefabs);
        }
    }
    
    /// <summary>
    /// Добавит элемент в список для передачи данных об префабе
    /// И передаст информацию об уже созданном префабе (если тип One)
    /// Или создаст новый экземпляр префаба и передаст информацию об новосозданном префабе
    /// </summary>
    public virtual void AddElementSetData(Key key, SetDataInObj SetDataInObj )
    {
        foreach (var VARIABLE in _listPrefab)
        {
            if (VARIABLE._key.GetKey().Equals(key))
            {
                if (VARIABLE.setterType == SetterTypeSet.One)
                {
                    _setData[key].Add(SetDataInObj);
                   
                    SetDataInObj.SetData(_inst[key][0]);
                }


                if (VARIABLE.setterType == SetterTypeSet.Full)
                {
                    var obj = Instantiate(VARIABLE._prefab, VARIABLE._parent);
                   
                    _inst[key].Add(obj);
                    _setData[key].Add(SetDataInObj);
                    
                    SetDataInObj.SetData(obj);
                }
                
            }
            
        }
        
    }
    //Проверка на пустые значения и заполнения списков по ключам
    protected void CheckNull()
    {
        
        //Очистка пустых элементов списка с префабами(_listPrefab)
        int target = _listPrefab.Count;
        for (int i = 0; i < target; i++)
        {
            if (ChekNullElementListPref(_listPrefab[i])  == true)
            {
                _listPrefab.Remove(_listPrefab[i]);

                i--;
                target--;
            }
        }

        //Очистка пустых элементов списка созданных экземпляров(_listInstObj)
        target = _listInstObj.Count;
        for (int i = 0; i < target; i++)
        {
            if (ChekNullElementListInt(_listInstObj[i]) == true)
            {
                _listInstObj.Remove(_listInstObj[i]);

                i--;
                target--;
            }
        }


        //Очистка пустых элементов внутреннего списка в списка созданных экземпляров(_listInstObj)
        for (int i = 0; i < _listInstObj.Count; i++)
        {
            target = _listInstObj[i]._prefabs.Count;
            for (int j = 0; j < target; j++)
            {
                if (ChekNullElementListIntElement(_listInstObj[i]._prefabs[j]) == true) 
                {
                    _listInstObj[i]._prefabs.Remove(_listInstObj[i]._prefabs[j]);

                    j--;
                    target--;
                }
            }
        }
        

        //Очистка пустых элементов списка элементов для засовывания в них данных(_listSetDataInstObj)
        target = _listSetDataInstObj.Count;
        for (int i = 0; i < target; i++)
        {
            if (ChekNullElementListSetData (_listSetDataInstObj[i]) == true)
            {
                _listSetDataInstObj.Remove(_listSetDataInstObj[i]);

                i--;
                target--;
            }
        }


        //Очистка пустых элементов внутреннего списка в списка элементов для засовывания в них данных(_listSetDataInstObj)
        for (int i = 0; i < _listSetDataInstObj.Count; i++)
        {
            target = _listSetDataInstObj[i]._prefabs.Count;
            for (int j = 0; j < target; j++)
            {
                if (ChekNullElementListSetDataElement(_listSetDataInstObj[i]._prefabs[j]) == true)
                {
                    _listSetDataInstObj[i]._prefabs.Remove(_listSetDataInstObj[i]._prefabs[j]);

                    j--;
                    target--;
                }
            }
        }
        
        //добавление всех типов ключей из списка _listPrefab в список _listInstObj
        for (int i = 0; i < _listPrefab.Count; i++)
        {
            bool isType = false;
            target = _listInstObj.Count;
            for (int j = 0; j < target; j++)
            {
                if (_listInstObj[j]._key.GetKey().Equals(_listPrefab[i]._key.GetKey()))
                {
                    isType = true;
                    break;
                }
            }

            if (isType == false)
            {
                _listInstObj.Add(new SetterKeyListData<TypeList, InstObj>(_listPrefab[i]._key, new List<InstObj>()));
            }
        }
        

        //добавление всех типов ключей из списка _listPrefab в список _listSetDataInstObj
        for (int i = 0; i < _listPrefab.Count; i++)
        {
            bool isType = false;
            target = _listSetDataInstObj.Count;
            for (int j = 0; j < target; j++)
            {
                if (_listSetDataInstObj[j]._key.GetKey().Equals(_listPrefab[i]._key.GetKey()))
                {
                    isType = true;
                    break;
                }
            }

            if (isType == false)
            {
                _listSetDataInstObj.Add(
                    new SetterKeyListData<TypeList, SetDataInObj>(_listPrefab[i]._key, new List<SetDataInObj>()));
            }
        }
    }
    
    
    
    /// <summary>
    /// Проверка элементов списка _listPrefab на Null, в случаае если Null будет
    /// считаться не пустой ключ. а что то еще, то класс наследник должен будет переопределить метод
    /// </summary>
    protected virtual bool ChekNullElementListPref(SetterPrefabData<TypeList,InstObj> element)
    {
        if (element._key.GetKey() == null)
        {
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Проверка элементов списка _listInstObj на Null, в случаае если Null будет
    /// считаться не пустой ключ. а что то еще, то класс наследник должен будет переопределить метод
    /// </summary>
    protected virtual bool ChekNullElementListInt(SetterKeyListData<TypeList,InstObj> element)
    {
        if (element._key.GetKey() == null)
        {
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Проверка элементов списка хранящегося внутри списка _listInstObj на Null, в случаае если Null будет
    /// считаться не пустой элемент списка. а что то еще, то класс наследник должен будет переопределить метод
    /// </summary>
    protected virtual bool ChekNullElementListIntElement(InstObj element)
    {
        if (element == null)
        {
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Проверка элементов списка _listSetDataInstObj на Null, в случаае если Null будет
    /// считаться не пустой ключ. а что то еще, то класс наследник должен будет переопределить метод
    /// </summary>
    protected virtual bool ChekNullElementListSetData(SetterKeyListData<TypeList,SetDataInObj> element)
    {
        if (element._key.GetKey() == null)
        {
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Проверка элементов списка хранящегося внутри списка _listSetDataInstObj на Null, в случаае если Null будет
    /// считаться не пустой элемент списка. а что то еще, то класс наследник должен будет переопределить метод
    /// </summary>
    protected virtual bool ChekNullElementListSetDataElement(SetDataInObj element)
    {
        if (element == null)
        {
            return true;
        }

        return false;
    }
}

public enum SetterTypeSet
{
    One,
    Full
}


[System.Serializable]
public class SetterPrefabData<Key,InstObj>
{
    
    [SerializeField]  
    public Transform _parent;
    [SerializeField] 
    public InstObj _prefab;
    [SerializeField] 
    public Key _key;
    
    [SerializeField]
    public SetterTypeSet setterType;
}

[System.Serializable]
public class SetterKeyListData<Key,InstObj>
{
    public SetterKeyListData()
    {
        
    }
    public SetterKeyListData(Key key, List<InstObj> instObj)
    {
        _key = key;
        _prefabs = instObj;
    }
    
    [SerializeField] 
    public Key _key;
    
    [SerializeField] 
    public List<InstObj>  _prefabs;
}
