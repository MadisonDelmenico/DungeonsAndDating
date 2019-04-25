using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animationscript : MonoBehaviour
{
    public bool walking;
    public bool idling;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (walking== true)
        {
            animator.Play("Player_Walk");
        }
        if (GetComponent<NavMeshAgent>().velocity.x >0 || GetComponent<NavMeshAgent>().velocity.y > 0 || GetComponent<NavMeshAgent>().velocity.y > 0)
        {
            animator.Play("Player_Walk");
            walking = true;
        }

        if (GetComponent<NavMeshAgent>().velocity.x == 0 && GetComponent<NavMeshAgent>().velocity.y == 0 && GetComponent<NavMeshAgent>().velocity.y == 0)
        {
            walking = false;
        }

        if (GetComponent<PlayerAI>().isAttacking  == false && walking == false)
        {
            animator.Play("Player_Idle");
        }

        if ( Vector3.Distance(GetComponent<NavMeshMovement>().meshAgent.destination, gameObject.transform.position) > 5f)
        {
            walking = true;
        }

        if (Vector3.Distance(GetComponent<NavMeshMovement>().meshAgent.destination, gameObject.transform.position) <2f)
        {
            
        }

        if (GetComponent<PlayerAI>().isAttacking)
        {
            Attack();
        }
    }

    public void Die()
    {
        animator.Play("Player_Die");
    }

    public void Cast()
    {
        animator.Play("Player_Cast");
    }

    public void Attack()
    {
        animator.Play("Player_Attack");
    }

    public void WildSpin()
    {
        animator.Play("Player_WildSpin");
    }
}
