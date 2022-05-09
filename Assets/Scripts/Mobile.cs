using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mobile : Damageable
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float xSpeed = 1.0f;
    protected float ySpeed = 0.8f;
    public Animator animator;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        // Reset moveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // Normalize vector to prevent added up diagonal speed
        moveDelta = moveDelta.normalized;

        // If character is moving, change sprite from idle to moving
        if (!moveDelta.magnitude.Equals(0))
            animator.SetFloat("Speed", 1);
        else
            animator.SetFloat("Speed", 0);

        // Swap sprite direction
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Add push vector
        moveDelta += pushDirection;

        // Reduce push force based on recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Collision"));
        if (hit.collider == null)
        {
            // Player movement on the y axis
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Collision"));
        if (hit.collider == null)
        {
            // Player movement on the x axis
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
