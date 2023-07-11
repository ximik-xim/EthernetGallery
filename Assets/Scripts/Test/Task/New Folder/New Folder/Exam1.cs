using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exam1 : BankTaskDef<ItesTaskType<TElelementType>,TElelementType,TGetLIstType,TesStatType<TElelementType>>
{
    [SerializeField] 
    private TListType _listType;
    
    protected override int GetHashKey()
    {
        return _listType.GetHashCode();
    }

    private void Awake()
    {
        Debug.Log("INIT 1");
Init();
    }
}
