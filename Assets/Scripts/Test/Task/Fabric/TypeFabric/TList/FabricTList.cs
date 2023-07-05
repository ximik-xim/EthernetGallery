using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Key - ключ
/// ListElement - класс эелементов списка
/// Prefab - класс префаба
/// 
/// Создает экземпляры префаба
/// Этот класс нужен в случае, если ключ будет доставаться каким либо образом из элементов списка
/// В таком сулчае класс ListElement должен реализовывать интерфеис для получения ключа IGetKey
/// </summary>
public abstract class FabricTList<Key,ListElement, Prefab > : MonoBehaviour where ListElement : IGetKey<Key> where Prefab : MonoBehaviour
{

    public event Action<Key, Transform> OnCreateObject;
    [SerializeField]
    protected List<FabricInstantiateData<Prefab,ListElement>> _list;
    protected Dictionary<Key, Prefab> _loggerElementUis;
    
    protected event Action<Key, Prefab> _OnLocalCreateObject;
    
    /// <summary>
    ///Очистит списки от пустых элементов и заполнит словарь 
    /// </summary>
    protected void Init()
    {
     
            int target = _list.Count;
            for (int i = 0; i < target; i++)
            {
                if (ChekElementNull(_list[i]) == true)
                {
                    _list.Remove(_list[i]);
                    i--;
                    target--;
                }

            }

            _loggerElementUis = new Dictionary<Key, Prefab>();
            foreach (var VARIABLE in _list)
            {
                _loggerElementUis.Add(VARIABLE.key.GetKey(),VARIABLE.prefab);
            }
        
    }



    /// <summary>
    ///Создаст указанное кол-во экземпляров префаба по указ. типу, и оповещает с помощью CallBack об создании обьекта 
    /// </summary>
    public void Create(Key type, int count, Action<Key, Prefab> onLocalCreateObject = null)
    {
        _OnLocalCreateObject += onLocalCreateObject;
        for (int i = 0; i < count; i++)
        {

            foreach (var VARIABLE in _list)
            {
                if (VARIABLE.key.GetKey().Equals(type))
                {
                    var obj = Instantiate(_loggerElementUis[type], VARIABLE._parent);
                    _OnLocalCreateObject?.Invoke(type,obj);
                    OnCreateObject?.Invoke(type,obj.transform);
                }
            }
            
        }
        _OnLocalCreateObject -= onLocalCreateObject;
    }
    
    
    /// <summary>
    /// Проверка элементов на Null, в случаае если Null будет считаться не знач ключ. а что то еще, то класс наследник должен будет переопределить метод 
    /// </summary>
    protected virtual bool ChekElementNull(FabricInstantiateData<Prefab,ListElement> element)
    {
    
        if (element.key.GetKey() == null)
        {
            return true;
        }

        return false;
    }
}
[System.Serializable]
public class FabricInstantiateData<Prefab,Key>
{
    [SerializeField] 
    public Transform _parent;
    [SerializeField]
    public Prefab prefab;
    [SerializeField] 
    public Key key;
}