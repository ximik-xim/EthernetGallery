    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отвечает за обновление UI у задачи
/// </summary>
[RequireComponent(typeof(TaskElementControllerUIZero))]
public class TaskElemUI : NewIntrefaseControlUITE
{
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

    
    private TaskElementControllerUIZero _taskElementControllerUIZero;
    
    
    private void Start()
    {
        _taskElementControllerUIZero = GetComponent<TaskElementControllerUIZero>();
        _taskElementControllerUIZero.OnOpen += Open;
        _taskElementControllerUIZero.OnClose += Close;
        _taskElementControllerUIZero.OnClearData += ClearData;
        _taskElementControllerUIZero.OnUpdateStatuse += UpdateData;
        
        _errorImage.sprite = _errorImageSet;
        _errorImage.gameObject.SetActive(false);
    }

    
    /// <summary>
    /// Обновляет UI у задачи и записывает статусы
    /// </summary>
    public override void UpdateData(LoaderStatuse arg1)
    {
        _nameTask.text = arg1.Name;
        _loaderComplite.text = (arg1.Comlite * 100).ToString() + "%";
        _SliserImage.fillAmount = arg1.Comlite;

        UIUpdateIsStatus(arg1);
    }


    public override void Open(bool clearData = false)
    {
        _background.gameObject.SetActive(true);
    }



    /// <summary>
    /// Очищает список логов статусов
    /// </summary>
    public override void ClearData()
    {
        _errorImage.gameObject.SetActive(false);
        _nameTask.text = "";
        _loaderComplite.text = "0%";
        _SliserImage.fillAmount = 0;
        _loaderImage.sprite = null;
    }

    public override void Close()
    {
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

}
