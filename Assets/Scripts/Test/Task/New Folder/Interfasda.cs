using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interfasda
{
    public void AddtTT(ILoaderTask taskType);
    public void RemoveTT(ILoaderTask taskType);

    public void StartLoad();
    public void StartLoadScene(int idScene, bool executeAfterLoading);
    
    public void ActiveUILoader(bool clear = false);
    public void DisactiveUiLoader();
}
