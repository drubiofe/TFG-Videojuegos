using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{

    int totalWeapons = 1;
    public int currentWeaponIndex;

    public List<Weapon> weapons;
    public GameObject weaponHolder;
    public Weapon currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        weapons = new List<Weapon>();

        for (int i = 0; i < totalWeapons; i++)
        {
            // Add owned weapons to weapon list
            weapons.Add(weaponHolder.transform.GetChild(i).GetComponent<Weapon>());
            weapons[i].gameObject.SetActive(false);
        }

        weapons[0].gameObject.SetActive(true);
        currentWeapon = weapons[0];
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // If a new weapon is picked up, add it to the list
        totalWeapons = weaponHolder.transform.childCount;
        if (totalWeapons > weapons.Count) weapons.Add(weaponHolder.transform.GetChild(totalWeapons - 1).GetComponent<Weapon>());
        currentWeapon = weapons[currentWeaponIndex];
    }

    public void PreviousWeapon()
    {
        if (currentWeaponIndex > 0)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
            currentWeaponIndex -= 1;
            weapons[currentWeaponIndex].gameObject.SetActive(true);
        }
        else
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
            currentWeaponIndex = totalWeapons - 1;
            weapons[currentWeaponIndex].gameObject.SetActive(true);
        }
    }

    public void NextWeapon()
    {
        if (currentWeaponIndex < totalWeapons - 1)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
            currentWeaponIndex += 1;
            weapons[currentWeaponIndex].gameObject.SetActive(true);
        }
        else
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
            currentWeaponIndex = 0;
            weapons[currentWeaponIndex].gameObject.SetActive(true);
        }
    }
}
