using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDataSetType<Key> : ParentDataSet,IGetKey<Key>
{
    public ParentDataSetType(Transform _parent,Key key) : base(_parent)
    {
        _key = key;
    }

    private Key _key;
    public Key GetKey()
    {
        return _key;
    }
}
