 using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ImageController : MonoBehaviour
{
    public event Action OnImageLoadFinish;

    [SerializeField] 
    private Transform _parent;

    [SerializeField] 
    private ImageSet _prefab;
    
    [SerializeField] 
    private GetInfoFileURL _getInfoFileURL;

    private List<ImageSet> _image=new List<ImageSet>();

[SerializeField]
private GetImageURL _test; 

    private void Awake()
    {
        _getInfoFileURL.OnGetUrlImage += OnComlite;
        OnImageLoadFinish += Test;
    }

    private void OnComlite(UnityWebRequest.Result obj)
    {
        if (obj == UnityWebRequest.Result.Success)
        {
            Debug.Log(_getInfoFileURL.UrlImage[0]);
            //Вот сюда логику с запросами засунуть

            for (int i = 0; i < _getInfoFileURL.UrlImage.Count; i++)
            {
                var image = Instantiate(_prefab,_parent);
                _image.Add(image);
            }

            SetImage();
        }
    }

    //При такой реализации, получ след.
    //Отправляем запрос у 1 элемента и ждем пока получим результат
    //Затем только переключаемся на элемент 2 и снова отправляем запрос и снова ждем
    
    //Подумать над реализацией отправки запросов сразу на все картинки, а потом ждать ответа ото всех
    
    private async Task SetImage()
    {
        for (int i = 0; i < _getInfoFileURL.UrlImage.Count; i++)
        {
            //Ожидания поочередно прогузки информации
            //Texture2D texture2D = await GetImageURL.GetStaticAsinkTexrure(_getInfoFileURL.UrlImage[i]);
            //Texture2D texture2D = await _test.GetAsinkTexrure(_getInfoFileURL.UrlImage[i]);
           
            //Оповешение об завершении загрузки информации по мере того как загрузка любого обьекта закончиться
            //StartCoroutine( _test.GetTexture(_getInfoFileURL.UrlImage[i],i,TestCoroutine));
            _test.GetAsinkTexrure(_getInfoFileURL.UrlImage[i], i, TestAsink);

        }
        OnImageLoadFinish?.Invoke();
    }

    private void TestCoroutine(int i, Texture2D texture2D)
    {
        _image[i].SetImage(texture2D);
        Debug.Log(i);
    }
    
    private void TestAsink(int i, UnityWebRequest.Result result, Texture2D texture2D)
    {
        if (result == UnityWebRequest.Result.Success)
        {
            _image[i].SetImage(texture2D);
            Debug.Log(i);    
        }
    } 


    private void Test()
    {
        Debug.Log("Comlite");   
    }

    private void OnDestroy()
    {
        _getInfoFileURL.OnGetUrlImage -= OnComlite;
    }
}
