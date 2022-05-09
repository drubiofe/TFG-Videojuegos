using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : Collidable
{
    public int addHP = 1;

    private float cooldown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name != "Player")
            return;

        if(Time.time - lastHeal > cooldown)
        {
            lastHeal = Time.time;
            GameManager.instance.player.GiveHP(addHP);
        }
    }
}
