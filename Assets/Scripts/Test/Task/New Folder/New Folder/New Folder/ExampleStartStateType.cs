using System.Collections;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class ExampleStartStateType : MonoBehaviour, ItesTaskType<TElelementType>
{
    [SerializeField] 
    private TListType _listType;
    [SerializeField] 
    private TGetLIstType key;

    private int _hash => this.GetHashCode();
    private string _name = "Искуственная задержка";
    
    public int LoaderHash
    {
        get => _hash;
    }
    public string LoaderName { get=>_name; }
    public event Action<LoaderStatuse> OnStatus;


    public TElelementType GetKey()
    {
        return key.GetElement();
    }

    public int _HashTypeKey { get=>_listType.GetHashCode(); }
    public Type _typeKeyTask { get=>typeof(TElelementType); }


    private void Awake()
    {
        
        _listType.GetElementName(key);
        Debug.Log("Set Key = "+key.GetKey());
        
        TesterLoaderF.statikLoad.AddTaskType(_typeKeyTask,_HashTypeKey,this);

        
        
        
        TesterLoaderF.statikLoad.StartLoadBank(_typeKeyTask, _HashTypeKey);
    }
    
    public void StartLoad()
    {
//        StartTimer();
        StartCoroutine(StatTimer());
    }

    //аппасно тут делать асинхроность, т.к если будет какая либо ошибка она не выведеться в консоль
    public async Task StartTimer()
    {
        int time = 300;
        
        await Task.Delay(time);
        var status = new ExamStat(LoaderStatuse.StatusLoad.Start, _hash, _name, 1f / 10, new LoaderStatuse.Start("Старт задержки"));
        status.SetKey(key.GetKey());
        OnStatus?.Invoke(status);
 
        for (int i = 0; i < 8; i++)
        {
            await Task.Delay(time);
            status = new ExamStat(LoaderStatuse.StatusLoad.Load, _hash, _name, 1f / 10 * (i + 2),null,new LoaderStatuse.Load("Ожидание задержки"));
            status.SetKey(key.GetKey());
            OnStatus?.Invoke(status);
        }
        
        await Task.Delay(time);
        status = new ExamStat(LoaderStatuse.StatusLoad.Complite, _hash, _name, 1f,null,null,null,new LoaderStatuse.Complite("Ожидание задержки завершено") );
        status.SetKey(key.GetKey());
        OnStatus?.Invoke(status);
    }

    private IEnumerator StatTimer()
    {
        var status = new ExamStat(LoaderStatuse.StatusLoad.Start, _hash, _name, 1f / 10, new LoaderStatuse.Start("Старт задержки"));
        status.SetKey(key.GetKey());
        OnStatus?.Invoke(status);
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(1000 / 300);
            status = new ExamStat(LoaderStatuse.StatusLoad.Load, _hash, _name, 1f / 10 * (i + 2), null, new LoaderStatuse.Load("Ожидание задержки"));
            status.SetKey(key.GetKey());
            OnStatus?.Invoke(status);
        }
        
        yield return new WaitForSeconds(1000 / 300);
        status = new ExamStat(LoaderStatuse.StatusLoad.Complite, _hash, _name, 1f, null, null, null, new LoaderStatuse.Complite("Ожидание задержки завершено"));
        status.SetKey(key.GetKey());
        OnStatus?.Invoke(status);
    }
}
