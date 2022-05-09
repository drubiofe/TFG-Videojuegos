using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loadout : SplashScreen
{
    // Text fields
    public Text playerLevel, weaponName, weaponDamage, weaponPush, weaponLevel, powerName, powerDescription, powerLevel;

    // Logic
    Weapon[] weapons;
    Ability[] abilities;
    private int currentWeaponIndex, totalWeapons, currentAbilityIndex, totalAbilities, currentLevel;

    // Visual fields
    public Image weaponSprite;
    public Image powerSprite;

    protected override void Start()
    {
        base.Start();
        currentLevel = GameManager.instance.GetCurrentLevel();
        playerLevel.text = currentLevel.ToString();
        weapons = Resources.LoadAll<Weapon>("Prefabs/Weapons");
        currentWeaponIndex = 0;
        totalWeapons = weapons.Length;
        abilities = Resources.LoadAll<Ability>("Abilities");
        currentAbilityIndex = 0;
        totalAbilities = abilities.Length;
    }

    private void Update()
    {
        // Always update weapon stats because the weapon can be changed from menu
        WeaponUpdate();
        PowerUpdate();
        //ability = GameManager.instance.player.GetComponents<AbilityHolder>();
    }

    private void PowerUpdate()
    {
        powerName.text = abilities[currentAbilityIndex].name.ToString();
        powerDescription.text = abilities[currentAbilityIndex].description.ToString();
        powerSprite.sprite = abilities[currentAbilityIndex].sprite;
        powerLevel.text = abilities[currentAbilityIndex].abilityLevel.ToString();
    }

    private void WeaponUpdate()
    {
        weaponName.text = weapons[currentWeaponIndex].weaponName.ToString();
        weaponSprite.sprite = weapons[currentWeaponIndex].spriteRenderer.sprite;
        weaponDamage.text = weapons[currentWeaponIndex].damagePoint.ToString();
        weaponPush.text = weapons[currentWeaponIndex].pushForce.ToString();
        weaponLevel.text = weapons[currentWeaponIndex].weaponLevel.ToString();
    }

    public void PreviousWeapon()
    {
        if (currentWeaponIndex > 0)
        {
            currentWeaponIndex -= 1;
        }
        else
        {
            currentWeaponIndex = totalWeapons - 1;
        }
    }

    public void NextWeapon()
    {
        if (currentWeaponIndex < totalWeapons - 1)
        {
            currentWeaponIndex += 1;
        }
        else
        {
            currentWeaponIndex = 0;
        }
    }

    public void PreviousAbility()
    {
        if (currentAbilityIndex > 0)
        {
            currentAbilityIndex -= 1;
        }
        else
        {
            currentAbilityIndex = totalAbilities - 1;
        }
    }

    public void NextAbility()
    {
        if (currentAbilityIndex < totalAbilities - 1)
        {
            currentAbilityIndex += 1;
        }
        else
        {
            currentAbilityIndex = 0;
        }
    }

    public void PlayGame()
    {
        // If player can acquire the weapon/ability, give them to the player
        if ((weapons[currentWeaponIndex].weaponLevel <= currentLevel) && (abilities[currentAbilityIndex].abilityLevel <= currentLevel))
        {
            Instantiate(weapons[currentWeaponIndex].gameObject, GameManager.instance.weapons.transform).SetActive(true);
            GameManager.instance.abilityHolder[0].ability = abilities[currentAbilityIndex];
            // If there is a saved scene, load it. Else, load the first level
            if (savedScene > 1)
                SceneManager.LoadScene(savedScene);
            else
                SceneManager.LoadScene(1);
        }
    }
}
