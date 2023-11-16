using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskElementControllerUIType<Key,Status> : TaskElementControllerUIZero where Status : TesStatType<Key>
{
    public event Action<Key,Status> OnUpdateStatuseElement;
    public event Action<Key,Status> OnUpdateStatusTypeGeneral;

    public void UpdateStatuseElement(Key key,Status  statuse)
    {
        OnUpdateStatuseElement?.Invoke(key, statuse);
    }
    
    public void UpdateStatusTypeGeneral(Key key,Status  statuse)
    {
        OnUpdateStatusTypeGeneral?.Invoke(key, statuse);
    }
    
}
