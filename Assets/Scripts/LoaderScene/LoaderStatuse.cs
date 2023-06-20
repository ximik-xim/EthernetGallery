//Статус выполнения задачи 
public class LoaderStatuse
{
    public LoaderStatuse(StatusLoad statuse, int hash, string name, float comlite, Load loadInfo=null, Error errorInfo=null, Complite compliteInfo = null)
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
    //Вернет статус задачи
    public StatusLoad Statuse{ get; private set; }
    //Вернет хэш код экземпляра класса
    public int Hash{ get; private set; }
    //Вернет имя задачи
    public string Name{ get; private set; }
    //Вернет процент загрузки в нормализованном виде (от 0 до 1)
    public float Comlite{ get; private set; }
    
public enum StatusLoad
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
