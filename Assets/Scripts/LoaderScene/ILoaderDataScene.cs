using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Интерфеис задачи для выполнения
public interface ILoaderDataScene 
{

    //Вернет хэш экземпляра класса
    public int LoaderHash{ get; }
    
    //Вернет имя задачи
    public string LoaderName { get;  }

    //Вернет статуст задачи при изменении
    public event Action<LoaderStatuse> OnStatus;

    //Вызовиться когда будет вызов всех задач
    public void StartLoad();

}
