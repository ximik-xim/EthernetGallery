using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableOrDisableLoadScene : MonoBehaviour
{
    [SerializeField] 
    private int _sceneNumber = 1;
    [SerializeField] 
    private bool LoadEnable = default;
    [SerializeField] 
    private bool LoadDisable = default;
    
    private void OnEnable()
    {
        if (LoadEnable)
        {
            SceneManager.LoadScene(_sceneNumber);
        }
    }

    private void OnDisable()
    {
        if (LoadDisable)
        {
            SceneManager.LoadScene(_sceneNumber);
        }
    }
}
