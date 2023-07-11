using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExTask : MonoBehaviour, ItesTaskType<TElelementType>
{

    [SerializeField] 
    private TListType _listType;
    [SerializeField] 
    private TGetLIstType key;

    public int LoaderHash { get=>this.GetHashCode(); }
    public string LoaderName { get; }
    public event Action<LoaderStatuse> OnStatus;
    public void StartLoad()
    {
        throw new NotImplementedException();
    }

    public TElelementType GetKey()
    {
        return key.GetElement();
    }

    public int _HashTypeKey { get=>_listType.GetHashCode(); }
    public Type _typeKeyTask { get=>typeof(TElelementType); }


    private void Start()
    {
        
        _listType.GetElementName(key);
        
        TesterLoaderF.statikLoad.AddTaskType(typeof(TElelementType),_HashTypeKey,this);
    }
}
