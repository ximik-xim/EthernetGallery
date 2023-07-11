using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskElementControllerUIType<Key,Status> : TaskElementControllerUIZero where Status : TesStatType<Key>
{
    public event Action<Key,Status> OnUpdateStatuse;



    public void UpdateData(Key key,Status  statuse)
    {
        OnUpdateStatuse?.Invoke(key, statuse);
    }
    
}
