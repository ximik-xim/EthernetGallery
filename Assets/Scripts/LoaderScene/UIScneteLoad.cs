using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Отвечает за обновления UI у загрузчика данных
//нужно что бы всегда был включен
public class UIScneteLoad : MonoBehaviour
{
    public event Action<LoaderStatuse> testStat;
    
    
    [SerializeField]
    private LoaderElemUI _prefab;
    [SerializeField] 
    private Transform _parentInfoElement;
    [SerializeField] 
    private Image _loaderImage;
    [SerializeField] 
    private Text _loaderText;
    [SerializeField] 
    private GameObject _panelUI;
  
  
  
    [SerializeField] 
    private LoaderPanelInfoStatuseUI _panelInfoStatuseUI;
    
    private Dictionary<int, LoaderElemUI> _infoElement = new Dictionary<int, LoaderElemUI>();
    private List<LoaderElemUI> _buffer = new List<LoaderElemUI>();
    private LoaderPacketInfo _infoLoad;

    /// <summary>
    /// Очистит UI и создаст нужное кол-во сообщений 
    /// </summary>
    public void UpdateInform(List<int> listHash)
    {
        CheckCountElement(listHash.Count);

        _infoElement = new Dictionary<int, LoaderElemUI>();
        for (int i = 0; i < _infoLoad.CountElement; i++)
        {
            _infoElement.Add(listHash[i], _buffer[i]);
        }
        
        for (int i = _infoElement.Count; i < _infoLoad.CountElement; i++)
        {
         
            _buffer[i].DisactiveElement(false);
           // _buffer[i].gameObject.SetActive(false);
        }
    }
    
    /// Выключит UI 
    public void DisactivateUILoader(bool clear)
    {
        if (clear == false)
        {
            _panelUI.gameObject.SetActive(false);
            _panelInfoStatuseUI.ClosPanel();
            return;
        }

        ClearUI();

        _panelUI.gameObject.SetActive(false);
        _panelInfoStatuseUI.ClosPanel();
    }
    /// Включит UI 
    public void ActiveUILoader(bool clear,List<LoaderStatuse> statuses )
    {
        _panelUI.gameObject.SetActive(true);
        
        if (clear == true) 
        {
            ClearUI();
        }

        if (statuses != null)
        {
            for (int i = 0; i < statuses.Count; i++)
            {
                if (_infoElement.ContainsKey(statuses[i].Hash) == false)
                {
                    continue;
                }
                
                _infoElement[statuses[i].Hash].gameObject.SetActive(true);
                _infoElement[statuses[i].Hash].UpdateUI(statuses[i]);
                statuses.Remove(statuses[i]);
            }

            if (statuses.Count > 0)
            {
                CreateElement(statuses.Count);

                for (int i = 0; i < statuses.Count; i++)
                {
                    _infoElement.Add(statuses[i].Hash, _buffer[_buffer.Count - statuses.Count + i]);
                    _infoElement[statuses[i].Hash].gameObject.SetActive(true);
                    _infoElement[statuses[i].Hash].UpdateUI(statuses[i]);
                }
                
            }
            
        }

    }
    
    private void Start()
    {
        _infoLoad = LoaderPacketInfo.PacketInfo;
        _infoLoad.OnUpdateElementStatuse += UpdateUiStatusElement;
        _infoLoad.OnUpdateGeneralStatuse += UpdateUiStatusGeneral;
    }
    
    private void UpdateUiStatusElement(LoaderStatuse arg1)
    {
        _infoElement[arg1.Hash].gameObject.SetActive(true);
        _infoElement[arg1.Hash].UpdateUI(arg1);
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
        for (int i = 0; i < count; i++)
        {
            var UIelement=   Instantiate(_prefab,_parentInfoElement);
            UIelement.SetPanelUI(_panelInfoStatuseUI);
            _buffer.Add(UIelement);
        }
    }
    
    private void ClearUI()
    {
        foreach (var VARIABLE in _buffer)
        {
            //VARIABLE.gameObject.SetActive(false);
            VARIABLE.ClearData();
        }

        _loaderImage.fillAmount = 0;

        _loaderText.text = "0%";
    }
}
