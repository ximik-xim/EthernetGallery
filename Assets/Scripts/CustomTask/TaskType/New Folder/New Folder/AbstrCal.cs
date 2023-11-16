using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstrCal<GetKey,Status> : AbstractCalcalcal,InterfacTypeTaskAct<GetKey,Status>
{
    public abstract Action<GetKey, Status> OnUpdateElementStatuseType { get; set; }

    public abstract Action<GetKey,Status> OnUpdateGeneralTypeStatus { get; set; }



}
