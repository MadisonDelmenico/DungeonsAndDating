using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : MonoBehaviour
{
    public NavMeshAgent meshAgent;
    public TargettingEnemies target;
    public CompanionFollowScript companionAI;
    public PlayerAI playerAI;

    // Start is called before the first frame update
    void Start()
    {
        //checking to see if it has all the components
        if (gameObject.CompareTag("Player"))
        {
            meshAgent = GetComponent<NavMeshAgent>();
        }

        if (GetComponent<CompanionFollowScript>())
        {
            companionAI = GetComponent<CompanionFollowScript>();
        }
        target = GetComponent<TargettingEnemies>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (gameObject.CompareTag("Player"))
                {
                    meshAgent.destination = hit.point;
                }
                //if you clicked on an enemy
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Attack();
                    //target the enemy you clicked on
                    target.target = hit.collider.gameObject;
                }

                //If you click on anything OTHER than an enemy
                else
                {   //if you're more than 5m away from the enemy, disengage & move on
                    if (target.Distance > 5)
                    {
                        Disengage();
                    }

                }
            }
        }
    }
    public void Attack()
    {
        //turn on the targettingEnemies component
        target.enabled = true;



        //if you are the player, attack!
        if (GetComponent<PlayerAI>())
        {
            GetComponent<PlayerAI>().isAttacking = true;
            // target.target.GetComponent<Material>().color = Color.red;
        }
    }

    public void Disengage()
    {

        //if you are a companion
        if (GetComponent<CompanionFollowScript>())
        {
            //if you are attacking
            if (companionAI.isAttacking == true)
            {
                //you are no longer attacking. Disable the Targetting Enemies component and follow the player.
                target.enabled = false;
                // target.target.GetComponent<Material>().color = new Color(0,0,0,0);
                companionAI.isAttacking = false;
                companionAI.isFollowingPlayer = true;
                Debug.Log("I'm no longer attacking");

            }
        }
        //if i have the targetting enemies component
        if (GetComponent<TargettingEnemies>())

        //if i am the player
        {
            if (GetComponent<PlayerAI>())
            {
                //if i am attacking
                if (GetComponent<PlayerAI>().isAttacking == true)
                {
                    //stop attacking and disable the targetting enemies component
                    GetComponent<PlayerAI>().isAttacking = false;
                    target.enabled = false;
                }
            }
        }
    }


}
