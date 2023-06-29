using System;
using UnityEngine;

//Отвечает за создание экземпляров префаба
public class PrototypeFabric : MonoBehaviour
{
    //Срабатывает при создании экземпляра префаба
    public event Action<Transform> OnCreateObject;
    
    [SerializeField] 
    private Transform _prefab;

    [SerializeField] 
    private Transform _parent;
    
    
    
    //Создаст указанное кол-во экземпляров префаба
    public void Create(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(_prefab, _parent);
            OnCreateObject?.Invoke(obj);
        }
    }
}
