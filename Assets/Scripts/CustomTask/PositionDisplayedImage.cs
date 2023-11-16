using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PositionDisplayedImage : MonoBehaviour
{

 public static PositionDisplayedImage PositionDisplayedImageStat;
 
 public static event Action OnInit;

 public ScreenOrientation CurrentPositionDisplayedImage => _currentPositionDisplayedImage;
 private ScreenOrientation _currentPositionDisplayedImage;
 public event Action<ScreenOrientation> OnRotateDisplayedImage;

 [SerializeField] 
 private bool _setDontDestroy = true;
 
 private void Awake()
 {
  PositionDisplayedImageStat = this;
  OnInit?.Invoke();

  if (_setDontDestroy == true)
  {
   DontDestroyOnLoad(gameObject); 
  }
  
 }

 public void SetOrientation(ScreenOrientation orientation)
 {
  if (orientation != CurrentPositionDisplayedImage)
  {
   Screen.orientation = orientation;
   return;
  }
  
  Debug.LogError(orientation+" уже была выбранна в виде текущей ориентации");
  
 }
 
 private void Update()
 {

  if (CurrentPositionDisplayedImage != Screen.orientation)
  {
   Debug.Log("INVOKE ");
   OnRotateDisplayedImage?.Invoke(Screen.orientation);

   _currentPositionDisplayedImage = Screen.orientation;

  }
  
  

  
  
  
  
  
  
  
  
  
  
  Debug.Log("In OR = "+Input.deviceOrientation);
  Debug.Log("Sc OR = "+Screen.orientation);


//Проверено тестами
//Input.deviceOrintation возращает текущее положение телефона в пространстве

//Screen.orientation возращает текущий режим отображения на телефоне

// Они могут спокойно не совподать между собой,
// к примеру
// Если отключить автоповорот в Портретном режиме и перевенуть его, то картинка как отображалась в Портретном режиме,
// так и будет отображаться в Портретном режиме, хотя телефон находиться Ланшафтном положении
// Соответственно в такой ситуации 
//  Screen.orientation - вернет Портретный режим отображения телефона
//  Input.deviceOrientation - вернет Ланшафтном положение телефона

// Так же к примеру значения не будут совпадаться, если отключить в Unity в Player Setting некоторые режими отображения
// Тогда они будут отключены для Screen.orientation и по этому телефон сам не будет в них переходит,
// и по этому значения будут отличаться между Input.deviceOrientation(положением телефона в пространстве) и Screen.orientation(режим отображения изображения в телефоне)
 } 

 
 
}
