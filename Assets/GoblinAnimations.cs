using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinAnimations : MonoBehaviour
{
    public EnemyAI ai;
    public Animator animator;
    public NavMeshAgent navMeshAgent;

    public bool walking;
    public bool idling;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ai = GetComponent<EnemyAI>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.velocity.x > 0.0f || navMeshAgent.velocity.y > 0.0f || navMeshAgent.velocity.z > 0.0f)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }

        if (ai.currentState == EnemyAI.State.Idle)
        {
            animator.Play("Goblin_Idle");
        }
    }

    public void Die()
    {
        animator.Play("Goblin_Die");
    }

    public void Cast()
    {
        animator.Play("Goblin_Cast");
    }

    public void Attack()
    {
        animator.Play("Goblin_Attack");
    }
}
