using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestPanelUI : NewIntrefaseControlUITE
{
    [SerializeField] 
    private Image _loaderImage;
    [SerializeField] 
    private Text _loaderText;
    [SerializeField] 
    private GameObject _panelUI;
    
    public override void Open(bool clearData = false)
    {
        _panelUI.gameObject.SetActive(true);
    }

    public override void UpdateData(LoaderStatuse statuse)
    {
        _loaderImage.fillAmount = statuse.Comlite;
        _loaderText.text = (statuse.Comlite * 100).ToString() + "%";
    }

    public override void ClearData()
    {
        _loaderImage.fillAmount = 0;
        _loaderText.text = "0%";
    }

    public override void Close()
    {
        _panelUI.gameObject.SetActive(false);
    }
}
