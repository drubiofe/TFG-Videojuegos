using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Text fields
    public Text playerNameMenu, levelText, hitpointText, xpText, weaponName, weaponDamage, weaponPush;
    public Text[] powerName, powerDescription;
    public TextMeshProUGUI playerNameDeath;

    // Logic
    public static bool isPaused = false;
    public Animator menuAnim;
    private AbilityHolder[] abilities;

    // Visual fields
    public Image playerSprite;
    public Image weaponSprite;
    public Image[] powerSprite;
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
        // Always update weapon stats because the weapon can be changed from menu
        WeaponUpdate();
        abilities = GameManager.instance.player.GetComponents<AbilityHolder>();
    }

    // Update info
    public void UpdateStats()
    {
        // Player name
        playerNameMenu.text = playerNameDeath.text = GameManager.instance.player.playerName;

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

        // Powers
        for (int i = 0; i <= 2; i++)
        {
            if (abilities[i].ability != null)
            {
                powerName[i].text = abilities[i].ability.name.ToString();
                powerDescription[i].text = abilities[i].ability.description.ToString();
                powerSprite[i].sprite = abilities[i].ability.sprite;
                powerSprite[i].color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                powerName[i].text = "".ToString();
                powerDescription[i].text = "".ToString();
                powerSprite[i].color = new Color(0f, 0f, 0f, 0f);
            }
        }
    }

    private void WeaponUpdate()
    {
        weaponName.text = GameManager.instance.weapons.currentWeapon.weaponName;
        weaponSprite.sprite = GameManager.instance.weapons.currentWeapon.spriteRenderer.sprite;
        weaponDamage.text = GameManager.instance.weapons.currentWeapon.damagePoint.ToString();
        weaponPush.text = GameManager.instance.weapons.currentWeapon.pushForce.ToString();
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
