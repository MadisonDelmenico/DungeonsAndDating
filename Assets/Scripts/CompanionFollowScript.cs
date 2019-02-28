﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionFollowScript : MonoBehaviour
{

    NavMeshAgent meshAgent;
    public GameObject player;
    private GameObject[] NumberofCompanions;
    public int companionNumber;

    public GameObject companionTarget;
    private Transform companionTargetTransform;
    public float rotspeed;
    CharacterClass companionClass;
    private float myMaxHealth;
    private float playerMaxHealth;
    public float castTime;
    public bool isFollowingPlayer;
    public bool isAttacking;
    public float distanceFromPlayer;



    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        isFollowingPlayer = true;
        rotspeed = 3f;
        distanceFromPlayer = 0;
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
            if (NumberofCompanions[i] == gameObject)
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
        //check the distance between myself and the player
        distanceFromPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        //if im attacking
        if (isAttacking == true)
        {
            //and the distance between myself and the player is > 5m
            if (distanceFromPlayer > 5f)
            {
                //stop fighting, go back to following the player
                GetComponent<NavMeshMovement>().Disengage();
            }
        }
        //if im not casting, im following the player
        castTime -= Time.deltaTime;
        if (castTime <= 0)

        {
            isFollowingPlayer = true;
        }
        //if im attacking, im not following the player
        if (isAttacking)
        {
            isFollowingPlayer = false;
        }
        //if im following the player, head towards my assigned waypoint
        if (isFollowingPlayer)
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

            //look in the same direction as the player
            transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, Time.deltaTime * rotspeed);

        }
        //if im not following the player, look towards my target
        if (isFollowingPlayer == false)
        {
            if (GetComponent<TargettingEnemies>().enabled)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, GetComponent<TargettingEnemies>().target.transform.rotation, Time.deltaTime * rotspeed);
            }
        }


        switch (companionClass.currentClass)
        {
            //if i am a Barbarian
            case CharacterClass.Class.Barbarian:


                //  ___   _   ___ ___   _   ___ ___   _   _  _     _   ___ ___ _    ___ _____ ___ ___ ___ 
                // | _ ) /_\ | _ \ _ ) /_\ | _ \_ _| /_\ | \| |   /_\ | _ )_ _| |  |_ _|_   _|_ _| __/ __|
                // | _ \/ _ \|   / _ \/ _ \|   /| | / _ \| .` |  / _ \| _ \| || |__ | |  | |  | || _|\__ \
                // |___/_/ \_\_|_\___/_/ \_\_|_\___/_/ \_\_|\_| /_/ \_\___/___|____|___| |_| |___|___|___/
                                                                                        





                break;



            //if i am a Paladin
            case CharacterClass.Class.Paladin:

                 //       ___  _   _      _   ___ ___ _  _     _   ___ ___ _    ___ _____ ___ ___ ___ 
                //       | _ \/_\ | |    /_\ |   \_ _| \| |   /_\ | _ )_ _| |  |_ _|_   _|_ _| __/ __|
                //       |  _/ _ \| |__ / _ \| |) | || .` |  / _ \| _ \| || |__ | |  | |  | || _|\__ \
                //       |_|/_/ \_\____/_/ \_\___/___|_|\_| /_/ \_\___/___|____|___| |_| |___|___|___/



                Debug.Log("Checking Health for healing");

                //if my health is less than or equal to 1/4 my max health, HEAL MYSELF!
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
                //if the player has more than 1/4 of their max health and i have more than 1/4 of my max health, ATTACK!
                if (player.GetComponent<Health>().health > (playerMaxHealth / 4) && GetComponent<Health>().health > (myMaxHealth / 4))
                {
                    if (GetComponent<TargettingEnemies>().enabled)
                    {
                        if (player.GetComponent<TargettingEnemies>().target != player)
                        {
                            GetComponent<TargettingEnemies>().target = player.GetComponent<TargettingEnemies>().target;
                            isAttacking = true;
                            meshAgent.destination = GetComponent<TargettingEnemies>().target.transform.position;
                            transform.LookAt(GetComponent<TargettingEnemies>().target.transform);
                            Debug.Log(gameObject.name + ":" + " I'm attacking " + GetComponent<TargettingEnemies>().target.name);

                        }
                    }
                }
                //if the player has <= 1/4 of their max health and im not already casting, HEAL THE PLAYER!
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

            //if i am a polymath
            case CharacterClass.Class.Polymath:
                
                    //  ___  ___  _ __   ____  __   _ _____ _  _     _   ___ ___ _    ___ _____ ___ ___ ___ 
                    // | _ \/ _ \| |\ \ / /  \/  | /_\_   _| || |   /_\ | _ )_ _| |  |_ _|_   _|_ _| __/ __|
                    // |  _/ (_) | |_\ V /| |\/| |/ _ \| | | __ |  / _ \| _ \| || |__ | |  | |  | || _|\__ \
                    // |_|  \___/|____|_| |_|  |_/_/ \_\_| |_||_| /_/ \_\___/___|____|___| |_| |___|___|___/
                                                                                      

                break;

            //if i am a Sorcerer
            case CharacterClass.Class.Sorcerer:

                    //  ___  ___  ___  ___ ___ ___ ___ ___     _   ___ ___ _    ___ _____ ___ ___ ___ 
                    // / __|/ _ \| _ \/ __| __| _ \ __| _ \   /_\ | _ )_ _| |  |_ _|_   _|_ _| __/ __|
                    // \__ \ (_) |   / (__| _||   / _||   /  / _ \| _ \| || |__ | |  | |  | || _|\__ \
                    // |___/\___/|_|_\\___|___|_|_\___|_|_\ /_/ \_\___/___|____|___| |_| |___|___|___/
                                                                                

                break;


            default:
                break;
        }


    }
}
