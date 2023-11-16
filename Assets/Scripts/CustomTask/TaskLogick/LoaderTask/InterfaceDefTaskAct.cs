using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  InterfaceDefTaskAct
{
  public Action<LoaderStatuse> OnUpdateElementStatuseDef { get;set; }
  public Action<LoaderStatuse> OnUpdateGeneralStatuseDef { get;set; }
  
}
