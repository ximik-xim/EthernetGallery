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



    private float kofAncParentPortraitTop;
    private float kofAncParentPortraitBot;
    private float kofAncParentPortraitLef;
    private float kofAncParentPortraitRig;
    
    private float _difference;
    
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
    
    
    
    
    
    PortraitRotTop = PortraitTop - Tes2;
    PortraitRotBottom = PortraitBottom + Tes2;

    Debug.Log("PortraitRotLeft = " + PortraitRotLeft);
    Debug.Log("PortraitRotRight = " + PortraitRotRight);
    
        Debug.Log("PortraitRotLeft = " + _transform.offsetMin.x +" - " + Tes2);
        Debug.Log("PortraitRotRight = " + _transform.offsetMax.x + " + " + Tes2);
    
    Debug.Log("PortraitRotTop = "+PortraitRotTop);
    Debug.Log("PortraitRotBottom = "+PortraitRotBottom);

    Debug.Log("PortraitRotTop = " + _transform.offsetMax.y + " - " + Tes2);
    Debug.Log("PortraitRotBottom = "+_transform.offsetMin.y + " + " + Tes2);
    
    
    
    //Не уверен что тут 0, но пока будет так
    PortraitRotBottomX = (0 - PortraitRotRight) - _transformParent.rect.height * _transform.anchorMin.y + (1 - _transform.anchorMax.x) * _transformParent.rect.width;
    PortraitRotTopX = (_transformParent.rect.width - PortraitRotLeft) - _transformParent.rect.height * _transform.anchorMax.y - _transform.anchorMin.x * _transformParent.rect.width;

    //PortraitRotRightX = PortraitRotTop - _transformParent.rect.width * _transform.anchorMax.x + _transformParent.rect.height * _transform.anchorMax.y;
    //PortraitRotLeftX = PortraitRotBottom - _transformParent.rect.width * _transform.anchorMin.x + _transformParent.rect.height * _transform.anchorMin.y;


    PortraitRotRightX = PortraitRotTop - _transformParent.rect.width * (1 - _transform.anchorMin.x) + _transformParent.rect.height * _transform.anchorMax.y;
    PortraitRotLeftX = PortraitRotBottom - _transformParent.rect.width * (1 - _transform.anchorMax.x) + _transformParent.rect.height * _transform.anchorMin.y;

    Debug.Log("PortraitRotBottomX = " + PortraitRotBottomX);
    Debug.Log("PortraitRotTopX = " + PortraitRotTopX);
    Debug.Log("PortraitRotBottomX = " + " (0 "+ " - "+ PortraitRotRight+") - " +_transformParent.rect.height+" * " +_transform.anchorMin.y+" + " + " (1"+ " - " +_transform.anchorMax.x+") * "+ _transformParent.rect.width + " = " +PortraitRotBottomX) ;
    
    Debug.Log("PortraitRotRightX = " + PortraitRotRightX);
    Debug.Log("PortraitRotRightX = " + PortraitRotTop + " - " + _transformParent.rect.width + " * " + _transform.anchorMin.x + " + " + _transformParent.rect.height + " * " + _transform.anchorMax.y + " = "+ PortraitRotRightX);
    Debug.Log("PortraitRotLeftX = " + PortraitRotLeftX);
    Debug.Log("PortraitRotLeftX = " + PortraitRotBottom + " - " + _transformParent.rect.width + " * " + _transform.anchorMax.x + " + " + _transformParent.rect.height + " * " + _transform.anchorMin.y + " = " + PortraitRotLeftX);
    
    
    //_transform.offsetMax = new Vector2(PortraitRotRight, PortraitRotTop);
    //_transform.offsetMin = new Vector2(PortraitRotLeft, PortraitRotBottom);
    




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
                 
                 
                 
                 // var bb = _transformParent.rect.height * _transform.anchorMin.y +PortraitBottom;
                 // testBottonAnc.position = new Vector3(testBottonAnc.position.x, bb , testBottonAnc.position.z);
                 // Debug.Log("bb = " + _transformParent.rect.height + " * " + _transform.anchorMin.y + " + " + PortraitBottom + " = " + bb);
                 
                 
                 _transform.rotation = Quaternion.Euler(PortraitRotation);
             }
                break;
               
            case ScreenOrientation.LandscapeRight:
            {

               // _transformParent.offsetMax = new Vector2(ParentPortraitTop, 0 - ParentPortraitLeft);
               // _transformParent.offsetMin = new Vector2(ParentPortraitBottom, 0 - ParentPortraitRight);
                
               
               
       
                
               // _transform.anchorMax = new Vector2(PortraitMaxAnchXY.y,1-PortraitMinAnchXY.x) ;
               /// _transform.anchorMin = new Vector2(PortraitMinAnchXY.y,1-PortraitMaxAnchXY.x) ;
            
       
            
                //Для расчета без переворота элементов
                // _transform.offsetMax = new Vector2( PortraitTop, 0 - PortraitLeft);
                // _transform.offsetMin = new Vector2(PortraitBottom, 0 - PortraitRight);







                //_transform.offsetMax = new Vector2(PortraitRotTop -ParentPortraitLeft/2+ParentPortraitRight/2 , 0 - PortraitRotLeft - ParentPortraitTop / 2 + ParentPortraitBottom / 2);
                //_transform.offsetMin = new Vector2(PortraitRotBottom+ParentPortraitLeft/2-ParentPortraitRight/2, 0 - PortraitRotRight + ParentPortraitTop / 2 - ParentPortraitBottom / 2);


                
                
                
                //_transform.offsetMax = new Vector2(PortraitRotTop , 0 - PortraitRotLeft - ParentPortraitTop / 2 + ParentPortraitBottom / 2);
                //_transform.offsetMin = new Vector2(PortraitRotBottom, 0 - PortraitRotRight + ParentPortraitTop / 2 - ParentPortraitBottom / 2);
                //Debug.Log("Cur Top = " + "0 - " + PortraitRotLeft + " - " + ParentPortraitTop + " / 2 + " + ParentPortraitBottom + " / 2" + " = " + _transform.offsetMax.y);
    //Debug.Log("Cur Bot = " + _transform.offsetMin.y);
    //Debug.Log("Cur Rig = " + _transform.offsetMax.x);
    //Debug.Log("Cur Lef = " + _transform.offsetMin.x);
                
                
                var Top = _transformParent.offsetMax.y;
                var Bot = _transformParent.offsetMin.y;
                
                var Rig = _transformParent.offsetMax.x;
                var Lef= _transformParent.offsetMin.x;
                Debug.Log("Top = " + Top);
                Debug.Log("Bot = " + Bot);

                Debug.Log("Lef = " + Lef);
                Debug.Log("Rig = " + Rig);
                
                
                
                /*
                var b = (_transformParent.rect.width + Top - Bot + Lef - Rig) * _transform.anchorMin.x + PortraitRotBottomX - Rig;
                testBottonAnc.position = new Vector3(testBottonAnc.position.x,b , testBottonAnc.position.z);
                
                var h = (_transformParent.rect.width + Top - Bot + Lef - Rig) * _transform.anchorMax.x + PortraitRotTopX - Rig; 
                testHeigAnc.position = new Vector3(testHeigAnc.position.x, h, testHeigAnc.position.z);

                Debug.Log("b = " + _transformParent.rect.width + " * " + _transform.anchorMin.x + " + " + PortraitRotBottomX + " = " + b);
                Debug.Log("h = " + _transformParent.rect.width + " * " + _transform.anchorMax.x + " + " + PortraitRotTopX + " = " + h);
                
                Debug.Log("_transformParent.rect.width = "+ _transformParent.rect.width);



                var rightX = (_transformParent.rect.height - Top + Bot - Lef + Rig) * _transform.anchorMax.y + PortraitRotRightX + Bot;
                var leftX = (_transformParent.rect.height - Top + Bot - Lef + Rig) * _transform.anchorMin.y + PortraitRotLeftX + Bot; 
                
                testBotton.position = new Vector3( rightX, testBottonAnc.position.y, testBottonAnc.position.z);
                testHeig.position = new Vector3( leftX, testBottonAnc.position.y, testBottonAnc.position.z);
                Debug.Log("rightX = "  + (_transformParent.rect.height - Top + Bot - Lef + Rig)  +  " * "+ _transform.anchorMax.y + " + " + PortraitRotRightX +  " = " + rightX);
                Debug.Log("leftX = "  + (_transformParent.rect.height - Top + Bot - Lef + Rig)  +  " * "+ _transform.anchorMin.y + " + " + PortraitRotLeftX +  " = " + leftX);
                    */
                
                
                
                
                
                
                
                
                
/*
                var ancBot = (_transformParent.rect.height - Top) / _transformParent.rect.height - (PortraitMaxAnchXY.x * (_transformParent.rect.height - Top + Bot)) / _transformParent.rect.height;
                var  ancTop= (_transformParent.rect.height - Top + Bot - Bot) / _transformParent.rect.height - (PortraitMinAnchXY.x * (_transformParent.rect.height - Top + Bot)) / _transformParent.rect.height;
                
               var ancTop2 = (-Lef + Rig) / _transformParent.rect.height + ((Lef - Rig) * PortraitMinAnchXY.x - Rig) / _transformParent.rect.height;
               var  ancBot2=(-Lef+ Rig) / _transformParent.rect.height + ((Lef - Rig) * PortraitMaxAnchXY.x - Rig) / _transformParent.rect.height;
               
               
               ancTop += ancTop2;
               ancBot += ancBot2;
               
               
               
               var ancLef = (_transformParent.rect.width - (_transformParent.rect.width - Rig + Lef) - Rig) / _transformParent.rect.width + PortraitMinAnchXY.y * ((_transformParent.rect.width - Rig + Lef) / _transformParent.rect.width);
               var ancRig = ( _transformParent.rect.width - (_transformParent.rect.width  + Lef) ) / _transformParent.rect.width + PortraitMaxAnchXY.y * ((_transformParent.rect.width - Rig + Lef) / _transformParent.rect.width);
               
                
                 var ancLef2 = ((+ Top - Bot) * PortraitMinAnchXY .y + Bot) / _transformParent.rect.width;
                 var ancRig2 = (( + Top - Bot) * PortraitMaxAnchXY.y + Bot) / _transformParent.rect.width;
                 
                
                ancRig += ancRig2;
                ancLef += ancLef2;
                
                
                
                _transform.anchorMax = new Vector2(ancRig, ancTop ) ;
                _transform.anchorMin = new Vector2( ancLef  ,ancBot) ;
                
                
                
                _transform.offsetMax = new Vector2(PortraitRotTop,0-PortraitRotLeft);
                _transform.offsetMin = new Vector2(PortraitRotBottom,0-PortraitRotRight);
                
                
                */
                
                
                var  ancTop= (_transformParent.rect.height - Top + Bot - Bot) / _transformParent.rect.height - (_transform.anchorMin.x * (_transformParent.rect.height - Top + Bot)) / _transformParent.rect.height;
                var ancBot = (_transformParent.rect.height - Top) / _transformParent.rect.height - (_transform.anchorMax.x * (_transformParent.rect.height - Top + Bot)) / _transformParent.rect.height;
                
                
               var ancTop2 = (-Lef + Rig) / _transformParent.rect.height + ((Lef - Rig) * _transform.anchorMin.x - Rig) / _transformParent.rect.height;
               var  ancBot2=(-Lef+ Rig) / _transformParent.rect.height + ((Lef - Rig) * _transform.anchorMax.x - Rig) / _transformParent.rect.height;

               kofAncParentPortraitTop = ancTop2;
               kofAncParentPortraitBot = ancBot2;
               
               Debug.Log("ancTop = " +ancTop);
               Debug.Log("ancTop = " + "("+_transformParent.rect.height+ " - "+Top+" + "+Bot+" - "+Bot+") / "+_transformParent.rect.height+" - ("+_transform.anchorMin.x +" * ("+_transformParent.rect.height+" - "+Top +" + "+ Bot+")) / "+_transformParent.rect.height );
               Debug.Log("ancTop2 = " +ancTop2);
               Debug.Log("ancTop2 = " + "(" +(-Lef) +" + "+Rig+") / "+_transformParent.rect.height + " + (("+Lef +" - "+Rig+") * "+_transform.anchorMin.x+" - "+Rig+") / "+_transformParent.rect.height);
               Debug.Log("ancTop += " + (ancTop + ancTop2));
               
               ancTop += ancTop2;
               ancBot += ancBot2;
               
               
               
               var ancLef = (_transformParent.rect.width - (_transformParent.rect.width - Rig + Lef) - Rig) / _transformParent.rect.width + _transform.anchorMin.y * ((_transformParent.rect.width - Rig + Lef) / _transformParent.rect.width);
               var ancRig = ( _transformParent.rect.width - (_transformParent.rect.width  + Lef) ) / _transformParent.rect.width + _transform.anchorMax.y * ((_transformParent.rect.width - Rig + Lef) / _transformParent.rect.width);
               
                
                 var ancLef2 = ((+ Top - Bot) * _transform.anchorMin .y + Bot) / _transformParent.rect.width;
                 var ancRig2 = (( + Top - Bot) * _transform.anchorMax.y + Bot) / _transformParent.rect.width;

                 kofAncParentPortraitLef = ancLef2;
                 kofAncParentPortraitRig = ancRig2;
                 
                 Debug.Log("ancLef = " +ancLef);
                 Debug.Log("ancLef2 = " +ancLef2);
                 Debug.Log("ancLef += " + (ancLef + ancLef2));
                 
                 Debug.Log("ancRig = " +ancRig);
                 Debug.Log("ancRig2 = " +ancRig2);
                 Debug.Log("ancRig += " + (ancRig + ancRig2));
                 
                ancRig += ancRig2;
                ancLef += ancLef2;
                
                
                
                _transform.anchorMax = new Vector2(ancRig, ancTop ) ;
                _transform.anchorMin = new Vector2( ancLef  ,ancBot) ;
                
                
                
                _transform.offsetMax = new Vector2(PortraitRotTop,0-PortraitRotLeft);
                _transform.offsetMin = new Vector2(PortraitRotBottom,0-PortraitRotRight);
                
                Debug.Log("kofAncParentPortraitTop = "+ kofAncParentPortraitTop);
                Debug.Log("kofAncParentPortraitBot = "+ kofAncParentPortraitBot);
                Debug.Log("kofAncParentPortraitLef = "+ kofAncParentPortraitLef);
                Debug.Log("kofAncParentPortraitRig = "+ kofAncParentPortraitRig);
                
                
                //testBotton.position = new Vector3( testBotton.position.x, _transformParent.rect.height*_transform.anchorMin.y+_transform.offsetMin.y+_transformParent.offsetMin.y, testBotton.position.z);
                //testHeig.position = new Vector3( testHeig.position.x, _transformParent.rect.height*_transform.anchorMax.y+_transform.offsetMax.y +_transformParent.offsetMin.y, testHeig.position.z);
                //testBottonAnc.position = new Vector3(_transformParent.rect.width*_transform.anchorMin.x+_transform.offsetMin.x + _transformParent.offsetMin.x,testBottonAnc.position.y,testBottonAnc.position.z);
                //testHeigAnc.position = new Vector3(_transformParent.rect.width*_transform.anchorMax.x +_transform.offsetMax.x+ _transformParent.offsetMin.x,testHeigAnc.position.y ,testHeigAnc.position.z);











                _transform.rotation = Quaternion.Euler(PortraitRotation + _data[ScreenOrientation.LandscapeRight]._rotation);

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

    if (Screen.orientation== ScreenOrientation.LandscapeRight)
    {
        // var Top = _transformParent.offsetMax.y;
        // var Bot = _transformParent.offsetMin.y;
        //         
        // var Rig = _transformParent.offsetMax.x;
        // var Lef= _transformParent.offsetMin.x;
        //
        // var b = (_transformParent.rect.width + Top - Bot + Lef - Rig) * _transform.anchorMin.x + PortraitRotBottomX - Rig;
        // testBottonAnc.position = new Vector3(testBottonAnc.position.x,b , testBottonAnc.position.z);
        //         
        // var h = (_transformParent.rect.width + Top - Bot + Lef - Rig) * _transform.anchorMax.x + PortraitRotTopX - Rig; 
        // testHeigAnc.position = new Vector3(testHeigAnc.position.x, h, testHeigAnc.position.z);
        //
        // Debug.Log("b = " + _transformParent.rect.width + " * " + _transform.anchorMin.x + " + " + PortraitRotBottomX + " = " + b);
        // Debug.Log("h = " + _transformParent.rect.width + " * " + _transform.anchorMax.x + " + " + PortraitRotTopX + " = " + h);
        //         
        // Debug.Log("_transformParent.rect.width = "+ _transformParent.rect.width);
        //
        //
        //
        // var rightX = (_transformParent.rect.height - Top + Bot - Lef + Rig) * _transform.anchorMax.y + PortraitRotRightX + Bot;
        // var leftX = (_transformParent.rect.height - Top + Bot - Lef + Rig) * _transform.anchorMin.y + PortraitRotLeftX + Bot; 
        //         
        // testBotton.position = new Vector3( rightX, testBottonAnc.position.y, testBottonAnc.position.z);
        // testHeig.position = new Vector3( leftX, testBottonAnc.position.y, testBottonAnc.position.z);
        
        
        
        
        
        //testBotton.position = new Vector3( testBotton.position.x, _transformParent.rect.width*_transform.anchorMin.x, testBotton.position.z);
        //testHeig.position = new Vector3( testHeig.position.x, -630f, testHeig.position.z);
        //testBottonAnc.position = new Vector3(testBottonAnc.position.x,1650,testBottonAnc.position.z);


        Debug.Log("PortraitTop = "+ PortraitTop);
        Debug.Log("PortraitBottom = "+ PortraitBottom);
        Debug.Log("PortraitRight = "+ PortraitRight);
        Debug.Log("PortraitLeft = "+ PortraitLeft);





        
        // var widh = _transformParent.rect.height * _transform.anchorMax.y + PortraitRight -(_transformParent.rect.height * _transform.anchorMin.y+PortraitLeft);
        // Debug.Log("Widht = " + widh);
        // //Нужен ли тут множитель или нет вопрос,т.к если не нужен то может можно было бы дальше по формуле заменить отступы родителя?!!!!!!!!!!!!!
        // var distance = (_transformParent.rect.height * _transform.anchorMax.y - widh) / 2;
        // Debug.Log("distance = " + widh);
        // var kof = PortraitLeft - distance;
        // Debug.Log("kof = " + kof);
        //
        //
        // var bot = _transformParent.rect.width * _transform.anchorMin.x - PortraitTop / 2 + PortraitBottom / 2;
        //
        // Debug.Log("bot = " + bot);
        // bot += -1140 - 30;
        // //bot += -(_transformParent.rect.width/2) - kof;
        // Debug.Log("bot = " + bot);
        // bot += 540;
        // //bot += (_transformParent.rect.height/2);
        // Debug.Log("bot = " + bot);
        // bot -= _transformParent.offsetMax.y / 2 + _transformParent.offsetMin.y / 2;
        // Debug.Log("bot = " + bot);
        // testBotton.position = new Vector3( testBotton.position.x, bot, testBotton.position.z);
        //
        //
        //
        // var top = 0f;
        // top = 1140 + 540 + PortraitTop / 2 - PortraitBottom / 2;
        // //top = _transformParent.rect.width/2  + _transformParent.rect.height/2  + PortraitTop / 2 - PortraitBottom / 2;
        // Debug.Log("top = " + top);
        // top -= 30;
        // Debug.Log("top = " + top);
        // top += _transformParent.offsetMax.y / 2 - _transformParent.offsetMin.y / 2;
        // Debug.Log("top = " + top);
        // testHeig.position = new Vector3( testHeig.position.x, top, testHeig.position.z);
        
        
        
        
        
        
        
        
        
        /*
        
        
        //var widh = _transformParent.rect.height * _transform.anchorMax.y + PortraitRight -(_transformParent.rect.height * _transform.anchorMin.y+PortraitLeft);
        var widh = 930f;
        Debug.Log("Widht = " + widh);

        //var distance = (_transformParent.rect.height  - widh) / 2;
        var distance = (_transformParent.rect.height  - widh) / 2;
        Debug.Log("distance = " + distance);
        var kofTop = distance - PortraitLeft;
        var kofBot =  distance + PortraitRight;
        Debug.Log("kofBot = " + kofBot);
        Debug.Log("kofTop = " + kofTop);
        
        //
        // var bot = _transformParent.rect.width * _transform.anchorMin.x - PortraitTop / 2 + PortraitBottom / 2;
        //
        // Debug.Log("bot = " + bot);
        // bot += -1140 - 30;
        // Debug.Log("bot = " + bot);
        // bot += 540;
        // Debug.Log("bot = " + bot);
        // bot -= _transformParent.offsetMax.y / 2 + _transformParent.offsetMin.y / 2;
        // Debug.Log("bot = " + bot);
        
        
        //var bot = -1140 + 540 - kofBot +PortraitBottom/2 - PortraitTop/2;
        var bot = -(_transformParent.rect.width/2) + 540 - kofBot +PortraitBottom/2 - PortraitTop/2;
        Debug.Log("bot = " + bot);
        bot += (_transformParent.rect.width - _transformParent.rect.width * _transform.anchorMax.x)/2;
        Debug.Log("bot = " + bot);
        bot += ( _transformParent.rect.width * _transform.anchorMin.x)/2;
        Debug.Log("bot = " + bot);
        bot += (_transformParent.offsetMax.y / 2) -(_transformParent.offsetMin.y / 2);
        Debug.Log("bot = " + bot);
        testBotton.position = new Vector3( testBotton.position.x, bot, testBotton.position.z);
        //testBottonAnc.position = new Vector3( testBottonAnc.position.x, -1125, testBottonAnc.position.z);
        
        
        // var top = 0f;
        // top = 1140 + 540 + PortraitTop / 2 - PortraitBottom / 2;
        // //top = _transformParent.rect.width/2  + _transformParent.rect.height/2  + PortraitTop / 2 - PortraitBottom / 2;
        // Debug.Log("top = " + top);
        // top -= 30;
        // Debug.Log("top = " + top);
        // top += _transformParent.offsetMax.y / 2 - _transformParent.offsetMin.y / 2;
        // Debug.Log("top = " + top);

        
        
        //Скореее всего прийдеться избавиться от знач 1140(может тогда и делить не надо будет при кофицентах?)
        //var top = 1140 + 540 + kofTop-PortraitBottom/2 + PortraitTop/2;
        var top = (_transformParent.rect.width/2) + 540 + kofTop-PortraitBottom/2 + PortraitTop/2;
        Debug.Log("top = " + top);
        top -= (_transformParent.rect.width - _transformParent.rect.width * _transform.anchorMax.x )/2;
        Debug.Log("top = " + top);
        top -= ( _transformParent.rect.width * _transform.anchorMin.x)/2;
        Debug.Log("top = " + top);
        top -= (_transformParent.offsetMax.y / 2) +(_transformParent.offsetMin.y / 2);
        Debug.Log("top = " + top);
        testHeig.position = new Vector3( testHeig.position.x, top, testHeig.position.z);


        _transform.offsetMax = new Vector2(PortraitRotTop, top - 1080);
        _transform.offsetMin = new Vector2(PortraitRotBottom,bot);
        
        //testHeigAnc.position = new Vector3( testHeigAnc.position.x, 2155f, testHeigAnc.position.z);
        
        */

/*
        var height=_transformParent.rect.width*_transform.anchorMax.x-_transformParent.rect.width*_transform.anchorMin.x;
        var posBot = (-PortraitRight) - _difference / 2;
        var posHeig=posBot+height;
        posHeig += PortraitTop - PortraitBottom;
        
        testBotton.position = new Vector3( testBotton.position.x, posBot, testBotton.position.z);
        testHeig.position = new Vector3( testHeig.position.x, posHeig, testHeig.position.z);
        
        _transform.offsetMax = new Vector2(PortraitRotTop-1080, posHeig);
        _transform.offsetMin = new Vector2(PortraitRotBottom,posBot);
        */
        







        var posHeig = _difference / 2 - PortraitLeft;
        var posBot = (-PortraitRight) - _difference / 2;

        var posRig = PortraitTop - _difference / 2;
        var posLeft = _difference / 2  + PortraitBottom ;
        
        
        // растяжение по горизонтали
        posHeig += (_transformParent.offsetMax.x * (_transform.anchorMax.x ) / 2);//- (_transformParent.offsetMin.x * _transform.anchorMin.x / 2);
        posBot -= (_transformParent.offsetMax.x * (_transform.anchorMax.x ) / 2); //+ (_transformParent.offsetMin.x * _transform.anchorMin.x / 2);
        
        //Убирает увел по горизонтале при растяж в вверх
        posHeig -= (_transformParent.offsetMax.y * _transform.anchorMax.y / 2);
        posBot += (_transformParent.offsetMax.y * _transform.anchorMax.y / 2);
        
        
        // растяжение по вертикале
        posRig += (_transformParent.offsetMax.y * _transform.anchorMax.y / 2);//- (_transformParent.offsetMin.y * _transform.anchorMin.y / 2);
        posLeft -= (_transformParent.offsetMax.y * _transform.anchorMax.y / 2);// + (_transformParent.offsetMin.y * _transform.anchorMin.y / 2);

        //Убирает увел по вертикали при растяж в право
        posRig -=(_transformParent.offsetMax.x * (_transform.anchorMax.x) / 2);
        posLeft += (_transformParent.offsetMax.x * (_transform.anchorMax.x) / 2);

        
        //Для возможности растягивания вниз
        posHeig += _transformParent.offsetMin.y * _transform.anchorMax.y  / 2;
        posBot -= _transformParent.offsetMin.y * _transform.anchorMax.y/ 2;
        
        posRig -= _transformParent.offsetMin.y * _transform.anchorMax.y/ 2;
        posLeft += _transformParent.offsetMin.y * _transform.anchorMax.y/ 2;
        
        
        //Учет нминимального якоря по Y(этот якорь именно после переворота) при раст. вниз
        posHeig -= _transformParent.offsetMin.y * _transform.anchorMin.y / 2;
        posBot += _transformParent.offsetMin.y * _transform.anchorMin.y/ 2;
        
        posRig += _transformParent.offsetMin.y * _transform.anchorMin.y/ 2;
        posLeft -= _transformParent.offsetMin.y * _transform.anchorMin.y/ 2;
        
        
        //Для возможности растягивания влево (test)
        posRig += _transformParent.offsetMin.x * _transform.anchorMax.x/2;
        posLeft -= _transformParent.offsetMin.x *_transform.anchorMax.x/ 2;

        posHeig -= _transformParent.offsetMin.x * _transform.anchorMax.x/2;
        posBot += _transformParent.offsetMin.x  * _transform.anchorMax.x/ 2;

        
        //Учет нминимального якоря по X(этот якорь именно после переворота) при раст. влево
        posRig -= _transformParent.offsetMin.x * _transform.anchorMin.x/2;
        posLeft += _transformParent.offsetMin.x *_transform.anchorMin.x/ 2;

        posHeig += _transformParent.offsetMin.x * _transform.anchorMin.x/2;
        posBot -= _transformParent.offsetMin.x  * _transform.anchorMin.x/ 2;
        
        
        //Учет нминимального якоря по X(этот якорь именно после переворота)
        posRig +=(_transformParent.offsetMax.x * (_transform.anchorMin.x ) / 2);
        posLeft -= (_transformParent.offsetMax.x * (_transform.anchorMin.x ) / 2);
        
        posHeig -= (_transformParent.offsetMax.x * (_transform.anchorMin.x ) / 2);
        posBot += (_transformParent.offsetMax.x * (_transform.anchorMin.x ) / 2);
        
        
        //Учет нминимального якоря по Y(этот якорь именно после переворота) 
        posHeig += (_transformParent.offsetMax.y * (_transform.anchorMin.y ) / 2);
        posBot -= (_transformParent.offsetMax.y * (_transform.anchorMin.y ) / 2);
        
        posRig -= (_transformParent.offsetMax.y * (_transform.anchorMin.y ) / 2);
        posLeft += (_transformParent.offsetMax.y * (_transform.anchorMin.y ) / 2);
        
        
        testBotton.position = new Vector3( testBotton.position.x, posBot, testBotton.position.z);
        testHeig.position = new Vector3(testHeig.position.x, posHeig + 1080, testHeig.position.z);
        
        testBottonAnc.position = new Vector3(posRig+2280,testBottonAnc.position.y,testBottonAnc.position.z);
        testHeigAnc.position = new Vector3(posLeft,testHeigAnc.position.y ,testHeigAnc.position.z);
        
        _transform.offsetMax = new Vector2(posRig, posHeig);
        _transform.offsetMin = new Vector2(posLeft,posBot);
        
        //_transform.rotation = Quaternion.Euler(PortraitRotation + _data[ScreenOrientation.LandscapeRight]._rotation);
        
        
        
        
        
        
        //testHeig.position = new Vector3( testHeig.position.x, _transformParent.rect.height*_transform.anchorMax.y+_transform.offsetMax.y +_transformParent.offsetMin.y, testHeig.position.z);
        //testBotton.position = new Vector3( testBotton.position.x, _transformParent.rect.height*_transform.anchorMin.y+_transform.offsetMin.y+_transformParent.offsetMin.y, testBotton.position.z);        
        //testBottonAnc.position = new Vector3(_transformParent.rect.width*_transform.anchorMin.x+_transform.offsetMin.x + _transformParent.offsetMin.x,testBottonAnc.position.y,testBottonAnc.position.z);
        //testHeigAnc.position = new Vector3(_transformParent.rect.width*_transform.anchorMax.x +_transform.offsetMax.x+ _transformParent.offsetMin.x,testHeigAnc.position.y ,testHeigAnc.position.z);
    }
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

private void FixedUpdate()
{

    switch (Screen.orientation)
    {
        case ScreenOrientation.Portrait:
        {
            var difference = _transform.rect.height - _transform.rect.width;
            var coefficient = difference / 2;

_difference=difference;

            PortraitRotLeft = _transform.offsetMin.x - coefficient;
            PortraitRotRight = _transform.offsetMax.x + coefficient;

            PortraitRotTop = _transform.offsetMax.y - coefficient;
            PortraitRotBottom = _transform.offsetMin.y + coefficient;
            
            
            PortraitTop = _transform.offsetMax.y;
            PortraitBottom = _transform.offsetMin.y;
            
            PortraitRight = _transform.offsetMax.x;
            PortraitLeft = _transform.offsetMin.x;
        }
            break;
           
    }
    
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
