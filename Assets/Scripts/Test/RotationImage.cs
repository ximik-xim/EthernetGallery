using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationImage : MonoBehaviour
{
    [SerializeField] 
    private AbsPointRotation _pointRotation;
    [SerializeField] 
    private TypeSetData _typeSetData;
    [SerializeField]
    private List<RotateImage> _list;
    
    private Dictionary<ScreenOrientation, RotateImage> _data=new Dictionary<ScreenOrientation, RotateImage>();

    //для теста
    private RectTransform _transform;
    private RectTransform _transformParent;
    private Vector2 _scaleTransform;

    private Vector2 ParentSize;
    
    private Vector2 PortraitMaxAnchXY;
    private Vector2 PortraitMinAnchXY;
    
    //тек. отступы
    private float PortraitTop;
    private float PortraitBottom;
    
    private float PortraitLeft;
    private float PortraitRight;

    private Vector3 PortraitRotation;
    
    //Отступы после поворота
    private float PortraitRotTop;
    private float PortraitRotBottom;
    
    private float PortraitRotLeft;
    private float PortraitRotRight;
    
    //Значение отступа для расчета верхней и нижней точки
    private float PortraitRotTopX;
    private float PortraitRotBottomX;

    private float PortraitRotLeftX;
    private float PortraitRotRightX;
    
    //отступы родителя
    private float ParentPortraitTop;
    private float ParentPortraitBottom;
    
    private float ParentPortraitLeft;
    private float ParentPortraitRight;
    
    
    [SerializeField] 
    private TypeCurrentOrint _currentOrint; 
    
[SerializeField]
    private Transform testHeig;
    [SerializeField]
    private Transform testBotton;
    
    [SerializeField]
    private Transform testHeigAnc;
    [SerializeField] 
    private Transform testBottonAnc;
    
    
public enum TypeSetData
{
    Screen,
    SaveCurrentSize,
    CurrentSize
}

public enum TypePer
{
    Width,
    Height
    
}

public enum TypeCurrentOrint
{
    Portrait,
    PortraitUpsideDown,
    LandscapeRight,
    LandscapeLeft
}

private void Awake()
{
    _transform = GetComponent<RectTransform>();

    switch (_currentOrint)
    {

        case TypeCurrentOrint.Portrait:
        {
            PortraitMaxAnchXY = _transform.anchorMax;
            PortraitMinAnchXY = _transform.anchorMin;
         
            PortraitTop = _transform.offsetMax.y;
            PortraitBottom = _transform.offsetMin.y;
            
            PortraitRight = _transform.offsetMax.x;
            PortraitLeft = _transform.offsetMin.x;
            
            
            


           
        }
            break;
    }

    PortraitRotation = _transform.rotation.eulerAngles;

    
    
    
    var par= transform.parent.GetComponent<RectTransform>();
    _transformParent = par;
    ParentSize = par.rect.size;

    var Tes = _transform.rect.height - _transform.rect.width;
    var Tes2 = Tes / 2;
 
 
    Debug.Log("Tes = "+ Tes);

    PortraitRotLeft = PortraitLeft - Tes2;
    PortraitRotRight = PortraitRight + Tes2;
    Debug.Log("PortraitRotRight = " + PortraitRight + " - (" + _transform.rect.height + " - " + _transform.rect.width + ") / " + 2 + " = " + PortraitRotRight);
    
    
    PortraitRotTop = PortraitTop - Tes2;
    PortraitRotBottom = PortraitBottom + Tes2;

    //Не уверен что тут 0, но пока будет так
    PortraitRotBottomX = (0 - PortraitRotRight) - _transformParent.rect.height * _transform.anchorMin.y + (1 - _transform.anchorMax.x) * _transformParent.rect.width;
    PortraitRotTopX = (_transformParent.rect.width - PortraitRotLeft) - _transformParent.rect.height * _transform.anchorMax.y - _transform.anchorMin.x * _transformParent.rect.width;

    PortraitRotRightX = PortraitRotTop - _transformParent.rect.width * _transform.anchorMax.x + _transformParent.rect.height * _transform.anchorMax.y;
    PortraitRotLeftX = PortraitRotBottom - _transformParent.rect.width * _transform.anchorMin.x + _transformParent.rect.height * _transform.anchorMin.y;
    
    Debug.Log("PortraitRotBottomX = " + PortraitRotBottomX);
    Debug.Log("PortraitRotTopX = " + PortraitRotTopX);
    Debug.Log("PortraitRotBottomX = " + " (0 "+ " - "+ PortraitRotRight+") - " +_transformParent.rect.height+" * " +_transform.anchorMin.y+" + " + " (1"+ " - " +_transform.anchorMax.x+") * "+ _transformParent.rect.width + " = " +PortraitRotBottomX) ;
    
    Debug.Log("PortraitRotRightX = " + PortraitRotRightX);
    Debug.Log("PortraitRotRightX = " + PortraitRotTop + " - " + _transformParent.rect.width + " * " + _transform.anchorMax.x + " + " + _transformParent.rect.height + " * " + _transform.anchorMax.y + " = "+ PortraitRotRightX);
    Debug.Log("PortraitRotLeftX = " + PortraitRotLeftX);
    
    //_transform.offsetMax = new Vector2(PortraitRotRight, PortraitRotTop);
    //_transform.offsetMin = new Vector2(PortraitRotLeft, PortraitRotBottom);
    
    Debug.Log("PortraitRotLeft = " + PortraitRotLeft);
    Debug.Log("PortraitRotRight = " + PortraitRotRight);
    
    Debug.Log("PortraitRotTop = "+PortraitRotTop);
    Debug.Log("PortraitRotBottom = "+PortraitRotBottom);



    ParentPortraitTop = _transformParent.offsetMax.y;
    ParentPortraitBottom = _transformParent.offsetMin.y;

    ParentPortraitRight = _transformParent.offsetMax.x;
    ParentPortraitLeft = _transformParent.offsetMin.x;
   
    
    

    foreach (var VARIABLE in _list)
    {
        _data.Add(VARIABLE._orientation,VARIABLE);
    }
    
    if (PositionDisplayedImage.PositionDisplayedImageStat == null)
    {
        PositionDisplayedImage.OnInit += initDis;
        return;
    } 
    
    PositionDisplayedImage.PositionDisplayedImageStat.OnRotateDisplayedImage += UpdateDis;
    UpdateDis(PositionDisplayedImage.PositionDisplayedImageStat.CurrentPositionDisplayedImage);

}

private void initDis()
{
    PositionDisplayedImage.OnInit -= initDis;

    PositionDisplayedImage.PositionDisplayedImageStat.OnRotateDisplayedImage += UpdateDis;

    UpdateDis(PositionDisplayedImage.PositionDisplayedImageStat.CurrentPositionDisplayedImage);



}

private void UpdateDis(ScreenOrientation orientation)
{

    if (_data.ContainsKey(orientation))
    {
        switch (orientation)
        {
             case ScreenOrientation.Portrait:
             {
                 _transformParent.offsetMax = new Vector2(ParentPortraitRight,  ParentPortraitTop);
                 _transformParent.offsetMin = new Vector2(ParentPortraitLeft, ParentPortraitBottom);
               
                 _transform.anchorMax = PortraitMaxAnchXY;
                 _transform.anchorMin = PortraitMinAnchXY;
                 
                 _transform.offsetMax = new Vector2(PortraitRight, PortraitTop);
                 _transform.offsetMin = new Vector2(PortraitLeft, PortraitBottom);
                 
                 
                 
                 var bb = _transformParent.rect.height * _transform.anchorMin.y +PortraitBottom;
                 testBottonAnc.position = new Vector3(testBottonAnc.position.x, bb , testBottonAnc.position.z);
                 Debug.Log("bb = " + _transformParent.rect.height + " * " + _transform.anchorMin.y + " + " + PortraitBottom + " = " + bb);
                 
                 
                 _transform.rotation = Quaternion.Euler(PortraitRotation);
             }
                break;
               
            case ScreenOrientation.LandscapeRight:
            {

               // _transformParent.offsetMax = new Vector2(ParentPortraitTop, 0 - ParentPortraitLeft);
               // _transformParent.offsetMin = new Vector2(ParentPortraitBottom, 0 - ParentPortraitRight);
                
                
                _transform.anchorMax = new Vector2(PortraitMaxAnchXY.y,1-PortraitMinAnchXY.x) ;
                _transform.anchorMin = new Vector2(PortraitMinAnchXY.y,1-PortraitMaxAnchXY.x) ;
            
       
            
                //Для расчета без переворота элементов
                // _transform.offsetMax = new Vector2( PortraitTop, 0 - PortraitLeft);
                // _transform.offsetMin = new Vector2(PortraitBottom, 0 - PortraitRight);




                _transform.offsetMax = new Vector2(PortraitRotTop , 0 - PortraitRotLeft - ParentPortraitTop / 2 + ParentPortraitBottom / 2);
                _transform.offsetMin = new Vector2(PortraitRotBottom, 0 - PortraitRotRight + ParentPortraitTop / 2 - ParentPortraitBottom / 2);


                //_transform.offsetMax = new Vector2(PortraitRotTop -ParentPortraitLeft/2+ParentPortraitRight/2 , 0 - PortraitRotLeft - ParentPortraitTop / 2 + ParentPortraitBottom / 2);
                //_transform.offsetMin = new Vector2(PortraitRotBottom+ParentPortraitLeft/2-ParentPortraitRight/2, 0 - PortraitRotRight + ParentPortraitTop / 2 - ParentPortraitBottom / 2);



                var Top = _transformParent.offsetMax.y;
                var Bot = _transformParent.offsetMin.y;
                
                var Rig = _transformParent.offsetMax.x;
                var Lef= _transformParent.offsetMin.x;
                Debug.Log("Top = " + Top);
                Debug.Log("Bot = " + Bot);

                Debug.Log("Lef = " + Lef);
                Debug.Log("Rig = " + Rig);


                var b = (_transformParent.rect.width + Top - Bot + Lef - Rig) * _transform.anchorMin.x + PortraitRotBottomX - Rig;
                testBottonAnc.position = new Vector3(testBottonAnc.position.x,b , testBottonAnc.position.z);
//-638.5f
                var h = (_transformParent.rect.width + Top - Bot + Lef - Rig) * _transform.anchorMax.x + PortraitRotTopX - Rig; 
                testHeigAnc.position = new Vector3(testHeigAnc.position.x, h, testHeigAnc.position.z);

                Debug.Log("b = " + _transformParent.rect.width + " * " + _transform.anchorMin.x + " + " + PortraitRotBottomX + " = " + b);
                Debug.Log("h = " + _transformParent.rect.width + " * " + _transform.anchorMax.x + " + " + PortraitRotTopX + " = " + h);
                
                Debug.Log("_transformParent.rect.width = "+ _transformParent.rect.width);



                var rightX = (_transformParent.rect.height - Top + Bot - Lef + Rig) * _transform.anchorMax.y + PortraitRotRightX + Bot; 
                var leftX = (_transformParent.rect.height -Top+Bot-Lef+Rig ) * _transform.anchorMin.y + PortraitRotLeftX + Bot; 
                
                
                testBotton.position = new Vector3( rightX, testBottonAnc.position.y, testBottonAnc.position.z);
                testHeig.position = new Vector3( leftX, testBottonAnc.position.y, testBottonAnc.position.z);
                Debug.Log("rightX = "  + _transformParent.rect.height  +  " * "+ _transform.anchorMax.y + " + " + PortraitRotRightX + " + " + Lef / 2 + " + " + Rig / 2 + " = " + rightX);

                //_transform.rotation = Quaternion.Euler(PortraitRotation + _data[ScreenOrientation.LandscapeRight]._rotation);

            }
                break;
            
            case ScreenOrientation.LandscapeLeft:
            {
                _transform.anchorMax = new Vector2(1-PortraitMinAnchXY .y ,PortraitMaxAnchXY.x) ;
                _transform.anchorMin = new Vector2(1 - PortraitMaxAnchXY.y, PortraitMinAnchXY.x);

                
                 // _transform.offsetMax = new Vector2(0-PortraitBottom,PortraitRight  );
                 // _transform.offsetMin = new Vector2(0-PortraitTop,PortraitLeft);
                 
                 
                 _transform.offsetMax = new Vector2(0-PortraitRotBottom, PortraitRotRight);
                 _transform.offsetMin = new Vector2(0-PortraitRotTop, PortraitRotLeft);

                 _transform.rotation = Quaternion.Euler(PortraitRotation + _data[ScreenOrientation.LandscapeLeft]._rotation);
            }
                break;
       
            case ScreenOrientation.PortraitUpsideDown:
            {
                 _transform.anchorMax = new Vector2(1-PortraitMinAnchXY .x,1-PortraitMinAnchXY.y) ;
                 _transform.anchorMin = new Vector2(1-PortraitMaxAnchXY.x,1-PortraitMaxAnchXY.y) ;
                
                 
                  _transform.offsetMax = new Vector2(  0-PortraitLeft,0-PortraitBottom);
                  _transform.offsetMin = new Vector2(0-PortraitRight,0-PortraitTop);

                 _transform.rotation = Quaternion.Euler(PortraitRotation + _data[ScreenOrientation.PortraitUpsideDown]._rotation);
            }
                break;
        }
        
        
    }
    
}

private void Update()
{
    // Debug.Log("1Max "+_transform.rect.center);
    // Debug.Log("2Max "+_transform.rect.height);
    // Debug.Log("3Max "+_transform.rect.max);
    // Debug.Log("4Max "+_transform.rect.position);
    // Debug.Log("5Max "+_transform.rect.size);
    // Debug.Log("6Max "+_transform.rect.y);
    // Debug.Log("7Max "+_transform.rect.yMax);
    // Debug.Log("8Max "+_transform.rect.yMin);
    //
    //
    // Debug.Log("9Max "+_transform.pivot);
    // Debug.Log("10Max "+_transform.anchoredPosition);
    // Debug.Log("11Max "+_transform.anchorMax);
    // Debug.Log("12Max "+_transform.anchorMin);
    //Debug.Log("13Max "+_transform.offsetMax);
    //Debug.Log("14Max "+_transform.offsetMin);
    // Debug.Log("14Max "+_transform.offsetMax);
    // Debug.Log("15Max "+_transform.sizeDelta);
    // Debug.Log("16Max "+_transform.forward);
    // Debug.Log("17Max "+_transform.root.name);
    // Debug.Log("18Max "+_transform.lossyScale);
    



   // var pivotPosNormal = _transform.pivot;
   // var pivotPos = pivotPosNormal * _transform.rect.size;

  
}



}
[System.Serializable]
public class RotateImage
{
    
    [SerializeField]
    public ScreenOrientation _orientation;
    [SerializeField] 
    public Vector3 _rotation;
    [SerializeField]
    public RotationImage.TypePer TypeSetWidth;
    [SerializeField]
    public RotationImage.TypePer TypeSetHeight;

}
