using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfacTypeTaskAct<GetKey,Status>
{
    public Action<GetKey, Status> OnUpdateElementStatuseType { get;set; }
    


    public abstract Action<GetKey,Status> OnUpdateGeneralTypeStatus { get; set; }
}

