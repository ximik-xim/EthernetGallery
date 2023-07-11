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
}
