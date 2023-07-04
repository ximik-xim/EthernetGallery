using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetterDataType <Key,InstObj, SetDataInObj> : MonoBehaviour where SetDataInObj : ISetterData<InstObj> where InstObj : MonoBehaviour 
{
    [Header("Spawn")]
[SerializeField]
    protected List<TesterrrrrData<Key,InstObj>> _listPrefab;
//Список нужен пока только для отображения
[SerializeField]
    protected List<TesterrrrrDataЕЕЕ<Key,InstObj>> _listInstObj;
    protected Dictionary<Key,List<InstObj> > _inst;

    [Header("Insert")]
    [SerializeField]
    protected    List<TesterrrrrDataЕЕЕ<Key,SetDataInObj>> _listSetDataInstObj;
    protected Dictionary<Key, List<SetDataInObj>> _setData;

    


    protected virtual void Init()
    {
        CheckNull();
        
        InsertDataDictinary();

       
        
///////////////////////////////// Создание экземпляра

        foreach (var VARIABLE in _listPrefab)
        {
            if (VARIABLE._type == TypeFsfdfasdas.One)
            {
                if (_inst[VARIABLE._key].Count==0)
                {
                    var obj = Instantiate(VARIABLE._prefab, VARIABLE._parent);
                    _inst[VARIABLE._key].Add(obj);
                }
            }
            
        }

        
       
        
        

    }
    
    //Засовывание данных всем элементом в списке для уст. данных
    public virtual void SetDataLenghtList()
    {
        foreach (var VARIABLE in _listPrefab)
        {
            if (VARIABLE._type == TypeFsfdfasdas.One)
            {
                foreach (var VARIABLE2 in _setData[VARIABLE._key])
                {
                    VARIABLE2.SetData(_inst[VARIABLE._key][0]);
                }
            }
            
            
            if (VARIABLE._type == TypeFsfdfasdas.Full)
            {
                foreach (var VARIABLE2 in _setData[VARIABLE._key])
                {
                    int tar = _setData[VARIABLE._key].Count;
                    tar -= _inst[VARIABLE._key].Count;

                    for (int i = 0; i < tar; i++)
                    {
                        var obj = Instantiate(VARIABLE._prefab, VARIABLE._parent);
                        _inst[VARIABLE._key].Add(obj);
                    }

                    tar = _setData[VARIABLE._key].Count;
                    for (int i = 0; i < tar; i++)
                    {
                        var data = _inst[VARIABLE._key][i];
                        _setData[VARIABLE._key][i].SetData(data);
                        
                        // тут с отобр. подумать
                    }
                    
                }
            }
        }
    }

    protected virtual void InsertDataDictinary()
    {
        ///////////////////////////////////
        _inst = new Dictionary<Key, List<InstObj>>();
        foreach (var VARIABLE in _listInstObj)
        {
            _inst.Add(VARIABLE._key,VARIABLE._prefab);
        }

        _setData = new Dictionary<Key, List<SetDataInObj>>();
        foreach (var VARIABLE in _listSetDataInstObj)
        {
            _setData.Add(VARIABLE._key,VARIABLE._prefab);
        }
    }
    
    public virtual void AddElementSetData(Key key, SetDataInObj SetDataInObj )
    {
        foreach (var VARIABLE in _listPrefab)
        {
            if (VARIABLE._key.Equals(key))
            {
                if (VARIABLE._type == TypeFsfdfasdas.One)
                {
                    Debug.Log(SetDataInObj == null);
                    Debug.Log(_setData==null);
                    Debug.Log(_setData.ContainsKey(key));
                    _setData[key].Add(SetDataInObj);
                   
                    SetDataInObj.SetData(_inst[key][0]);
                    // тут с отобр. подумать
                }


                if (VARIABLE._type == TypeFsfdfasdas.Full)
                {
                    var obj = Instantiate(VARIABLE._prefab, VARIABLE._parent);
                   
                    _inst[key].Add(obj);
                    _setData[key].Add(SetDataInObj);
                    
                    SetDataInObj.SetData(obj);
                    // тут с отобр. подумать
                }
                
            }
            
        }
        
    }
    //Проверка на пустые значения и заполнения списков по ключам
    protected void CheckNull()
    {
        
        //Очистка пустых элементов списка с префабами
        int target = _listPrefab.Count;
        for (int i = 0; i < target; i++)
        {
            if (_listPrefab[i]._key == null)
            {
                _listPrefab.Remove(_listPrefab[i]);

                i--;
                target--;
            }
        }

        //Очистка пустых элементов списка созданных экземпляров
        target = _listInstObj.Count;
        for (int i = 0; i < target; i++)
        {
            if (_listInstObj[i]._key == null)
            {
                _listInstObj.Remove(_listInstObj[i]);

                i--;
                target--;
            }
        }


        //Очистка пустых элементов внутреннего списка в списка созданных экземпляров
        for (int i = 0; i < _listInstObj.Count; i++)
        {
            target = _listInstObj[i]._prefab.Count;
            for (int j = 0; j < target; j++)
            {
                if (_listInstObj[i]._prefab[j] == null)
                {
                    _listInstObj[i]._prefab.Remove(_listInstObj[i]._prefab[j]);

                    j--;
                    target--;
                }
            }
        }
        

        //Очистка пустых элементов списка элементов для засовывания в них данных
        target = _listSetDataInstObj.Count;
        for (int i = 0; i < target; i++)
        {
            if (_listSetDataInstObj[i]._key == null)
            {
                _listSetDataInstObj.Remove(_listSetDataInstObj[i]);

                i--;
                target--;
            }
        }


        //Очистка пустых элементов внутреннего списка в списка элементов для засовывания в них данных
        for (int i = 0; i < _listSetDataInstObj.Count; i++)
        {
            target = _listSetDataInstObj[i]._prefab.Count;
            for (int j = 0; j < target; j++)
            {
                if (_listSetDataInstObj[i]._prefab[j] == null)
                {
                    _listSetDataInstObj[i]._prefab.Remove(_listSetDataInstObj[i]._prefab[j]);

                    j--;
                    target--;
                }
            }
        }
        

        for (int i = 0; i < _listPrefab.Count; i++)
        {
            bool isType = false;
            target = _listInstObj.Count;
            for (int j = 0; j < target; j++)
            {
                if (_listInstObj[j]._key.Equals(_listPrefab[i]._key))
                {
                    isType = true;
                    break;
                }
            }

            if (isType == false)
            {
                _listInstObj.Add(new TesterrrrrDataЕЕЕ<Key, InstObj>(_listPrefab[i]._key, new List<InstObj>()));
            }
        }
        

        for (int i = 0; i < _listPrefab.Count; i++)
        {
            bool isType = false;
            target = _listSetDataInstObj.Count;
            for (int j = 0; j < target; j++)
            {
                if (_listSetDataInstObj[j]._key.Equals(_listPrefab[i]._key))
                {
                    isType = true;
                    break;
                }
            }

            if (isType == false)
            {
                _listSetDataInstObj.Add(
                    new TesterrrrrDataЕЕЕ<Key, SetDataInObj>(_listPrefab[i]._key, new List<SetDataInObj>()));
            }
        }
    }
}

