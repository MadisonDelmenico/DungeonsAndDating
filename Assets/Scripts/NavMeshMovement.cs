using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavMeshMovement : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent meshAgent;
    [HideInInspector]
    public CompanionAIScript companionAI;
    [HideInInspector]
    public PlayerAI playerAI;
    [HideInInspector]
    public TargettingEnemies target;

    [Header("Debug Text Object")]
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        // if I'm the player
        if (gameObject.CompareTag("Player"))
        {
            playerAI = GetComponent<PlayerAI>();
        }
        target = GetComponent<TargettingEnemies>();

        // if I'm a companion
        if (GetComponent<CompanionAIScript>())
        {
            companionAI = GetComponent<CompanionAIScript>();
            playerAI = companionAI.player.GetComponent<PlayerAI>();
            text = companionAI.text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //only players and companions need this stuff
        if (gameObject.CompareTag("Enemy") == false)
        {
            // if left mouse button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                // if the area pressed has a collider
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9))
                {
                    if (GetComponent<PlayerAI>())
                    {
                        if (hit.collider.gameObject.CompareTag("Companion"))
                        {
                            target.friendlyTarget = hit.collider.gameObject;
                            target.enabled = false;
                        }
                    }

                    // If im the player, move to the clicked point
                    if (gameObject.CompareTag("Player"))
                    {
                        meshAgent.destination = hit.point;
                    }

                    // if the area clicked was an enemy
                    if (hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        // target the enemy you clicked on
                        target.target = hit.collider.gameObject;

                        // attack the target
                        Attack();
                    }
                    else // If the area clicked was anything OTHER than an enemy
                    {
                        // if you are the player, disenage
                        if (GetComponent<PlayerAI>())
                        {
                            Disengage();
                        }
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
            companionAI.isAttacking = false;
            companionAI.isFollowingPlayer = true;
            text.text = " I'm disengaging";
        }

        // if i am the player
        if (GetComponent<PlayerAI>())
        {
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
