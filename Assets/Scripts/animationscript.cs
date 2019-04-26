using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animationscript : MonoBehaviour
{
    public bool walking;
    public bool idling;
    public bool casting;
    public bool whirlwinding;
    public Animator animator;
    public float animationtimer;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animationtimer = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {

        if (casting) 
        {
            Cast();
        }

        if (whirlwinding)
        {
            WildSpin();
        }

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

        if (GetComponent<PlayerAI>().isAttacking  == false && walking == false && GetComponent<PlayerAI>().state == PlayerAI.PlayerState.Idle)
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
        animator = GetComponentInChildren<Animator>();
        animator.Play("Player_Die");
    }

    public void Cast()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play("Player_Cast");
    }

    public void Attack()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play("Player_Attack");
    }

    public void WildSpin()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play("Player_WildSpin");
    }
}
