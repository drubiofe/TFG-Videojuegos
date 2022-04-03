using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Text fields
    public Text playerNameMenu, levelText, hitpointText, xpText, weaponName, weaponDamage, weaponPush;
    public TextMeshProUGUI playerNameDeath;

    // Logic
    public static bool isPaused = false;
    public Animator menuAnim;
    public Image playerSprite;
    public Image weaponSprite;
    public RectTransform xpBar;
    public RectTransform healthBar;

    // Pause or resume game
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                UpdateStats();
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    // Update info
    public void UpdateStats()
    {
        // Player name
        playerNameMenu.text = playerNameDeath.text = GameManager.instance.player.playerName;

        // Weapon
        weaponName.text = GameManager.instance.weapon.weaponName;
        weaponSprite.sprite = GameManager.instance.weaponSprites[0];
        weaponDamage.text = GameManager.instance.weapon.damagePoint.ToString();
        weaponPush.text = GameManager.instance.weapon.pushForce.ToString();

        // Meta
        hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        // Health bar
        
        int currentHealth = GameManager.instance.player.hitPoint;
        int maxHealth = GameManager.instance.player.maxHP;
        float completionRatioHp = (float)currentHealth / (float)maxHealth;
        healthBar.localScale = new Vector3(completionRatioHp, 1, 1);
        hitpointText.text = currentHealth.ToString() + "/" + maxHealth;
        
        // XP bar
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if(currentLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.xp.ToString() + " total xp";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int previousLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currentLevelXp = GameManager.instance.GetXpToLevel(currentLevel);

            int XpDiff = currentLevelXp - previousLevelXp;
            int currentXpBar = GameManager.instance.xp - previousLevelXp;

            float completionRatioXp = (float)currentXpBar / (float)XpDiff;
            xpBar.localScale = new Vector3(completionRatioXp, 1, 1);
            xpText.text = currentXpBar.ToString() + "/" + XpDiff;
        }
    }
    private void PauseGame()
    {
        menuAnim.SetTrigger("Show");
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeGame()
    {
        menuAnim.SetTrigger("Hide");
        Time.timeScale = 1;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
