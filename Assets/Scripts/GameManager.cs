using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(gameInterface);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> xpTable;

    // References
    public Player player;
    public WeaponHolder weapons;
    public AbilityHolder[] abilityHolder;
    public FloatingTextManager floatingTextManager;
    public GameObject gameInterface;
    public Animator deathScreenAnim;

    // Tracker
    private int savedLevel = 1;
    public int xp;
    public bool bossBeaten = false;

    // Experience system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while(xp >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
                return r;
        }

        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xpToLevel = 0;

        while (r < level)
        {
            xpToLevel += xpTable[r];
            r++;
        }

        return xpToLevel;
    }
    public void GrantXp(int grantedXp)
    {
        int currentLevel = GetCurrentLevel();
        xp += grantedXp;
        if (currentLevel < GetCurrentLevel())
            OnLevelUp();
    }
    private void OnLevelUp()
    {
        player.OnLevelUp();
    }

    // Death screen
    public void Respawn()
    {
        AudioManager.PlayClipStatic("Click");
        deathScreenAnim.SetTrigger("Hide");
        SceneManager.LoadScene(0);
        player.Respawn();
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // On scene load
    public void OnSceneLoad(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    // Save game
    public void SaveState()
    {
        savedLevel = SceneManager.GetActiveScene().buildIndex+1;
        string s = $"{savedLevel}|{xp}|{bossBeaten}";

        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Player experience
        xp = int.Parse(data[1]);
        if(GetCurrentLevel() != 1)
        player.SetLevel(GetCurrentLevel());

        // Boss beaten check
        bossBeaten = bool.Parse(data[2]);
    }

    public void ResetState()
    {
        string s = $"{0}|{xp}|{bossBeaten}";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void ResetXp()
    {
        savedLevel = 1;
        xp = 0;
        bossBeaten = false;
        string s = $"{savedLevel}|{xp}|{bossBeaten}";
        player.hitPoint = 10;
        player.maxHP = 10;

        PlayerPrefs.SetString("SaveState", s);
    }
}
