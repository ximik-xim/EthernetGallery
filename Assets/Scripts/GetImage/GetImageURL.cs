using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetImageURL : MonoBehaviour
{
    [SerializeField] 
    private RawImage _image;
    
    void Start()
    {
        StartCoroutine(GetTexture("http://data.ikppbb.com/test-task-unity-data/pics/1.jpg"));
        GetAsink();
        GetStatic();
    }

    private async void GetAsink()
    {
        _image.texture = await GetAsinkTexrure("http://data.ikppbb.com/test-task-unity-data/pics/1.jpg");
    }
    
    private async void GetStatic()
    {
        _image.texture = await GetStaticAsinkTexrure("http://data.ikppbb.com/test-task-unity-data/pics/1.jpg");
    }
    
   private IEnumerator GetTexture(string url) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) 
        {
            Debug.Log(www.error);
        }
        else 
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            _image.texture = myTexture;
        }
    }

   public IEnumerator GetTexture(string url, int number, Action<int, Texture2D> complite) {
     
       UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
       yield return www.SendWebRequest();

       if (www.result != UnityWebRequest.Result.Success) 
       {
           Debug.Log(www.error);
       }
       else 
       {
           complite.Invoke(number,DownloadHandlerTexture.GetContent(www));
       }
   }
   

    public async Task <Texture2D> GetAsinkTexrure(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            
        var asyncOp = www.SendWebRequest();

        while (asyncOp.isDone == false)
        {
            await Task.Delay(1000 / 30);
        }

        if (www.result != UnityWebRequest.Result.Success) 
        {
            Debug.Log(www.error);

            return null;
        }
        else
        {
            //return ((DownloadHandlerTexture) www.downloadHandler).texture;
            return DownloadHandlerTexture.GetContent(www);
        }
    }
    
    public static async Task <Texture2D> GetStaticAsinkTexrure(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            
            var asyncOp = www.SendWebRequest();

            while (asyncOp.isDone == false)
            {
                await Task.Delay(1000 / 30);
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);

                return null;
            }
            else
            {
                //return ((DownloadHandlerTexture) www.downloadHandler).texture;
                return DownloadHandlerTexture.GetContent(www);
            }
        }
    }
    
    
    public void GetAsinkTexrure(string url,int id, Action<int,UnityWebRequest.Result,Texture2D> action)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            
        var asyncOp = www.SendWebRequest();

        asyncOp.completed += test;
        
        void test(AsyncOperation obj)
        {
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Id = " + id + " " + www.error);

                action?.Invoke(id,www.result,null);
                return;
            }
            
            action?.Invoke(id,www.result,DownloadHandlerTexture.GetContent(www));
        }
    }

    
}
