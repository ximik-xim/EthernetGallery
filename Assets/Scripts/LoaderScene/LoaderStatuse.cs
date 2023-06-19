using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderStatuse
{
    public LoaderStatuse(Type statuse, int hash, string name, float comlite, Load loadInfo=null, Error errorInfo=null, Complite compliteInfo = null)
    {
        Statuse = statuse;
        Hash = hash;
        Name = name;
        Comlite = comlite;
        LoadInfo = loadInfo;
        ErrorInfo = errorInfo;
        CompliteInfo = compliteInfo;

    }

    public Load LoadInfo{ get; private set; }
    public Error ErrorInfo{ get; private set; }
    public Complite CompliteInfo{ get; private set; }
    
    public Type Statuse{ get; private set; }
    public int Hash{ get; private set; }

    public string Name{ get; private set; }

    public float Comlite{ get; private set; }
public enum Type
{
    Load,
    Error,
    Complite
    
}
    


public class  Load
{
    
}

public class  Error
{
    
}


public class  Complite
{
    
}


}
