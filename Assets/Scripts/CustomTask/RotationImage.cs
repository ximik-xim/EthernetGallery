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
    
    //отступы родителя
    private float ParentPortraitTop;
    private float ParentPortraitBottom;
    private float ParentPortraitLeft;
    private float ParentPortraitRight;

    
    private float ParentPortraitAncMinX;
    private float ParentPortraitAncMaxX;
    private float ParentPortraitAncMinY;
    private float ParentPortraitAncMaxY;
    
    
    private float kofAncParentPortraitTop;
    private float kofAncParentPortraitBot;
    private float kofAncParentPortraitLef;
    private float kofAncParentPortraitRig;
    
    
    private float antiKofAncParentPortraitTop;
    private float antiKofAncParentPortraitBot;
    private float antiKofAncParentPortraitLef;
    private float antiKofAncParentPortraitRig;
    
    private float _difference;

    [SerializeField][Range(-3f,3f)]
    private float testCofTop = 0;
    [SerializeField][Range(-3f,3f)]
    private float testCofBot = 0;
    [SerializeField][Range(-3f,3f)]
    private float testCofLef = 0;
    [SerializeField][Range(-3f,3f)]
    private float testCofRig = 0;
    
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
               
                 _transform.anchorMax = new Vector2(ParentPortraitAncMaxX,ParentPortraitAncMaxY);
                 _transform.anchorMin = new Vector2(ParentPortraitAncMinX,ParentPortraitAncMinY);
                 
                 _transform.offsetMax = new Vector2(PortraitRight, PortraitTop);
                 _transform.offsetMin = new Vector2(PortraitLeft, PortraitBottom);
                 
                 
                 
                 
                 _transform.rotation = Quaternion.Euler(PortraitRotation);
             }
                break;
               
            case ScreenOrientation.LandscapeRight:
            {


                var Top = _transformParent.offsetMax.y;
                var Bot = _transformParent.offsetMin.y;
                
                var Rig = _transformParent.offsetMax.x;
                var Lef= _transformParent.offsetMin.x;
                Debug.Log("Top = " + Top);
                Debug.Log("Bot = " + Bot);

                Debug.Log("Lef = " + Lef);
                Debug.Log("Rig = " + Rig);

                
                var  ancTop= (_transformParent.rect.height - Top ) / _transformParent.rect.height - (_transform.anchorMin.x * (_transformParent.rect.height - Top + Bot)) / _transformParent.rect.height;
                var ancBot = (_transformParent.rect.height - Top) / _transformParent.rect.height - (_transform.anchorMax.x * (_transformParent.rect.height - Top + Bot)) / _transformParent.rect.height;
                
                
               var ancTop2 = (-Lef + Rig) / _transformParent.rect.height + ((Lef - Rig) * _transform.anchorMin.x - Rig) / _transformParent.rect.height;
               var  ancBot2=(-Lef+ Rig) / _transformParent.rect.height + ((Lef - Rig) * _transform.anchorMax.x - Rig) / _transformParent.rect.height;

               kofAncParentPortraitTop = ancTop2;
               kofAncParentPortraitBot = ancBot2;

               //Не уверен пока в правельности формулы
               antiKofAncParentPortraitTop = ( -Top) / _transformParent.rect.height - (_transform.anchorMin.x * (- Top + Bot)) / _transformParent.rect.height;
               antiKofAncParentPortraitBot = (- Top) / _transformParent.rect.height - (_transform.anchorMax.x * (- Top + Bot)) / _transformParent.rect.height;
               
                   
               Debug.Log("ancTop = " +ancTop);
               Debug.Log("ancTop = " + "("+_transformParent.rect.height+ " - "+Top+" + "+Bot+" - "+Bot+") / "+_transformParent.rect.height+" - ("+_transform.anchorMin.x +" * ("+_transformParent.rect.height+" - "+Top +" + "+ Bot+")) / "+_transformParent.rect.height );
               Debug.Log("ancTop2 = " +ancTop2);
               Debug.Log("ancTop2 = " + "(" +(-Lef) +" + "+Rig+") / "+_transformParent.rect.height + " + (("+Lef +" - "+Rig+") * "+_transform.anchorMin.x+" - "+Rig+") / "+_transformParent.rect.height);
               Debug.Log("ancTop += " + (ancTop + ancTop2));
               
               Debug.Log("ancBot = " +ancBot);
               Debug.Log("ancBot = " + "("+_transformParent.rect.height+ " - "+Top+" + "+Bot+" - "+Bot+") / "+_transformParent.rect.height+" - ("+_transform.anchorMax.x +" * ("+_transformParent.rect.height+" - "+Top +" + "+ Bot+")) / "+_transformParent.rect.height );
               Debug.Log("ancBot2 = " +ancBot2);
               Debug.Log("ancBot2 = " + "(" +(-Lef) +" + "+Rig+") / "+_transformParent.rect.height + " + (("+Lef +" - "+Rig+") * "+_transform.anchorMax.x+" - "+Rig+") / "+_transformParent.rect.height);
               Debug.Log("ancBot += " + (ancBot+ancBot2));
               
               ancTop += ancTop2;
               ancBot += ancBot2;
               
               
               
               var ancLef = (_transformParent.rect.width - (_transformParent.rect.width  + Lef) ) / _transformParent.rect.width + _transform.anchorMin.y * ((_transformParent.rect.width - Rig + Lef) / _transformParent.rect.width);
               var ancRig = ( _transformParent.rect.width - (_transformParent.rect.width  + Lef) ) / _transformParent.rect.width + _transform.anchorMax.y * ((_transformParent.rect.width - Rig + Lef) / _transformParent.rect.width);
               
               
               // //возможно тут - в самом начале 
               // antiKofAncParentPortraitLef= Lef  / _transformParent.rect.width + _transform.anchorMin.y * (( - Rig + Lef) / _transformParent.rect.width);
               // antiKofAncParentPortraitRig = Lef  / _transformParent.rect.width + _transform.anchorMax.y * (( - Rig + Lef) / _transformParent.rect.width);

               //возможно тут - в самом начале 
               antiKofAncParentPortraitLef= -Lef  / _transformParent.rect.width - _transform.anchorMin.y * (( - Rig + Lef) / _transformParent.rect.width);
               antiKofAncParentPortraitRig = -Lef  / _transformParent.rect.width - _transform.anchorMax.y * (( - Rig + Lef) / _transformParent.rect.width);

               var ancLef2 = ((+ Top - Bot) * _transform.anchorMin .y + Bot) / _transformParent.rect.width;
                 var ancRig2 = (( + Top - Bot) * _transform.anchorMax.y + Bot) / _transformParent.rect.width;

                 kofAncParentPortraitLef = ancLef2;
                 kofAncParentPortraitRig = ancRig2;
                 
                 Debug.Log("ancLef = " +ancLef);
                 Debug.Log("ancLef = " + "(" + _transformParent.rect.width + " - (" + _transformParent.rect.width + " + " + Lef + ")) / " + _transformParent.rect.width + " + " + _transform.anchorMin.y + " * ((" + _transformParent.rect.width + " - " + Rig + " + " + Lef + ") / " + _transformParent.rect.width);
                 Debug.Log("ancLef2 = " +ancLef2);
                 Debug.Log("ancLef += " + (ancLef + ancLef2));
                 
                 Debug.Log("ancRig = " +ancRig);
                 Debug.Log("ancRig = " + "(" + _transformParent.rect.width + " - (" + _transformParent.rect.width + " + " + Lef + ")) / " + _transformParent.rect.width + " + " + _transform.anchorMax.y + " * ((" + _transformParent.rect.width + " - " + Rig + " + " + Lef + ") / " + _transformParent.rect.width);
                 Debug.Log("ancRig2 = " +ancRig2);
                 Debug.Log("ancRig += " + (ancRig + ancRig2));


                 Debug.Log("antiKofAncParentPortraitRig = " + Lef + " / " + _transformParent.rect.width + " + " + _transform.anchorMax.y + " * ((" + -Rig + " + " + Lef + ") / " + _transformParent.rect.width + ")");
                 Debug.Log("antiKofAncParentPortraitRig = " + antiKofAncParentPortraitRig);
                 
                 Debug.Log("kofAncParentPortraitLef = " + Lef + " / " + _transformParent.rect.width + " + " + _transform.anchorMin.y + " * ((" + -Rig + " + " + Lef + ") / " + _transformParent.rect.width + ")");
                 Debug.Log("kofAncParentPortraitLef = " + kofAncParentPortraitLef);
                 
                 
                 
                 
                 
                 
                 
                 
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

        

        var ancLef = (_transformParent.rect.width - (_transformParent.rect.width  + ParentPortraitLeft) ) / _transformParent.rect.width + ParentPortraitAncMinY * ((_transformParent.rect.width - ParentPortraitRight + ParentPortraitLeft) / _transformParent.rect.width);
        var ancRig = (_transformParent.rect.width - (_transformParent.rect.width + ParentPortraitLeft)) / _transformParent.rect.width + ParentPortraitAncMaxY * ((_transformParent.rect.width - ParentPortraitRight + ParentPortraitLeft) / _transformParent.rect.width);
        var ancLef2 = (( ParentPortraitTop - ParentPortraitBottom) * ParentPortraitAncMinY + ParentPortraitBottom) / _transformParent.rect.width;
        var ancRig2 = ((  ParentPortraitTop - ParentPortraitBottom) * ParentPortraitAncMaxY + ParentPortraitBottom) / _transformParent.rect.width;
       
        
        Debug.Log("CurAncLef = " +ancLef);
        Debug.Log("CurAncLef = " + "(" + _transformParent.rect.width + " - (" + _transformParent.rect.width + " + " + ParentPortraitLeft + ")) / " + _transformParent.rect.width + " + " + ParentPortraitAncMinY + " * ((" + _transformParent.rect.width + " - " + ParentPortraitRight + " + " + ParentPortraitLeft + ") / " + _transformParent.rect.width);
        Debug.Log("CurAncLef2 = " +ancLef2);
        Debug.Log("CurAncLef += " + (ancLef + ancLef2));
                 
        Debug.Log("CurAncRig = " +ancRig);
        Debug.Log("CurAncRig = " + "(" + _transformParent.rect.width + " - (" + _transformParent.rect.width + " + " + ParentPortraitLeft + ")) / " + _transformParent.rect.width + " + " + ParentPortraitAncMaxY + " * ((" + _transformParent.rect.width + " - " + ParentPortraitRight + " + " + ParentPortraitLeft + ") / " + _transformParent.rect.width);
        Debug.Log("CurAncRig2 = " +ancRig2);
        Debug.Log("CurAncRig += " + (ancRig + ancRig2));
        
        kofAncParentPortraitLef = ancLef2;
        kofAncParentPortraitRig = ancRig2;

        ancRig += ancRig2;
        ancLef += ancLef2;
        
        
        
        var  ancTop= (_transformParent.rect.height - ParentPortraitTop ) / _transformParent.rect.height - (ParentPortraitAncMinX * (_transformParent.rect.height - ParentPortraitTop + ParentPortraitBottom)) / _transformParent.rect.height;
        var ancBot = (_transformParent.rect.height - ParentPortraitTop) / _transformParent.rect.height - (ParentPortraitAncMaxX * (_transformParent.rect.height - ParentPortraitTop + ParentPortraitBottom)) / _transformParent.rect.height;
        var ancTop2 = (-ParentPortraitLeft + ParentPortraitRight) / _transformParent.rect.height + ((ParentPortraitLeft - ParentPortraitRight) * ParentPortraitAncMinX - ParentPortraitRight) / _transformParent.rect.height;
        var  ancBot2=(-ParentPortraitLeft+ ParentPortraitRight) / _transformParent.rect.height + ((ParentPortraitLeft - ParentPortraitRight) * ParentPortraitAncMaxX - ParentPortraitRight) / _transformParent.rect.height;

        Debug.Log("ancTop = " +ancTop);
        Debug.Log("ancTop = " + "("+_transformParent.rect.height+ " - "+ParentPortraitTop+" ) / "+_transformParent.rect.height+" - ("+ParentPortraitAncMinX +" * ("+_transformParent.rect.height+" - "+ParentPortraitTop +" + "+ ParentPortraitBottom+")) / "+_transformParent.rect.height );
        Debug.Log("ancTop2 = " +ancTop2);
        Debug.Log("ancTop += " + (ancTop + ancTop2));
               
        Debug.Log("ancBot = " +ancBot);
        Debug.Log("ancBot = " + "("+_transformParent.rect.height+ " - "+ParentPortraitTop+" ) / "+_transformParent.rect.height+" - ("+ParentPortraitAncMaxX +" * ("+_transformParent.rect.height+" - "+ParentPortraitTop +" + "+ ParentPortraitBottom+")) / "+_transformParent.rect.height );
        Debug.Log("ancBot2 = " +ancBot2);
        Debug.Log("ancBot += " + (ancBot+ancBot2));
        
        ancTop += ancTop2;
        ancBot += ancBot2;

        kofAncParentPortraitTop = ancTop2;
        kofAncParentPortraitBot = ancBot2;
        
        
        
        _transform.anchorMax = new Vector2(ancRig, ancTop ) ;
        _transform.anchorMin = new Vector2( ancLef  ,ancBot) ;
        
        
        Debug.Log("PortraitTop = "+ PortraitTop);
        Debug.Log("PortraitBottom = "+ PortraitBottom);
        Debug.Log("PortraitRight = "+ PortraitRight);
        Debug.Log("PortraitLeft = "+ PortraitLeft);


        antiKofAncParentPortraitTop = ( -ParentPortraitTop) / _transformParent.rect.height - (ParentPortraitAncMinX * (- ParentPortraitTop + ParentPortraitBottom)) / _transformParent.rect.height;
        antiKofAncParentPortraitBot = (- ParentPortraitTop) / _transformParent.rect.height - (ParentPortraitAncMaxX * (- ParentPortraitTop + ParentPortraitBottom)) / _transformParent.rect.height;

        antiKofAncParentPortraitLef= -ParentPortraitLeft  / _transformParent.rect.width - ParentPortraitAncMinY * (( - ParentPortraitRight + ParentPortraitLeft) / _transformParent.rect.width);
        antiKofAncParentPortraitRig = -ParentPortraitLeft  / _transformParent.rect.width - ParentPortraitAncMaxY * (( - ParentPortraitRight + ParentPortraitLeft) / _transformParent.rect.width);



        //Точка сверху после переворота на -90 после смены оринтации( а так это лево)
        var posHeig = _difference / 2 - PortraitLeft;
        var posBot = (-PortraitRight) - _difference / 2;

        //Точка справа после переворота на -90 после смены оринтации( а так это верх)
        var posRig = PortraitTop - _difference / 2;
        var posLeft = _difference / 2  + PortraitBottom ;
        
        
        var ParentOffsetMaxX = _transformParent.offsetMax.x;
        var ParentOffsetMaxY = _transformParent.offsetMax.y;

        var ParentOffsetMinX = _transformParent.offsetMin.x;
        var ParentOffsetMinY = _transformParent.offsetMin.y;


        var ancMaxX = _transform.anchorMax.x; 
        var ancMaxY = _transform.anchorMax.y;
        
        var ancMinX = _transform.anchorMin.x;
        var ancMinY = _transform.anchorMin.y;
        
        
        
        Debug.Log("-----------------------------------------------------");
        Debug.Log("ParentOffsetMaxX = " + ParentOffsetMaxX);
        Debug.Log("ParentOffsetMaxY = " + ParentOffsetMaxY);
        Debug.Log("ParentOffsetMinX = " + ParentOffsetMinX);
        Debug.Log("ParentOffsetMinY = " + ParentOffsetMinY);
        
        Debug.Log("----------------------------------------------------");
        Debug.Log("ancMaxX = " + ancMaxX);
        Debug.Log("ancMaxY = " + ancMaxY);
        Debug.Log("ancMinX = " + ancMinX);
        Debug.Log("ancMinY = " + ancMinY);
        
        Debug.Log("---------------------------------------------------");
        Debug.Log("ParentPortraitTop = " + ParentPortraitTop);
        Debug.Log("ParentPortraitBottom = " + ParentPortraitBottom);
        Debug.Log("ParentPortraitLeft = " + ParentPortraitLeft);
        Debug.Log("ParentPortraitRight = " + ParentPortraitRight);
        
        Debug.Log("--------------------------------------------------");
        Debug.Log("kofAncParentPortraitTop = " + kofAncParentPortraitTop);
        Debug.Log("kofAncParentPortraitBot = " + kofAncParentPortraitBot);
        Debug.Log("kofAncParentPortraitLef = " + kofAncParentPortraitLef);
        Debug.Log("kofAncParentPortraitRig = " + kofAncParentPortraitRig);
        
        Debug.Log("-------------------------------------------------");
        Debug.Log("anitKofAncParentPortraitTop = " + antiKofAncParentPortraitTop);
        Debug.Log("anitKkofAncParentPortraitBot = " + antiKofAncParentPortraitBot);
        Debug.Log("anitKkofAncParentPortraitLef = " + antiKofAncParentPortraitLef);
        Debug.Log("anitKkofAncParentPortraitRig = " + antiKofAncParentPortraitRig);

        
         ParentOffsetMaxY -= ParentPortraitTop;
         ParentOffsetMinX -= ParentPortraitLeft;
         ParentOffsetMinY -= ParentPortraitBottom;
         ParentOffsetMaxX -= ParentPortraitRight;
         
         


        // Для возможности растягивания в право
        posHeig += (ParentOffsetMaxX * (ancMaxX - kofAncParentPortraitRig + antiKofAncParentPortraitRig) / 2);
        posBot -= (ParentOffsetMaxX * (ancMaxX - kofAncParentPortraitRig + antiKofAncParentPortraitRig) / 2);

        posRig -= (ParentOffsetMaxX * (ancMaxX - kofAncParentPortraitRig + antiKofAncParentPortraitRig) / 2);
        posLeft += (ParentOffsetMaxX * (ancMaxX - kofAncParentPortraitRig + antiKofAncParentPortraitRig) / 2);
        
        //Для возможности растягивания влево 
        posHeig -= ParentOffsetMinX * (ancMaxX - kofAncParentPortraitRig + antiKofAncParentPortraitRig) / 2;
        posBot += ParentOffsetMinX * (ancMaxX - kofAncParentPortraitRig + antiKofAncParentPortraitRig) / 2;

        posRig += ParentOffsetMinX * (ancMaxX - kofAncParentPortraitRig + antiKofAncParentPortraitRig) / 2;
        posLeft -= ParentOffsetMinX * (ancMaxX - kofAncParentPortraitRig + antiKofAncParentPortraitRig) / 2;
        

        
        //Учет нминимального якоря по X(этот якорь именно после переворота) и растяг в право
        posHeig -= (ParentOffsetMaxX * (ancMinX - kofAncParentPortraitLef + antiKofAncParentPortraitLef) / 2);
        posBot += (ParentOffsetMaxX * (ancMinX - kofAncParentPortraitLef + antiKofAncParentPortraitLef) / 2);

        posRig += (ParentOffsetMaxX * (ancMinX - kofAncParentPortraitLef + antiKofAncParentPortraitLef) / 2);
        posLeft -= (ParentOffsetMaxX * (ancMinX - kofAncParentPortraitLef + antiKofAncParentPortraitLef) / 2);

        //Учет нминимального якоря по X(этот якорь именно после переворота) при раст. влево
        posHeig += ParentOffsetMinX * (ancMinX - kofAncParentPortraitLef + antiKofAncParentPortraitLef) / 2;
        posBot -= ParentOffsetMinX * (ancMinX - kofAncParentPortraitLef + antiKofAncParentPortraitLef) / 2;

        posRig -= ParentOffsetMinX * (ancMinX - kofAncParentPortraitLef + antiKofAncParentPortraitLef) / 2;
        posLeft += ParentOffsetMinX * (ancMinX - kofAncParentPortraitLef + antiKofAncParentPortraitLef) / 2;

                
        
        // Для возможности растягивания вверх
        posHeig -= (ParentOffsetMaxY * (ancMaxY - antiKofAncParentPortraitTop - kofAncParentPortraitTop) / 2);
        posBot += (ParentOffsetMaxY * (ancMaxY - antiKofAncParentPortraitTop - kofAncParentPortraitTop) / 2);

        posRig += (ParentOffsetMaxY * (ancMaxY - antiKofAncParentPortraitTop - kofAncParentPortraitTop) / 2);
        posLeft -= (ParentOffsetMaxY * (ancMaxY - antiKofAncParentPortraitTop - kofAncParentPortraitTop) / 2);
         
        //Для возможности растягивания вниз
        posHeig += ParentOffsetMinY * (ancMaxY - antiKofAncParentPortraitTop - kofAncParentPortraitTop) / 2;
        posBot -= ParentOffsetMinY * (ancMaxY - antiKofAncParentPortraitTop - kofAncParentPortraitTop) / 2;

        posRig -= ParentOffsetMinY * (ancMaxY - antiKofAncParentPortraitTop - kofAncParentPortraitTop) / 2;
        posLeft += ParentOffsetMinY * (ancMaxY - antiKofAncParentPortraitTop - kofAncParentPortraitTop) / 2;

        
        
        //Учет нминимального якоря по Y(этот якорь именно после переворота)  и растяг в вверх
        posHeig += (ParentOffsetMaxY * (ancMinY - antiKofAncParentPortraitBot - kofAncParentPortraitBot) / 2);
        posBot -= (ParentOffsetMaxY * (ancMinY - antiKofAncParentPortraitBot - kofAncParentPortraitBot) / 2);

        posRig -= (ParentOffsetMaxY * (ancMinY - antiKofAncParentPortraitBot - kofAncParentPortraitBot) / 2);
        posLeft += (ParentOffsetMaxY * (ancMinY - antiKofAncParentPortraitBot - kofAncParentPortraitBot) / 2);

        
        //Учет нминимального якоря по Y(этот якорь именно после переворота) при раст. вниз
        posHeig -= ParentOffsetMinY * (ancMinY -antiKofAncParentPortraitBot-kofAncParentPortraitBot) / 2;
        posBot += ParentOffsetMinY * (ancMinY - antiKofAncParentPortraitBot - kofAncParentPortraitBot) / 2;

        posRig += ParentOffsetMinY * (ancMinY - antiKofAncParentPortraitBot - kofAncParentPortraitBot) / 2;
        posLeft -= ParentOffsetMinY * (ancMinY - antiKofAncParentPortraitBot - kofAncParentPortraitBot) / 2;

        

        testBotton.position = new Vector3( testBotton.position.x, posBot, testBotton.position.z);
        testHeig.position = new Vector3(testHeig.position.x, posHeig + 1080, testHeig.position.z);
        
        testBottonAnc.position = new Vector3(posRig+2280,testBottonAnc.position.y,testBottonAnc.position.z);
        testHeigAnc.position = new Vector3(posLeft,testHeigAnc.position.y ,testHeigAnc.position.z);


        _transform.offsetMax = new Vector2(posRig , posHeig);
        _transform.offsetMin = new Vector2(posLeft  ,posBot);

        Debug.Log("posHeig = " + posHeig);
        Debug.Log("posBot = " + posBot);
        Debug.Log("posRig = " + posRig);
        Debug.Log("posLeft = " + posLeft);
  
        
        //_transform.rotation = Quaternion.Euler(PortraitRotation + _data[ScreenOrientation.LandscapeRight]._rotation);
        
    }
}

private void FixedUpdate()
{

    switch (Screen.orientation)
    {
        case ScreenOrientation.Portrait:
        {

            ParentPortraitAncMinX = _transform.anchorMin.x;
            ParentPortraitAncMaxX = _transform.anchorMax.x;
            ParentPortraitAncMinY = _transform.anchorMin.y;
            ParentPortraitAncMaxY = _transform.anchorMax.y;

            Debug.Log("ParentPortraitAncMinX = " + ParentPortraitAncMinX);
            Debug.Log("ParentPortraitAncMaxX = " + ParentPortraitAncMaxX);
            Debug.Log("ParentPortraitAncMinY = " + ParentPortraitAncMinY);
            Debug.Log("ParentPortraitAncMaxY = " + ParentPortraitAncMaxY);
            
         
            var difference = _transform.rect.height - _transform.rect.width;
            var coefficient = difference / 2;

            Debug.Log("difference = " + difference);
            _difference = difference;

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
}