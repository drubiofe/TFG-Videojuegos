using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public int damagePoint = 1;
    public float pushForce = 2.0f;
    //public GameObject hitEffect;
    GameObject enemy;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        Destroy(gameObject);
        if (coll.transform.tag == "Enemy")
        {
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };
            enemy = coll.gameObject;
            enemy.SendMessage("FireEffect", dmg);
        }
    }
}