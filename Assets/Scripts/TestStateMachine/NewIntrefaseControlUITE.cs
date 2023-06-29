using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewIntrefaseControlUITE : MonoBehaviour
{

    public abstract void Open(bool clearData = false);

    public abstract void UpdateData(LoaderStatuse statuse);

    public abstract void ClearData();

    public abstract void Close();

}
