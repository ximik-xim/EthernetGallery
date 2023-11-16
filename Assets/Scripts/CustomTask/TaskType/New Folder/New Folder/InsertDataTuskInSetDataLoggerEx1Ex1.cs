using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertDataTuskInSetDataLoggerEx1Ex1<Key,ElementList, InstObj,SetDataInObj> : MonoBehaviour where ElementList : IGetKey<Key>  where InstObj : MonoBehaviour where SetDataInObj : ISetterData<InstObj>
{

    
    [SerializeField]
    private FabricInterActType<Key, Transform> _fabricType;
    
    [SerializeField] 
    private  SetterDataTypeTList<Key,ElementList, InstObj, SetDataInObj> defaultLoggerElement;
   
    private void Awake()
    {
        _fabricType.OnCreateObjectType += CreateElement;
    }

    private void CreateElement(Key arg1, Transform arg2)
    {
        var obj = arg2.GetComponent<SetDataInObj>();
        defaultLoggerElement.AddElementSetData(arg1,obj);
    }
    
    private void OnDestroy()
    {
        _fabricType.OnCreateObjectType -= CreateElement;
    }
}
