using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Пример добавления сообщения в UI задач
/// </summary>
public class ExampleAddUnplannedStatus : MonoBehaviour
{
    private void Awake()
    {
        List<LoaderStatuse> list = new List<LoaderStatuse>();
        list.Add(new LoaderStatuse(LoaderStatuse.StatusLoad.Load,3442,"Example unplanned status",0));
        LoaderPacketInfo.PacketInfo.ActiveUILoader(false, list);

    }
    
    
    
    
}
