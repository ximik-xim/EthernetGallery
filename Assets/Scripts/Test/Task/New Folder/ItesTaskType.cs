using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItesTaskType<T> : ILoaderTask,IGetKey<T>
{
    //Пока тупо знач. хэша ключа, потом может и сылку на ключ сделаю
    public int _HashTypeKey { get; }
}
