using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FabricDefault<T> : MonoBehaviour where T : MonoBehaviour
{
    //Срабатывает при создании экземпляра префаба
    public event Action<Transform> OnCreateObject;
    [SerializeField] 
    protected Transform _parent;
    [SerializeField] 
    protected T _prefab;
    
    protected event Action<T> _OnLocalCreateObject;
    
    
    //Создаст указанное кол-во экземпляров префаба
    public virtual void Create(int count,Action<T> onLocalCreateObject )
    {
        _OnLocalCreateObject += onLocalCreateObject;
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(_prefab, _parent);
            _OnLocalCreateObject?.Invoke(obj);
            OnCreateObject?.Invoke(obj.transform);
        }
        _OnLocalCreateObject -= onLocalCreateObject; 
    }
}
