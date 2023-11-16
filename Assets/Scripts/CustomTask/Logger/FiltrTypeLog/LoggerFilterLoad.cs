using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(StateInsertFilterInLogger))]
public class LoggerFilterLoad : MonoBehaviour,IFilterLogicDebug
{
    public string DataSuitable(LoaderStatuse statuse)
    {
        if (statuse.Statuse == LoaderStatuse.StatusLoad.Load)
        {
                if (statuse.LoadInfo.Text != String.Empty)
                {
                    string text = "<color=white>" + statuse.LoadInfo.Text + "</color>";
                    return text;    
                }
        }

        return String.Empty;
    }
}