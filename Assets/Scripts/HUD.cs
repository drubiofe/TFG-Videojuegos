using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Text healthText;
    public RectTransform healthBar;
    public Animator hudAnim;

    // Update is called once per frame
    void Update()
    {
        // Health Bar
        int currentHealth = GameManager.instance.player.hitPoint;
        int maxHealth = GameManager.instance.player.maxHP;
        float completionRatioHp = (float)currentHealth / (float)maxHealth;
        healthBar.localScale = new Vector3(completionRatioHp, 1, 1);
        healthText.text = currentHealth.ToString() + "/" + maxHealth;
    }
}
