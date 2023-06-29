using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отвечает за обновление UI у задачи
/// </summary>
public class LoaderElemUI : MonoBehaviour
{
    public IReadOnlyList<LoaderStatuse> Statuses => _listStatuse; 
    
    [SerializeField] 
    private Transform _background;
    
    [SerializeField] 
    private Text _nameTask;
    [SerializeField] 
    private Text _loaderComplite;

    [SerializeField] 
    private Image _loaderImage;
    [SerializeField] 
    private Image _SliserImage;
    [SerializeField]
    private Image _errorImage;
    
    [SerializeField] 
    private Sprite _loadImageSet;
    [SerializeField] 
    private Sprite _errorImageSet;
    [SerializeField] 
    private Sprite _fatalErrorImageSet;
    [SerializeField] 
    private Sprite _CompliteImageSet;

    private NewLogicPanel _panelInfoStatuseUI;
    private List<LoaderStatuse> _listStatuse = new List<LoaderStatuse>();
    private bool _select;
    
    
    private void Start()
    {
        _errorImage.sprite = _errorImageSet;
        _errorImage.gameObject.SetActive(false);
    }
    /// <summary>
    /// Указывает экземпляр панели, для передачи в нее всех статусов 
    /// </summary>
    public void SetPanelUI(NewLogicPanel panelInfoStatuseUI)
    {
        _panelInfoStatuseUI = panelInfoStatuseUI;
    }
    
    /// <summary>
    /// Обновляет UI у задачи и записывает статусы
    /// </summary>
    public void UpdateUI(LoaderStatuse arg1)
    {
        _nameTask.text = arg1.Name;
        _loaderComplite.text = (arg1.Comlite * 100).ToString() + "%";
        _SliserImage.fillAmount = arg1.Comlite;

        UIUpdateIsStatus(arg1);
        
        SetDataLogPanel(arg1);
    }
    
    /// <summary>
    /// Включает панель с логами от статусов и передает все статусы задачи
    /// </summary>
    public void OpenLogPanel()
    {
        _select = true;
        _panelInfoStatuseUI.OpenPanel(Statuses,true);
        _panelInfoStatuseUI.ClosePanel += CloseLogPanel;
    }
    
    /// <summary>
    /// Очищает список логов статусов
    /// </summary>
    public void ClearData()
    {
        _errorImage.gameObject.SetActive(false);
        _nameTask.text = "";
        _loaderComplite.text = "0%";
        _SliserImage.fillAmount = 0;
        _loaderImage.sprite = null;

        _listStatuse = new List<LoaderStatuse>();
        _panelInfoStatuseUI.ClearText();
    }

    public void DisactiveElement(bool clearData)
    {

        if (clearData == true)
        {
            ClearData();
        }
        
        _background.gameObject.SetActive(false);
        
        
    }
    private void UIUpdateIsStatus(LoaderStatuse arg1)
    {

        switch (arg1.Statuse)
        {
            case LoaderStatuse.StatusLoad.Start:
            {
                _loaderImage.sprite = _loadImageSet; 
            } break;
            
            case LoaderStatuse.StatusLoad.Load:
            {
                _loaderImage.sprite = _loadImageSet; 
            } break;
            
            case LoaderStatuse.StatusLoad.Error:
            {
                switch (arg1.ErrorInfo.Type)
                {
                    case LoaderStatuse.Error.TypeError.Error:
                    {
                        _errorImage.gameObject.SetActive(true);

                    } break;
                
                    case LoaderStatuse.Error.TypeError.FatalError:
                    {
                    
                        _loaderImage.sprite = _fatalErrorImageSet;
                    
                    } break;
                }
            } break;
            
            case LoaderStatuse.StatusLoad.Complite:
            {
                _loaderImage.sprite = _CompliteImageSet;
            } break;
        }
    }

    private void SetDataLogPanel(LoaderStatuse arg1)
    {
        _listStatuse.Add(arg1);
        if (_select == true)
        {
            if (_panelInfoStatuseUI.IsOpen)
            {
                _panelInfoStatuseUI.AddInfo(arg1);
            }    
        }
    }
    
    private void CloseLogPanel()
    {
        _select = false;
        _panelInfoStatuseUI.ClosePanel -= CloseLogPanel;
    }


}
