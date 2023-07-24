using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TypeGetPoint
{
    None,
    SavePivot,
    Pivot,
    SaveObjectPosition,
    ObjectPosition
}
public class PointRotation : AbsPointRotation
{
    [SerializeField]
    private TypeGetPoint _type;

    [SerializeField] 
    private Transform _objectPos;
    private Vector2 _savePos;
    private RectTransform _rectTrans;

    private void Awake()
    {
        _rectTrans = GetComponent<RectTransform>();
        switch (_type)
        {
            case TypeGetPoint.SavePivot:
            {
                _savePos = _rectTrans.pivot * _rectTrans.rect.size;
            } 
                break;
    
            case TypeGetPoint.SaveObjectPosition:
            {
                _savePos = _objectPos.position;
            } 
                break;
        }
    }

    public override Vector2 GetPoint()
    {
        switch (_type)
        {
            case TypeGetPoint.SavePivot:
            {
                return _savePos;
            } 
                break;
            case TypeGetPoint.Pivot:
            {
                var pos = _rectTrans.pivot * _rectTrans.rect.size;
                return pos;
            } 
                break;
            case TypeGetPoint.SaveObjectPosition:
            {
                return _savePos;
            } 
                break;
            case TypeGetPoint.ObjectPosition:
            {
                return _objectPos.position;
            } 
                break;
        }

        Debug.LogError("Ошибка, тип возращаемой точки не задан");
        return new Vector2(0,0);
    }
}
