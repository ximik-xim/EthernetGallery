using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
 [SerializeField] 
 private Transform _parentUI;

 [SerializeField] 
 private TListType _listType;

 [SerializeField] private Transform _parentTypeUI;
 [SerializeField]
 private TGetLIstType _getLIstType;
 

 private void Start()
 {
  TesterLoaderF.statikLoad.AddParentUITask(typeof(TElelementType),_listType.GetHashCode(),new ParentDataSet(_parentUI));
  _listType.GetElementName(_getLIstType);
  
  TesterLoaderF.statikLoad.AddParentUITaskTypeTT(typeof(TElelementType),_listType.GetHashCode(),new ParentDataSetType<TElelementType>(_parentTypeUI,_getLIstType.GetElement()));
  
 }
}
