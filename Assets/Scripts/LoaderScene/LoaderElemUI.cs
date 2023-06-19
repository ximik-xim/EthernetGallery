using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderElemUI : MonoBehaviour
{
    [SerializeField] 
    private Text _text;
public void updateUI(LoaderStatuse arg1)
{
    _text.text = arg1.Name + " = " + arg1.Comlite;
    Debug.Log(arg1.Name + " = " + arg1.Comlite);
}
}
