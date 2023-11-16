using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FakT<Key>: IGetKey<Key>
{
    [SerializeField]
    private Key _key;

    public Key Value => _key;
    public Key GetKey()
    {
        return _key;
    }
}