using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionAnimations : MonoBehaviour
{
    public Animator animator;

    public bool walking;

    public bool idling;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (walking == true)

        {
            switch (GetComponentInParent<CharacterClass>().currentClass)
            {
                case CharacterClass.Class.Paladin:
                    animator.Play("Kallista_Walk");
                    break;
                case CharacterClass.Class.Barbarian:
                    animator.Play("Strannik_Walk");
                    break;
                case CharacterClass.Class.Sorcerer:
                    animator.Play("Sheva_Walk");
                    break;
            }
        }

        if (GetComponentInParent<NavMeshAgent>().velocity.x > 0 || GetComponentInParent<NavMeshAgent>().velocity.y > 0 ||
            GetComponentInParent<NavMeshAgent>().velocity.y > 0)
        {
            switch (GetComponentInParent<CharacterClass>().currentClass)
            {
                case CharacterClass.Class.Paladin:
                    animator.Play("Kallista_Walk");
                    break;
                case CharacterClass.Class.Barbarian:
                    animator.Play("Strannik_Walk");
                    break;
                case CharacterClass.Class.Sorcerer:
                    animator.Play("Sheva_Walk");
                    break;
            }

            walking = true;
        }

        if (GetComponentInParent<NavMeshAgent>().velocity.x == 0 && GetComponentInParent<NavMeshAgent>().velocity.y == 0 &&
            GetComponentInParent<NavMeshAgent>().velocity.y == 0)
        {
            walking = false;
        }

        if (GetComponentInParent<CompanionAIScript>().state == CompanionAIScript.CompanionState.Following && walking == false)
        {
            switch (GetComponentInParent<CharacterClass>().currentClass)
            {
                case CharacterClass.Class.Paladin:
                    animator.Play("Kallista_Idle");
                    break;
                case CharacterClass.Class.Barbarian:
                    animator.Play("Strannik_Idle");
                    break;
                case CharacterClass.Class.Sorcerer:
                    animator.Play("Sheva_Idle");
                    break;
            }
        }
        if (Vector3.Distance(GetComponentInParent<NavMeshMovement>().meshAgent.destination, gameObject.transform.position) > 5f)
        {
            walking = true;
        }  
    }
    public void Die()
    {
        switch (GetComponentInParent<CharacterClass>().currentClass)
        {
            case CharacterClass.Class.Paladin:
                animator.Play("Kallista_Die");
                break;
            case CharacterClass.Class.Barbarian:
                animator.Play("Strannik_Die");
                break;
            case CharacterClass.Class.Sorcerer:
                animator.Play("Sheva_Die");
                break;
        }
    }

    public void Cast()
    {
            switch (GetComponentInParent<CharacterClass>().currentClass)
            {
                case CharacterClass.Class.Paladin:
                    animator.Play("Kallista_Cast");
                    break;
                case CharacterClass.Class.Sorcerer:
                    animator.Play("Sheva_Cast");
                    break;
            }
        }
      
    


    public void Attack()
    {
        switch (GetComponentInParent<CharacterClass>().currentClass)
        {
            case CharacterClass.Class.Paladin:
                animator.Play("Kallista_Attack");
                break;
            case CharacterClass.Class.Barbarian:
                animator.Play("Strannik_Attack");
                break;
            case CharacterClass.Class.Sorcerer:
                animator.Play("Sheva_Attack");
                break;
        }
    }

    public void WildSpin()
    {
        animator.Play("Strannik_WildSpin");
    }
}
