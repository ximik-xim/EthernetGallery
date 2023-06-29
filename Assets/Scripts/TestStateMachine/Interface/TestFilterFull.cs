using System;
using UnityEngine;

[RequireComponent(typeof(TestStatePanelSetData))]
public class TestFilterFull : MonoBehaviour,IFilterLogicDebug
{
    public string DataSuitable(LoaderStatuse statuse)
    {
        switch (statuse.Statuse)
        {
            case LoaderStatuse.StatusLoad.Start: 
            {
                if (statuse.StartInfo.Text != String.Empty)
                {
                    string text = "<color=blue>" + statuse.StartInfo.Text + "</color>";
                    return text;    
                }
            }
                break;
            
            case LoaderStatuse.StatusLoad.Load: 
            {
                if (statuse.LoadInfo.Text != String.Empty)
                {
                    string text = "<color=white>" + statuse.LoadInfo.Text + "</color>";
                    return text;    
                }
            }
                break;
            
            case LoaderStatuse.StatusLoad.Error: 
            {
                
                if (statuse.ErrorInfo.Type == LoaderStatuse.Error.TypeError.Error)
                {
                    if (statuse.ErrorInfo.Text != String.Empty)
                    {
                        string text = "<color=yellow>" + statuse.ErrorInfo.Text + "</color>";
                        return text;    
                    }
                
                }
                
                if (statuse.ErrorInfo.Type == LoaderStatuse.Error.TypeError.FatalError)
                {
                    if (statuse.ErrorInfo.Text != String.Empty)
                    {
                        string text = "<color=red>" + statuse.ErrorInfo.Text + "</color>";
                        return text;    
                    }
                }
                
            }
                break;
            
            case LoaderStatuse.StatusLoad.Complite: 
            {
                if (statuse.CompliteInfo.Text != String.Empty)
                {
                    string text = "<color=green>" + statuse.CompliteInfo.Text + "</color>";
                    return text;    
                }
            } 
                break;
        }
        
        return String.Empty;
    }
}
