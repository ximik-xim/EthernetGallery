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
        LoaderPacketInfo.PacketInfo.AddLoadData(exampleLoadData);
        LoaderPacketInfo.PacketInfo.StartLoadResourse();
    }
}
