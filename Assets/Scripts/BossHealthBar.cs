using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public FrostGiant boss;
    public Text healthText;
    public RectTransform healthBar;

    private void Update()
    {
        int currentHealth = boss.hitPoint;
        int maxHealth = boss.maxHP;
        float completionRatioHp = (float)currentHealth / (float)maxHealth;
        healthBar.localScale = new Vector3(completionRatioHp, 1, 1);
        healthText.text = boss.hitPoint.ToString() + "/" + boss.maxHP;
    }
}
