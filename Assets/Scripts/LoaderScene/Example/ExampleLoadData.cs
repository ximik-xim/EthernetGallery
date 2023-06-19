using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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
        StartTimer();
    }

    //аппасно тут делать асинхроность, т.к если будет какая либо ошибка она не выведеться в консоль
    public async Task StartTimer()
    {
        int time = 300;
        
        for (int i = 0; i < 9; i++)
        {
            await Task.Delay(time);
            OnStatus?.Invoke(new LoaderStatuse(LoaderStatuse.Type.Load, _hash, _name, 1f / 10 * (i + 1)));
        }
        
        await Task.Delay(time);
        OnStatus?.Invoke(new LoaderStatuse(LoaderStatuse.Type.Complite, _hash, _name, 1f));
    }
}
