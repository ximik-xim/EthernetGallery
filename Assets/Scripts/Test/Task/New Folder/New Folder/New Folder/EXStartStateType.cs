using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXStartStateType : MonoBehaviour
{
    [SerializeField] 
    private TListType _listType;
    
    private void Start()
    {
        
        TesterLoaderF.statikLoad.StartLoadBank(typeof(TElelementType) , _listType.GetHashCode());
    }
}
