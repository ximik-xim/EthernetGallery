using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonLoadScene : MonoBehaviour
{
    [SerializeField] 
    private int _sceneNuber = 1;
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SceneLoad);
    }

    private void SceneLoad()
    {
        SceneManager.LoadScene(_sceneNuber);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(SceneLoad);
    }
}
