using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Явно что то упускаю, т.к могу в качестве ключа указать TElelementType, но при этом Task нужен будет Enum
/// </summary>


public class Exam2Enum :  BankTaskDef<ExTask2,adflajf,FakT<adflajf>,ExStat2>
{
    private void Awake()
    {
        Init();
    }

    protected override int GetHashKey()
    {
        return typeof(adflajf).GetHashCode();
    }

    protected override ExStat2 LoadStatLoad(int hashTask, float comliteTask)
    {
        return new ExStat2(LoaderStatuse.StatusLoad.Load, hashTask, "Загрузка Task Type", comliteTask);
    }

    protected override ExStat2 LoadStatComlite(int hashTask, float comliteTask)
    {
        return new ExStat2(LoaderStatuse.StatusLoad.Complite, hashTask, "Загрузка Task Type", comliteTask);
    }
    
}
