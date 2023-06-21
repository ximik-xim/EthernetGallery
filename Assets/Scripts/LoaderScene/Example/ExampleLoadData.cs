using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

//Пример самой задачи
public class ExampleLoadData : MonoBehaviour, ILoaderDataScene
{
    public int LoaderHash
    {
        get => _hash;
    }

    public string LoaderName
    {
        get => _name;
    }

    public event Action<LoaderStatuse> OnStatus;

    private int _hash => this.GetHashCode();
    private string _name = "Искуственная задержка";
    
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
        OnStatus?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Start, _hash, _name, 1f / 10,new LoaderStatuse.Start("Старт задержки")));
 
        for (int i = 0; i < 8; i++)
        {
            await Task.Delay(time);
            OnStatus?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Load, _hash, _name, 1f / 10 * (i + 2),null,new LoaderStatuse.Load("Ожидание задержки")));
        }
        
        await Task.Delay(time);
        OnStatus?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Complite, _hash, _name, 1f,null,null,null,new LoaderStatuse.Complite("Ожидание задержки завершено") ));
    }

    private IEnumerator StatTimer()
    {
        OnStatus?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Start, _hash, _name, 1f / 10,new LoaderStatuse.Start("Старт задержки")));
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(1000 / 300);
            OnStatus?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Load, _hash, _name, 1f / 10 * (i + 2),null,new LoaderStatuse.Load("Ожидание задержки")));
        }
        
        yield return new WaitForSeconds(1000 / 300);
        OnStatus?.Invoke(new LoaderStatuse(LoaderStatuse.StatusLoad.Complite, _hash, _name, 1f,null,null,null,new LoaderStatuse.Complite("Ожидание задержки завершено") ));
    }
}
