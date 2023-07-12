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

    protected override TesStatType<TElelementType> LoadStatLoad(int hashTask,float comliteTask)
    {
        return new TesStatType<TElelementType>(LoaderStatuse.StatusLoad.Load, hashTask, "Загрузка Task Type", comliteTask);
    }

    protected override TesStatType<TElelementType> LoadStatComlite(int hashTask,float comliteTask)
    {
        return new TesStatType<TElelementType>(LoaderStatuse.StatusLoad.Complite, hashTask, "Загрузка Task Type", comliteTask);
    }
    

    private void Awake()
    {
        Init();
    }
}
