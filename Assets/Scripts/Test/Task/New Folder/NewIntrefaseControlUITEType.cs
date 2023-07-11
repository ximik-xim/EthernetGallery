using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class NewIntrefaseControlUITEType<Key,Status> : NewIntrefaseControlUITE where Status : TesStatType<Key>
{
    public abstract void UpdateData(Key key,Status  statuse);
    
}
