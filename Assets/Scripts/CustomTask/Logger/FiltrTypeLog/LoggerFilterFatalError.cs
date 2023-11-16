using System;
using UnityEngine;
[RequireComponent(typeof(StateInsertFilterInLogger))]
public class LoggerFilterFatalError : MonoBehaviour,IFilterLogicDebug
{
    public string DataSuitable(LoaderStatuse statuse)
    {
        if (statuse.Statuse == LoaderStatuse.StatusLoad.Error)
        {
            if (statuse.ErrorInfo.Type == LoaderStatuse.Error.TypeError.FatalError)
            {
                if (statuse.ErrorInfo.Text != String.Empty)
                {
                    string text = "<color=red>" + statuse.ErrorInfo.Text + "</color>";
                    return text;    
                }
                
            }
        }

        return String.Empty;
    }
}
