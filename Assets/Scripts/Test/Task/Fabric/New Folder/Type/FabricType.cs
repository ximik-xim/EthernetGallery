using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FabricType <Key,Prefab> : MonoBehaviour where Prefab:MonoBehaviour
{

    public event Action<Key, Transform> OnCreateObject;
    [SerializeField]
    protected List<FabricInstantiateData<Prefab,Key>> _list;
    protected Dictionary<Key, Prefab> _loggerElementUis;

    protected event Action<Key, Prefab> _OnLocalCreateObject;
    
    protected void Init()
    {
        if (typeof(Key) is Enum == false)
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
                _loggerElementUis.Add(VARIABLE.key,VARIABLE.prefab);
            }
        }
    }


    public void Create(Key type, int count, Action<Key, Prefab> onLocalCreateObject = null)
    {

        _OnLocalCreateObject += onLocalCreateObject;
        for (int i = 0; i < count; i++)
        {

            foreach (var VARIABLE in _list)
            {
                if (VARIABLE.key.Equals(type))
                {
                    var obj = Instantiate(_loggerElementUis[type], VARIABLE._parent);
                    _OnLocalCreateObject?.Invoke(type,obj);
                    OnCreateObject?.Invoke(type,obj.transform);
                }
            }
            
        }
        _OnLocalCreateObject -= onLocalCreateObject;
    }

    protected virtual bool ChekElementNull(FabricInstantiateData<Prefab,Key> fabricInstantiateData)
    {
        if (fabricInstantiateData.key == null)
        {
            return true;
        }

        return false;
    }
}
