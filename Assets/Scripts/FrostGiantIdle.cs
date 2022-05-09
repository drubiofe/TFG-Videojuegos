using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostGiantIdle : StateMachineBehaviour
{
    
    Transform playerTransform;
    Rigidbody2D rb;
    public float range = 1;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get player transform and frost giant rigidbody
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If player is in range of frost giant, start battle
        if (Vector2.Distance(playerTransform.position, rb.position) < range)
        {
            animator.SetTrigger("InRange");
            // Enable visual healthbar
            rb.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
