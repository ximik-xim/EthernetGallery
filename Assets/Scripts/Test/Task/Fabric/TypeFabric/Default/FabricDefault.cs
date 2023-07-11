using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// T - класс префаба
/// Создает экземпляры префаба
/// </summary>
public abstract class FabricDefault<T> : FabricInterActDef where T : MonoBehaviour
{
    /// <summary>
    ///Срабатывает при создании экземпляра префаба 
    /// </summary>

    [SerializeField] 
    protected Transform _parent;
    [SerializeField] 
    protected T _prefab;
    /// <summary>
    /// Тоже срабатывает при создании экземпляра префаба, нужен в качесте CallBack
    /// </summary>
    protected event Action<T> _OnLocalCreateObject;
    
    
    
    //Создаст указанное кол-во экземпляров префаба, и оповещает с помощью CallBack об создании обьекта
    public virtual void Create(int count, Action<T> onLocalCreateObject = null)
    {
        _OnLocalCreateObject += onLocalCreateObject;
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(_prefab, _parent);
            _OnLocalCreateObject?.Invoke(obj);
            InvokeOnCreateObject(obj.transform);
        }

        _OnLocalCreateObject -= onLocalCreateObject;
    }
}
