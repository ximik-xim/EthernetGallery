using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertDataTuskInSetDataLogger : MonoBehaviour
{
    [SerializeField] 
    private FabricTaskUI fabricTaskUI;

    [SerializeField] 
    private SetDataDefaultLoggerElement defaultLoggerElement;
    private void Awake()
    {
        fabricTaskUI.OnCreateObject += CreateElement;
    }

    private void CreateElement(Transform element)
    {
        var obj = element.GetComponent<LoggerElementUI>();
        defaultLoggerElement.AddElementSetData(obj);
    }

    private void OnDestroy()
    {
        fabricTaskUI.OnCreateObject -= CreateElement;
    }
}
