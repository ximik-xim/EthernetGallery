using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertDataTuskInSetDataLogger : MonoBehaviour
{
    [SerializeField] 
    private PrototypeFabric _fabric;

    [SerializeField] 
    private SetDataLoggerElement _loggerElement;
    private void Awake()
    {
        _fabric.OnCreateObject += CreateElement;
    }

    private void CreateElement(Transform element)
    {
        var obj = element.GetComponent<LoggerElementUI>();
        _loggerElement.AddElementSetData(obj);
    }

    private void OnDestroy()
    {
        _fabric.OnCreateObject -= CreateElement;
    }
}
