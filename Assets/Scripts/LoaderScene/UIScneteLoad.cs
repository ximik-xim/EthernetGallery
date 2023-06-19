using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScneteLoad : MonoBehaviour
{
    [SerializeField]
    private LoaderElemUI prefab;
    [SerializeField] 
    private Transform _parent;
    private Dictionary<int, LoaderElemUI> lElems = new Dictionary<int, LoaderElemUI>();
    private List<LoaderElemUI> _buffer = new List<LoaderElemUI>();


    private LoaderPacketInfo _infoLoad;

    private void Start()
    {
        _infoLoad = LoaderPacketInfo.PacketInfo;

        //для теста
        SceneManager.LoadScene(1);
    }
    
    public void UpdateInform(List<int> listHash)
    {
        _infoLoad.OnUpdateElementStatuse += UpdateUiStatuse;

        if (_infoLoad.CountElement > lElems.Count)
        {
            int difference = _infoLoad.CountElement - lElems.Count;
            for (int i = 0; i < difference; i++)
            {
                var UIelement=   Instantiate(prefab,_parent);
                _buffer.Add(UIelement);
            }
        }

        lElems = new Dictionary<int, LoaderElemUI>();
        for (int i = 0; i < _infoLoad.CountElement; i++)
        {
            lElems.Add(listHash[i], _buffer[i]);
        }
        
        for (int i = lElems.Count; i < _infoLoad.CountElement; i++)
        {
            _buffer[i].gameObject.SetActive(false);
        }
    }

    private void UpdateUiStatuse(LoaderStatuse arg1)
    {
        lElems[arg1.Hash].updateUI(arg1);
    }
    
}
