using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleStartLoadDataType : MonoBehaviour
{
    [SerializeField] 
    private ExampleStartStateType exampleLoadData;
    
    protected TesterLoaderF _load;
    private void OnEnable()
    {
        _load = TesterLoaderF.statikLoad;
        
        
        _load.AddTaskType(exampleLoadData._typeKeyTask,exampleLoadData._HashTypeKey,exampleLoadData);
            
            
     
    }
}
