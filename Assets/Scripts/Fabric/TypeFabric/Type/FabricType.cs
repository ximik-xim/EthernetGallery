using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Key - ключ
/// Prefab - класс префаба
/// 
/// Создает экземпляры префаба
/// Этот класс нужен в случае, если ключ(Key) указываеться на прямую
/// Используеться обертка в нутрь класса FakT
/// </summary>
public abstract class FabricType <Key,Prefab> : FabricTList<Key,FakT<Key>,Prefab> where Prefab : MonoBehaviour
{
    
}
