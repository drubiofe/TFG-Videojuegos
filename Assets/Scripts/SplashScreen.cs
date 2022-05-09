using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public int savedScene;
    string[] data;

    protected virtual void Start()
    {
        data = PlayerPrefs.GetString("SaveState").Split('|');
        savedScene = int.Parse(data[0]);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

