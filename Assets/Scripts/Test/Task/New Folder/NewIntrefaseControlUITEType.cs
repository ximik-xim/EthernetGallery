using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class NewIntrefaseControlUITEType<Key,Status> : NewIntrefaseControlUITE where Status : TesStatType<Key>
{
    //Пока не вижу в нем смысла, но пуска будет
    public abstract void UpdateDataTypeGeneral(Key key,Status  statuse);
    
}
