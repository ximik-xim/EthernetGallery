using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Отвечает за обновления UI у загрузчика данных
//нужно что бы всегда был включен
public class UIScneteLoad : MonoBehaviour
{
    [SerializeField] 
    private Image _loaderImage;
    [SerializeField] 
    private Text _loaderText;
    [SerializeField] 
    private GameObject _panelUI;


    [SerializeField] 
    private TestFabric _fabric;

    private Dictionary<int, NewControllUITe> _infoElement = new Dictionary<int, NewControllUITe>();
    private List<NewControllUITe> _buffer = new List<NewControllUITe>();
    private LoaderPacketInfo _infoLoad;

    /// <summary>
    /// Очистит UI и создаст нужное кол-во сообщений 
    /// </summary>
    public void UpdateInform(List<int> listHash)
    {
        CheckCountElement(listHash.Count);

        _infoElement = new Dictionary<int, NewControllUITe>();
        for (int i = 0; i < _infoLoad.CountElement; i++)
        {
            _infoElement.Add(listHash[i], _buffer[i]);
        }

        foreach (var VARIABLE in _infoElement.Values)
        {
            VARIABLE.Open();
        }
        
        
        for (int i = _infoElement.Count; i < _infoLoad.CountElement; i++)
        {
         
            _buffer[i].Close();
        }
    }
    
    /// Выключит UI 
    public void DisactivateUILoader(bool clear)
    {
        if (clear == true)
        {
            ClearUI();    
        }

        foreach (var VARIABLE in _buffer)
        {
            VARIABLE.Close();
        }
        
        _panelUI.gameObject.SetActive(false);
    }
    /// Включит UI 
   // public void ActiveUILoader(bool clear,List<LoaderStatuse> statuses )
    public void ActiveUILoader(bool clear)
    {
        _panelUI.gameObject.SetActive(true);
        
        if (clear == true) 
        {
            ClearUI();
        }
    }
    
    private void Start()
    {
        _fabric.OnCreateObject += CreateElement;
        
        _infoLoad = LoaderPacketInfo.PacketInfo;
        _infoLoad.OnUpdateElementStatuse += UpdateUiStatusElement;
        _infoLoad.OnUpdateGeneralStatuse += UpdateUiStatusGeneral;
    }
    
    private void UpdateUiStatusElement(LoaderStatuse arg1)
    {
        _infoElement[arg1.Hash].gameObject.SetActive(true);
        _infoElement[arg1.Hash].UpdateData(arg1);
    }
    
    private void UpdateUiStatusGeneral(LoaderStatuse arg1)
    {
        _loaderImage.fillAmount = arg1.Comlite;
        _loaderText.text = (arg1.Comlite * 100).ToString() + "%";
    }
    

    
    private void CheckCountElement(int targetCount)
    {
        if (targetCount > _infoElement.Count)
        {
            int difference = targetCount - _infoElement.Count;
            CreateElement(difference);
        }
    }
    
    
    private void CreateElement(int count)
    {
        _fabric.Create(count);
    }

    private void CreateElement(Transform element)
    {
        var obj = element.GetComponent<NewControllUITe>();
        _buffer.Add(obj);
    }
    
    private void ClearUI()
    {
        foreach (var VARIABLE in _buffer)
        {
            VARIABLE.ClearData();
        }

        _loaderImage.fillAmount = 0;

        _loaderText.text = "0%";
    }
}
