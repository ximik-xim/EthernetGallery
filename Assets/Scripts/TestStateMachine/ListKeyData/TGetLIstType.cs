using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TGetLIstType 
{
    public string Name => _name;
    [SerializeField] 
    private string _name = "none";

    
    private TElelementType _type;
    public void SetData(TElelementType type)
    {
        _type = type;
    }

    public TElelementType GetElement()
    {
        return _type;
    }

}
