using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mobile
{
    // Experience
    public int xpValue = 1;

    //Logic
    public float speed = 0.8f;
    public float detectionRange = 1;
    public float chaseRange = 1.5f;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector2 startingPosition;
    private bool isFlipped = false;

    // Hitbox
    private ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Rigidbody2D rb;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        rb = transform.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        // Change sprite animation depending on enemy movement
        if (rb.velocity.x.Equals(0) && rb.velocity.y.Equals(0))
        {
            animator.SetBool("Moving", false);
        }
        else animator.SetBool("Moving", true);

        // Swap sprite direction
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (rb.velocity.x > 0 && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (rb.velocity.x < 0 && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }

        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Check if player is in range of enemy and start chase
        if (Vector2.Distance(playerTransform.position, rb.position) < detectionRange)
        {
            Vector2 playerDirection = ((Vector2)playerTransform.position - rb.position).normalized;
            rb.velocity = new Vector2(playerDirection.x, playerDirection.y) * speed;
        }

        // If player escaped enemy, enemey returns to its starting position
        if (Vector2.Distance(playerTransform.position, rb.position) > chaseRange)
        {
            Vector2 moveToStart = Vector2.MoveTowards(transform.position, startingPosition, Time.deltaTime * speed);
            rb.MovePosition(moveToStart);
        }
    }
    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.green, transform.position, Vector3.up * 50, 1f);
    }

    protected virtual void ChangeSpeed(float value)
    {
        speed *= value;
    }

    IEnumerator IceEffect()
    {
        ChangeSpeed(0.5f);
        GameManager.instance.ShowText("Speed reduced!", 30, Color.cyan, transform.position, Vector3.up * 50, 1f);
        yield return new WaitForSeconds(5.0f);
        ChangeSpeed(2f);
        GameManager.instance.ShowText("Speed reduced!", 30, Color.cyan, transform.position, Vector3.up * 50, 1f);
    }

    IEnumerator FireEffect(Damage dmg)
    {
        ReceiveDamage(dmg);
        yield return new WaitForSeconds(1.0f);
        ReceiveDamage(dmg);
        yield return new WaitForSeconds(1.0f);
        ReceiveDamage(dmg);
        yield return new WaitForSeconds(1.0f);
        ReceiveDamage(dmg);
    }
}
