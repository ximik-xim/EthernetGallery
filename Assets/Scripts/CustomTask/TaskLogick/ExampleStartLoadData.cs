using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Пример запуска задач
public class ExampleStartLoadData : MonoBehaviour
{
    [SerializeField] 
    private ExampleLoadData exampleLoadData;
    private void OnEnable()
    {
        LoaderTask.Task.AddLoadData(exampleLoadData);
        LoaderTask.Task.StartLoadResourse();
    }
}
