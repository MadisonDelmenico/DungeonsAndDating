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
                            target.friendlyTarget = hit.collider.gameObject;
                            target.enabled = false;
                            break;
                        // If it was an enemy that was clicked
                        case "Enemy":
                            // Target the enemy you clicked on
                            target.target = hit.collider.gameObject;
                            // Attack the target
                            Attack();
                            break;
                        // Otherwise
                        default:
                            Disengage();
                            meshAgent.destination = hit.point;
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
}
