using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamStat : TesStatType<TElelementType>
{
    public ExamStat(StatusLoad statuse, int hash, string name, float comlite, Start startInfo = null, Load loadInfo = null, Error errorInfo = null, Complite compliteInfo = null) : base(statuse, hash, name, comlite, startInfo, loadInfo, errorInfo, compliteInfo)
    {
    }


}
