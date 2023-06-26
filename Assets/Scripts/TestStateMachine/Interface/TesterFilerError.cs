using System;
using UnityEngine;

[RequireComponent(typeof(TestStatePanelSetData))]
public class TesterFilerError : MonoBehaviour,IFilterLogicDebug
{
    public string DataSuitable(LoaderStatuse statuse)
    {
        if (statuse.Statuse == LoaderStatuse.StatusLoad.Error)
        {
            if (statuse.ErrorInfo.Type == LoaderStatuse.Error.TypeError.Error)
            {
                if (statuse.ErrorInfo.Text != String.Empty)
                {
                    string text = "<color=yellow>" + statuse.ErrorInfo.Text + "</color>";
                    return text;    
                }
                
            }
        }

        return String.Empty;
    }
    
}
