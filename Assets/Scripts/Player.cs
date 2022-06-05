using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mobile
{
    public string playerName = "Godie";
    [HideInInspector]
    public bool isAlive = true;

    protected override void Start()
    {
        base.Start();
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        // If player is death, can't receive damage
        if (!isAlive) return;
        base.ReceiveDamage(dmg);
        AudioManager.PlayClipStatic("PlayerDamage");
    }

    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathScreenAnim.SetTrigger("Show");
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(isAlive) UpdateMotor(new Vector3(x, y, 0));
    }
    public void OnLevelUp()
    {
        maxHP += 2;
        GameManager.instance.ShowText("LEVEL UP!", 40, Color.green, transform.position, Vector3.up * 70, 2f);

    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
    public void GiveHP(int hpGiven)
    {
        // Don't give health if it's already max
        if (hitPoint == maxHP) return;
        hitPoint += hpGiven;
        // If health is above limit, set to max
        if (hitPoint > maxHP) hitPoint = maxHP;
        // Show hp given
        GameManager.instance.ShowText("+" + hpGiven + " hp", 30, Color.cyan, transform.position, Vector3.up * 50, 1f);
    }
    public void Respawn()
    {
        GameManager.instance.ResetState();
        isAlive = true;
        GiveHP(maxHP);
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
        gameObject.SetActive(false);
    }
}
