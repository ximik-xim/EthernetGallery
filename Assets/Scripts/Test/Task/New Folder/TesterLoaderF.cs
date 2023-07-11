using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterLoaderF : MonoBehaviour
{

    public static TesterLoaderF statikLoad;
    public static Action OnInit;

    private void Awake()
    {
        statikLoad = this;
        OnInit?.Invoke();
    }


    //(Type)type - тип ключа
    //(int) ref type - ссылка на экз ключа(нужен хэш) (в случ с enum нужно будет просто обор. в класс FacT ) ( В случ Tlist у обьектов ScrOjb хран. ключ знач будут разные хэши )
    //(Interfasda) - Метод для добавления Task
    /// <summary>
    /// я тут подумал, если буду исп. Tlist в качестве типа или буду исп всего лишь 1 Enum, то и экземпляров Bank тода будет всего лишь 1
    /// хотя нет, стоп. хэш ключа та будет разный (enum  будет упакован в класс и по этому хэши даже  у него будут разные, тут все окейы)
    /// </summary>
    private Dictionary<Type, Dictionary<int, Interfasda>> _dictionaryBank = new Dictionary<Type, Dictionary<int, Interfasda>>();

    public void AddBank(Type typeKey,int hashKey,Interfasda interfaceAdd)
    {
        if (_dictionaryBank.ContainsKey(typeKey) == false)
        {
            _dictionaryBank.Add(typeKey,new Dictionary<int, Interfasda>());
        }

        if (_dictionaryBank[typeKey].ContainsKey(hashKey) == false)
        {
            _dictionaryBank[typeKey].Add(hashKey,interfaceAdd);
            return;
        }
        
        Debug.LogError("Ошибка, экземпляр хранилища с Task с таким ключем уже установлен");
        
    }
    
    


    public void AddTaskType(Type typeKey,int hashKey, ILoaderTask task)
    {
        _dictionaryBank[typeKey][hashKey].AddtTT(task);
    }
    
}
