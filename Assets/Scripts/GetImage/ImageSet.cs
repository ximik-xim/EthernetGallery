using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSet : MonoBehaviour
{
    [SerializeField] 
    private RawImage _image;
    public void SetImage(Texture2D texture)
    {
        _image.texture = texture;

    }
}
