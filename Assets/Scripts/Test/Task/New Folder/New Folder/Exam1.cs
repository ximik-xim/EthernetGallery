using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exam1 : BankTaskDef<ExTask,TElelementType,TGetLIstType,ExamStat>
{
    [SerializeField] 
    private TListType _listType;
    
    protected override int GetHashKey()
    {
        return _listType.GetHashCode();
    }

    private void Awake()
    {
                
Init();
    }
}
