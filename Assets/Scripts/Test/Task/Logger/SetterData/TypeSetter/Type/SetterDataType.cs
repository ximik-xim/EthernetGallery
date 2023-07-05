using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetterDataType <Key,InstObj, SetDataInObj> : SetterDataTypeTList<Key,FakT<Key>,InstObj,SetDataInObj> where InstObj : MonoBehaviour where SetDataInObj : ISetterData<InstObj> 
{

}
