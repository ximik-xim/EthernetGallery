using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FabricType <Key,Prefab> : FabricTList<Key,FakT<Key>,Prefab> where Prefab : MonoBehaviour
{
    
}
