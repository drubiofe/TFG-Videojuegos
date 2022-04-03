using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mobile
{
    // Experience
    public int xpValue = 1;

    //Logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    private ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        xSpeed = 0.75f;
        ySpeed = 0.6f;
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        // Check if player is in range of enemy
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    // Enemy moves towards player (if not colliding)
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                // When chase stops, enemy moves towards starting position and has idle animation
                UpdateMotor(startingPosition - transform.position);
                animator.SetFloat("Speed", 0);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        // Check overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

           if(hits[i].tag == "Player")
            {
                collidingWithPlayer = true;
            }

            // Clean up the array
            hits[i] = null;
        }
    }
    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.green, transform.position, Vector3.up * 50, 1f);
    }
}
