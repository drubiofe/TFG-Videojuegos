using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public int savedScene;
    protected string[] data;

    protected virtual void Start()
    {
        GameManager.instance.gameInterface.SetActive(false);
        data = PlayerPrefs.GetString("SaveState").Split('|');
        savedScene = int.Parse(data[0]);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

