using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostGiantMovement : StateMachineBehaviour
{

    public float speed = 0.2f;
    public float attackRange = 0.32f;

    Transform playerTransform;
    Rigidbody2D rb;
    FrostGiant fg;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameManager.instance.player.transform;
        rb = animator.GetComponent<Rigidbody2D>();
        fg = animator.GetComponent<FrostGiant>();
        fg.audioSource.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fg.LookAtPlayer();

        Vector2 playerDirection = ((Vector2)playerTransform.position - rb.position).normalized;
        rb.velocity = new Vector2(playerDirection.x, playerDirection.y) * speed;

        if (Vector2.Distance(playerTransform.position, rb.position) <= attackRange)
        {
            AudioManager.PlayClipStatic("GiantSwing");
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fg.audioSource.Stop();
        animator.ResetTrigger("Attack");
    }
}
