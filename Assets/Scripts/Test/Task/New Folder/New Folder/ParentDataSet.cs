using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDataSet
{
    public ParentDataSet(Transform parent)
    {
        _parent = parent;
    }

    protected Transform _parent;
    public Transform Parent => _parent;


}
