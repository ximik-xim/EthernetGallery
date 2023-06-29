using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TestFabric : MonoBehaviour
{
    [SerializeField] 
    private Transform _prefab;

    [SerializeField] 
    private Transform _parent;
    
    public event Action<Transform> OnCreateObject;
    
    public void Create(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(_prefab, _parent);
            OnCreateObject?.Invoke(obj);

        }
        
        
        
    }
}
