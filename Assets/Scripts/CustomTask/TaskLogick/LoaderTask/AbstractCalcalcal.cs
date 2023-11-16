using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCalcalcal : MonoBehaviour,InterfaceDefTaskAct
{
    public abstract Action<LoaderStatuse> OnUpdateElementStatuseDef { get; set;}
    public abstract Action<LoaderStatuse> OnUpdateGeneralStatuseDef { get; set;}
}
