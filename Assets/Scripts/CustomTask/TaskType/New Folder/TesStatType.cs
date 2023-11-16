using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class TesStatType<T> : LoaderStatuse,IGetKey<T>
{
    [SerializeField]
    private T _key;
    public  TesStatType(StatusLoad statuse, int hash, string name, float comlite, Start startInfo = null, Load loadInfo = null, Error errorInfo = null, Complite compliteInfo = null) : base(statuse, hash, name, comlite, startInfo, loadInfo, errorInfo, compliteInfo)
    {
    }

    public virtual void SetKey(T t)
    {
        _key = t;
    }

    public virtual T GetKey()
    {
        return _key;
    }
}
