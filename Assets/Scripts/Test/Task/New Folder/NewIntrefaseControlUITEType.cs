using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Потом может отнаследую от обычного интерфеиса, добавив лишь UpdateData
public abstract class NewIntrefaseControlUITEType<Key,Status> : MonoBehaviour where Status : TesStatType<Key>
{
    public abstract void Open(bool clearData = false);

    public abstract void UpdateData(Key key,Status  statuse);

    public abstract void ClearData();

    public abstract void Close();
}
