//Статус выполнения задачи 
public class LoaderStatuse
{
    public LoaderStatuse(StatusLoad statuse, int hash, string name, float comlite, Start startInfo = null, Load loadInfo = null, Error errorInfo = null, Complite compliteInfo = null)
    {
        Statuse = statuse;
        Hash = hash;
        Name = name;
        Comlite = comlite;
        StartInfo = startInfo;
        LoadInfo = loadInfo;
        ErrorInfo = errorInfo;
        CompliteInfo = compliteInfo;

        if (StartInfo == null)
        {
            startInfo = new Start("");
        }
        if (LoadInfo == null)
        {
            LoadInfo = new Load("");
        }
        if (ErrorInfo == null)
        {
            ErrorInfo = new Error(Error.TypeError.None,"");
        }
        if (CompliteInfo == null)
        {
            CompliteInfo = new Complite("");
        }
        
    }

    public Start StartInfo  {get; private set; }
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
    Start,
    Load,
    Error,
    Complite
    
}
    

public class  Start
{  
    public Start(string text)
    {
        _text = text;
    }
    public string Text
    {
        get => _text;
    }
    private string _text;
    

}
public class  Load
{  
    public Load(string text)
    {
        _text = text;
    }
    public string Text
    {
        get => _text;
    }
    private string _text;
    

}

public class  Error
{
    public Error(TypeError type, string text)
    {
        _type = type;
        _text = text;
    }

    public TypeError Type
    {
        get => _type;
    }
    
    public string Text
    {
        get => _text;
    }
    
    private TypeError _type;
    private string _text;
    
    public enum TypeError
    {
        None,
        Error,
        FatalError,
    }
}


public class  Complite
{
    public Complite(string text)
    {
        _text = text;
    }
    
    public string Text
    {
        get => _text;
    }
   
    private string _text;

}


}
