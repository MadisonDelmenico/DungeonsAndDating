using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionFollowScript : MonoBehaviour
{
    public bool isFollowingPlayer;
    NavMeshAgent meshAgent;
    private GameObject player;
    private GameObject[] NumberofCompanions;
    public int companionNumber;

    public GameObject companionTarget;
    private Transform companionTargetTransform;
    public float rotspeed;
    CharacterClass companionClass;
    private float myMaxHealth;
    private float playerMaxHealth;
    public float castTime;

    public bool isAttacking;



    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        isFollowingPlayer = true;
        rotspeed = 3f;
        meshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        //making sure the companions know what their health is
        myMaxHealth = GetComponent<Health>().maxHealth;
        playerMaxHealth = player.GetComponent<Health>().maxHealth;




        companionClass = GetComponent<CharacterClass>();

        //finding the number of companions currently in the dungeon, adding them to an array of companions
        NumberofCompanions = GameObject.FindGameObjectsWithTag("Companion");

        //setting the companion number for each companion, 0 by default
        for (int i = 0; i < NumberofCompanions.Length; i++)
        {
            if (NumberofCompanions[i] == this.gameObject)
            {
                companionNumber = i;
                break;
            }

        }
        //setting the transforms for companion target for quick referencing
        switch (companionNumber)
        {
            case 0:

                if (NumberofCompanions.Length == 2)
                {
                    companionTarget = GameObject.Find("CompanionTarget2");
                    break;
                }
                else
                {
                    companionTarget = GameObject.Find("CompanionTarget0");
                    break;
                }
            case 1:
                companionTarget = GameObject.Find("CompanionTarget1");
                break;
            case 2:
                companionTarget = GameObject.Find("CompanionTarget2");
                break;
            default:
                break;
        }
        companionTargetTransform = companionTarget.transform;
    }

    // Update is called once per frame
    void Update()
    {

        castTime -= Time.deltaTime;
        if (castTime <= 0)

        {
            isFollowingPlayer = true;
        }
        if (isAttacking == true)
        {
            isFollowingPlayer = false;
        }

        if (isFollowingPlayer == true)
        {

            switch (companionNumber)
            {
                case 0:
                    meshAgent.destination = companionTargetTransform.transform.position;
                    break;
                case 1:
                    meshAgent.destination = companionTargetTransform.transform.position;
                    break;
                case 2:
                    meshAgent.destination = companionTargetTransform.transform.position;
                    break;
                default:
                    break;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, Time.deltaTime * rotspeed);

        }
        if (isFollowingPlayer == false)
        {
            if (GetComponent<TargettingEnemies>().enabled == true)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, GetComponent<TargettingEnemies>().target.transform.rotation, Time.deltaTime * rotspeed);
            }
        }


        switch (companionClass.currentClass)
        {
            case CharacterClass.Class.Barbarian:


                break;
            case CharacterClass.Class.Paladin:
                Debug.Log("Checking Health for healing");
                if (GetComponent<Health>().health <= (myMaxHealth / 4))
                {
                    if (castTime <= 0)
                    {
                        castTime = 3;
                        isFollowingPlayer = false;

                        //i'm casting a spell and no longer following the player
                        //do heal -> target = myself;
                        Debug.Log("I'm healing myself!");

                    }
                    else
                    {

                        Debug.Log("I'm still casting!");
                    }

                }
                if (player.GetComponent<Health>().health > (playerMaxHealth / 4) && GetComponent<Health>().health > (myMaxHealth / 4))
                {
                    if (GetComponent<TargettingEnemies>().enabled == true)
                    {
                        if (player.GetComponent<TargettingEnemies>().target != null)
                        {
                            GetComponent<TargettingEnemies>().target = player.GetComponent<TargettingEnemies>().target;
                            isAttacking = true;
                            meshAgent.destination = GetComponent<TargettingEnemies>().target.transform.position;
                            Debug.Log("I'm attacking" + GetComponent<TargettingEnemies>().target.name);
                        }
                    }
                }
                if (player.GetComponent<Health>().health <= (playerMaxHealth / 4))
                {
                    if (castTime <= 0)
                    {
                        castTime = 3;
                        isFollowingPlayer = false;

                        //i'm casting a spell and no longer following the player
                        //do heal -> target = Player;
                        Debug.Log("I'm healing the player!");
                    }
                    else
                    {
                        Debug.Log("I'm still casting!");
                    }
                }


                break;
            case CharacterClass.Class.Polymath:
                break;
            case CharacterClass.Class.Sorcerer:
                break;
            default:
                break;
        }


    }
}
