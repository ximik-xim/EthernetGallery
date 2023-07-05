using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FabricTList<Key,ListElement, Prefab > : MonoBehaviour where ListElement : IGetKey<Key> where Prefab : MonoBehaviour
{

    public event Action<Key, Transform> OnCreateObject;
    [SerializeField]
    protected List<FabricInstantiateData<Prefab,ListElement>> _list;
    protected Dictionary<Key, Prefab> _loggerElementUis;
    
    protected event Action<Key, Prefab> _OnLocalCreateObject;
    
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