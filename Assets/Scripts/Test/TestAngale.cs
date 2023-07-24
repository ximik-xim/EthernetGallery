using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAngale : MonoBehaviour
{
    private RectTransform _rectTransform;
    private void Awake()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        

        
        
    }

    private void Update()
    {
        
        /*
        Vector3[] vector3 = new Vector3 [4];
        //Вернет координаты якорей(именно координаты сразу, всех 4 якорей)
        _rectTransform.GetLocalCorners(vector3);
        
        Debug.Log("1"+ vector3[0]);
        Debug.Log("1"+ vector3[1]);
        Debug.Log("2"+ vector3[2]);
        Debug.Log("3"+ vector3[3]);
        
        //Блять этот метод тупо установит размер окна в высоту = 10
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,10);
        */

        //До конца не понял, но установит якоря в один из 4 позиций, установит выпсоту относ якоря и что то еще (но вроде отступ) 
        //_rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 900, 100);
        
        Debug.Log( _rectTransform.rect.height);
        
    }
}
