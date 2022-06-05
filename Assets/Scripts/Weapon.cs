using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Weapon name
    public string weaponName;

    // Damage stats
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    // Weapon Level Unlocked
    public int weaponLevel = 1;
    public SpriteRenderer spriteRenderer;

    // Swing
    private Animator weaponAnim;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponAnim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Enemy")
        {
            // Apply damage to enemy hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        weaponAnim.SetTrigger("Swing");
        AudioManager.PlayClipStatic("Sword");
    }
}
