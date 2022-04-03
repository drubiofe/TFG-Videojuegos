using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public int savedScene;
    string[] data;

    private void Start()
    {
        data = PlayerPrefs.GetString("SaveState").Split('|');
        savedScene = int.Parse(data[0]);
    }
    public void PlayGame()
    {
        // If there is a saved scene, load it. Else, load the first level
        if (savedScene > 1)
            SceneManager.LoadScene(savedScene);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

