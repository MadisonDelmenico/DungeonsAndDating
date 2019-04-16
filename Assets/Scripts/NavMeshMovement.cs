using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavMeshMovement : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent meshAgent;
    [HideInInspector]
    public TargettingEnemies target;


    // Start is called before the first frame update
    void Start()
    {

        meshAgent = GetComponent<NavMeshAgent>();
        target = GetComponent<TargettingEnemies>();
        if (gameObject.CompareTag("Player"))
        {
            target.target = gameObject;
            target.friendlyTarget = gameObject;

        }

    }

    // Update is called once per frame
    void Update()
    {

        // Only the player needs this stuff
        if (gameObject.CompareTag("Player"))
        {
            // If left mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                // If the area clicked has a collider
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9))
                {
                    switch (hit.collider.gameObject.tag)
                    {
                        // If it was a companion that was clicked
                        case "Companion":
                            //make sure you aren't actively targeting an enemy
                            foreach (var i in gameObject.GetComponent<PlayerAI>().enemies)
                            {
                                if (i!= null)
                                {
                                    if (i.GetComponent<EnemyAI>().imTargetted == true)
                                    {
                                        i.GetComponent<EnemyAI>().DestroyTargetCircle();
                                    }
                                }
                             
                            }

                            //if you haven't selected the same companion twice
                            if (hit.collider.gameObject != target.friendlyTarget)
                            {
                                //spawn in the 'target' particles
                                hit.collider.gameObject.GetComponent<CompanionAIScript>().SpawnTargetCircle();
                            }
                            target.friendlyTarget = hit.collider.gameObject;
                            print("setting friendly target to" + hit.collider.name);
                            target.enabled = false;
                          
                            break;
                        // If it was an enemy that was clicked
                        case "Enemy":

                            foreach (var i in gameObject.GetComponent<PlayerAI>().enemies)
                            {
                                if (i != null)
                                {
                                    if (i.GetComponent<EnemyAI>().imTargetted == true)
                                    {
                                        i.GetComponent<EnemyAI>().DestroyTargetCircle();
                                    }
                                }
                                
                            }
                            //turn off the targeting for friendlies
                            if (target.friendlyTarget != gameObject)
                            {
                                target.friendlyTarget.GetComponent<CompanionAIScript>().DestroyTargetCircle();
                                target.friendlyTarget = gameObject;
                            }
                            //spawn in the 'target' particles
                            hit.collider.gameObject.GetComponent<EnemyAI>().SpawnTargetCircle();
                         
                            // Target the enemy you clicked on
                            target.target = hit.collider.gameObject;  

                            // Attack the target
                            Attack();
                            GetComponent<PlayerAI>().LookAt(target.transform.position);

                            break;
                            // Otherwise
                        default:
                            Disengage();
                            print("I'm moving");
                            meshAgent.destination = hit.point;

                            //if you were targeting a friendly, destroy the target particles
                            if (target.friendlyTarget.gameObject != gameObject)
                            {
                                target.friendlyTarget.GetComponent<CompanionAIScript>().DestroyTargetCircle();

                                //set the target back to yourself
                                target.friendlyTarget = gameObject;
                            }

                            //if you were targeting an enemy, destroy the target particles
                            if (target.target != gameObject && target.target != null)
                            {
                            target.target.GetComponent<EnemyAI>().DestroyTargetCircle();    
                            }
                          
                            break;
                    }
                }
            }
        }
    }

    public void Attack()
    {
        // turn on the targettingEnemies component
        target.enabled = true;

        // if you are the player, attack!
        if (GetComponent<PlayerAI>())
        {
            GetComponent<PlayerAI>().isAttacking = true;
            gameObject.GetComponent<CharacterActions>().DoAction(CharacterActions.Action.Basic, target.target);
        }
    }

    public void Disengage()
    {
        // if you are a companion
        if (GetComponent<CompanionAIScript>())
        {
            // you are no longer attacking. Disable the Targetting Enemies component and follow the player.
            target.enabled = false;
            // target.target.GetComponent<Material>().color = new Color(0,0,0,0);
            GetComponent<CompanionAIScript>().state = CompanionAIScript.CompanionState.Following;
        }

        // if i am the player
        if (GetComponent<PlayerAI>())
        {
            GetComponent<PlayerAI>().beingattacked = false;
            // if i am attacking
            if (GetComponent<PlayerAI>().isAttacking)
            {
                // stop attacking and disable the targetting enemies component
                GetComponent<PlayerAI>().isAttacking = false;
                target.enabled = false;
            }
        }
    }

    public void StopMoving()
    {
        meshAgent.destination = transform.position;
    }
}
