using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstrCal<GetKey,Status> : MonoBehaviour,InterfaceDefTaskAct,InterfacTypeTaskAct<GetKey,Status>
{
    public abstract Action<GetKey, Status> OnUpdateElementStatuseType { get; set; }
    public abstract Action<GetKey, Status> OnUpdateGeneralStatuseType { get; set;}
    
    public abstract Action<LoaderStatuse> OnUpdateElementStatuseDef { get; set;}
    public abstract Action<LoaderStatuse> OnUpdateGeneralStatuseDef { get; set;}




}
