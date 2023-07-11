using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertDataTuskInSetDataLoggerEx1 : MonoBehaviour
{
    [SerializeField]
    private FabricTList<TElelementType, TGetLIstType, TaskElementControllerUIType<TElelementType, ExamStat>> _fabricType;
    
    [SerializeField] 
    private ExampleSetterDataTypeTaskType defaultLoggerElement;
    //SetterDataTypeTList<TElelementType,TGetLIstType, LoggerPanel, LoggerElementUI >
    private void Awake()
    {
        _fabricType.OnCreateObject += CreateElement;
    }

    private void CreateElement(TElelementType arg1, Transform arg2)
    {
        var obj = arg2.GetComponent<LoggerElementUI>();
        defaultLoggerElement.AddElementSetData(arg1,obj);
    }



    private void OnDestroy()
    {
        _fabricType.OnCreateObject -= CreateElement;
    }
}
