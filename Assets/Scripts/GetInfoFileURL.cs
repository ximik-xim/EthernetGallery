using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetInfoFileURL : MonoBehaviour
{
    public IReadOnlyList<string> UrlImage => _urlImage;
    public event Action<UnityWebRequest.Result> OnGetUrlImage = default;
[SerializeField]
    private string Url = "http://data.ikppbb.com/test-task-unity-data/pics";
    private List<string> _urlImage = new List<string>();
    
    void Start()
    {
        StartCoroutine(GetTexture(Url));

    }
    
    
    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) 
        {
            OnGetUrlImage?.Invoke(www.result);
        }
        else
        {

            int indexStrart = 0;
           int indexEnd = 0;


           string htmlCode = www.downloadHandler.text;

        
           string startPoing = "tr><td data-sort=\"";
           string endPoint = ".jpg";
           
           while ((indexStrart = htmlCode.IndexOf(startPoing, indexStrart)) != -1) 
           {
               
            indexStrart += startPoing.Length;
            indexEnd = htmlCode.IndexOf(endPoint, indexStrart);
            indexEnd += endPoint.Length;
            
            string urlCurrentImage = "";
           for (int i = indexStrart; i < indexEnd; i++)
           {
               urlCurrentImage += htmlCode[i];
           }

           indexStrart = indexEnd;
           
           //Debug.Log(urlCurrentImage);
           _urlImage.Add( Url + "/" + urlCurrentImage);;
           }

           OnGetUrlImage?.Invoke(www.result);

           /*foreach (var VARIABLE in _urlImage)
           {
               Debug.Log(VARIABLE);
           }*/
           
        }
    }
}
