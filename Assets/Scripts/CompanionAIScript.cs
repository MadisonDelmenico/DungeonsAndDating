using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;
using VIDE_Data;
using MiniJSON_VIDE;

public class CompanionAIScript : MonoBehaviour
{
    public bool isRecruited;

    private NavMeshAgent meshAgent;
    [HideInInspector]
    public GameObject player;
    private GameObject[] allCompanions;
    [HideInInspector]
    public int companionNumber;
    [HideInInspector]
    public GameObject[] healthpotions;

    [HideInInspector]
    public GameObject companionTarget;
    private Transform companionTargetTransform;
    [HideInInspector]
    public float rotspeed;

    private CharacterClass companionClass;
    private CharacterActions companionActions;
    private float myMaxHealth;

    public float castTime;

    public enum CompanionState { Attacking, Casting, Following, Idle }

    [Header("Status")]
    public CompanionState state;

    [Header("Distances")]
    public float distanceFromPlayer;
    public float meleeDistance;
    public float rangedDistance;
    public float disengageDistance;
    [Header("Debug text item")]
    public Text text;



    private GameObject friendlyTargetParticleEffect;

    private GameObject particle;


    // Start is called before the first frame update
    void Start()
    {

        isRecruited = false;
        state = CompanionState.Following;
        rotspeed = 3f;
        distanceFromPlayer = 0;
        meleeDistance = 1.0f;
        rangedDistance = 5f;
        disengageDistance = 7f;
        meshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Making sure the companions know what their health is
        myMaxHealth = GetComponent<Health>().maxHealth;
        companionClass = GetComponent<CharacterClass>();
        companionActions = GetComponent<CharacterActions>();

        // Finding the number of companions currently in the dungeon, adding them to an array of companions
        allCompanions = GameObject.FindGameObjectsWithTag("Companion");

        //companions know where the health potions are
        healthpotions = GameObject.FindGameObjectsWithTag("Healthpotion");

        // Setting the companion number for each companion, 0 by default
        for (int i = 0; i < allCompanions.Length; i++)
        {
            if (allCompanions[i] == gameObject)
            {
                companionNumber = i;
                break;
            }
        }
        // Setting the transforms for companion target for quick referencing
        switch (companionNumber)
        {
            case 0:
                if (allCompanions.Length == 2)
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


        //setting particle effects
        friendlyTargetParticleEffect = Resources.Load<GameObject>("ParticleEffects/ParticleFriendly");
    }

    // Update is called once per frame
    void Update()
    {
        castTime -= Time.deltaTime;

        // If the player is being attacked, attack
        if (player.GetComponent<PlayerAI>().beingattacked == true)
        {
            state = CompanionState.Attacking;
            MoveToAttackRange();
            GetComponent<NavMeshMovement>().Attack();
        }

        // Check the distance between myself and the player
        distanceFromPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

        switch (state)
        {
            // If i'm attacking
            case CompanionState.Attacking:
                if (text != null)
                {
                    text.text = "Attacking";
                }


                // If the player is past my disengage distance
                if (distanceFromPlayer > disengageDistance)
                {
                    // Stop fighting, go back to following the player
                    GetComponent<NavMeshMovement>().Disengage();
                }

                // If i have a target and its an enemy
                if (GetComponent<TargettingEnemies>().target != null && GetComponent<TargettingEnemies>().target.CompareTag("Enemy"))
                {
                    if (GetComponent<TargettingEnemies>().enabled)
                    {
                        // Look at them
                        transform.rotation = Quaternion.Lerp(transform.rotation, GetComponent<TargettingEnemies>().target.transform.rotation, Time.deltaTime * rotspeed);
                    }

                    switch (companionClass.currentClass)
                    {
                        case CharacterClass.Class.Barbarian:
                            //  ___   _   ___ ___   _   ___ ___   _   _  _     _   ___ ___ _    ___ _____ ___ ___ ___ 
                            // | _ ) /_\ | _ \ _ ) /_\ | _ \_ _| /_\ | \| |   /_\ | _ )_ _| |  |_ _|_   _|_ _| __/ __|
                            // | _ \/ _ \|   / _ \/ _ \|   /| | / _ \| .` |  / _ \| _ \| || |__ | |  | |  | || _|\__ \
                            // |___/_/ \_\_|_\___/_/ \_\_|_\___/_/ \_\_|\_| /_/ \_\___/___|____|___| |_| |___|___|___/

                            // If i have more than 1/4 of my max health, ATTACK!
                            if (GetComponent<Health>().health > (myMaxHealth / 4))
                            {
                                MoveToAttackRange();
                                if (companionActions.wildSpinCooldown <= 0)
                                {
                                    companionActions.BeginCasting(CharacterActions.Action.WildSpin, GetComponent<TargettingEnemies>().target);
                                }
                                else
                                {
                                    AttackMelee();
                                }
                            }
                            break;
                        case CharacterClass.Class.Paladin:
                            //  ___  _   _      _   ___ ___ _  _     _   ___ ___ _    ___ _____ ___ ___ ___ 
                            // | _ \/_\ | |    /_\ |   \_ _| \| |   /_\ | _ )_ _| |  |_ _|_   _|_ _| __/ __|
                            // |  _/ _ \| |__ / _ \| |) | || .` |  / _ \| _ \| || |__ | |  | |  | || _|\__ \
                            // |_|/_/ \_\____/_/ \_\___/___|_|\_| /_/ \_\___/___|____|___| |_| |___|___|___/

                            GameObject healTarget;

                            // If any of my allies have low health
                            if (CheckAllyHealth(out healTarget, 0.25f) && companionActions.revitalizeCooldown <= 0)
                            {
                                state = CompanionState.Casting;
                                castTime = 3;
                                companionActions.BeginCasting(CharacterActions.Action.Revitalize, healTarget);
                                // companionActions.DoAction(CharacterActions.Action.Revitalize, healTarget);
                            }
                            // If neither the player or myself have low health, attack
                            else
                            {
                                MoveToAttackRange();
                                AttackMelee();
                            }
                            break;

                        case CharacterClass.Class.Sorcerer:
                            //  ___  ___  ___  ___ ___ ___ ___ ___     _   ___ ___ _    ___ _____ ___ ___ ___ 
                            // / __|/ _ \| _ \/ __| __| _ \ __| _ \   /_\ | _ )_ _| |  |_ _|_   _|_ _| __/ __|
                            // \__ \ (_) |   / (__| _||   / _||   /  / _ \| _ \| || |__ | |  | |  | || _|\__ \
                            // |___/\___/|_|_\\___|___|_|_\___|_|_\ /_/ \_\___/___|____|___| |_| |___|___|___/

                            // If i have more than 1/4 of my max health, ATTACK!
                            if (GetComponent<Health>().health > (myMaxHealth / 4))
                            {
                                if (companionActions.elementalSphereCooldown <= 0)
                                {
                                    companionActions.BeginCasting(CharacterActions.Action.ElementalSphere, GetComponent<TargettingEnemies>().target);
                                }
                                else
                                {
                                    AttackRanged();
                                }
                            }
                            break;
                        default:
                            // If i have more than 1/4 of my max health, ATTACK!
                            if (GetComponent<Health>().health > (myMaxHealth / 4))
                            {
                                MoveToAttackRange();
                                AttackMelee();
                            }
                            break;
                    }
                }
                // If the player is targeting something other than itself
                else if (player.GetComponent<TargettingEnemies>().target != player)
                {
                    // Target the player's target
                    GetComponent<TargettingEnemies>().target = player.GetComponent<TargettingEnemies>().target;
                }
                // Otherwise
                else
                {
                    // Target the player and follow them
                    GetComponent<TargettingEnemies>().target = player;
                    state = CompanionState.Following;
                }

                if (GetComponent<Health>().canUseHealthPotions == true)
                {
                    if (GetComponent<Health>().health <= (GetComponent<Health>().maxHealth / 2))
                    {

                        foreach (var i in healthpotions)
                        {
                            if (Vector3.Distance(gameObject.transform.position, i.transform.position) < 20f)
                            {
                                if (i.GetComponent<PickupHealth>().isSpawned == true)
                                {
                                    GetComponent<CharacterActions>().DoAction(CharacterActions.Action.GetHealthPotion, i);
                                    break;
                                }

                            }
                        }
                    }
                }

                break;

            // If i'm casting
            case CompanionState.Casting:
                if (text != null)
                {
                    text.text = "Casting";
                }

                if (castTime <= 0)
                {
                    if (companionActions.preparedAction != CharacterActions.Action.None)
                    {
                        companionActions.FinishCasting();
                    }
                    if (GetComponent<TargettingEnemies>().target != player && GetComponent<TargettingEnemies>().target != null)
                    {
                        state = CompanionState.Attacking;
                        MoveToAttackRange();
                    }
                    else
                    {
                        state = CompanionState.Following;
                    }
                }
                break;

            // If i'm following the player
            case CompanionState.Following:
                if (text != null)
                {
                    text.text = "Following";
                }

                // If my target isnt the player and my target isnt null
                if (GetComponent<TargettingEnemies>().target != player && GetComponent<TargettingEnemies>().target != null)
                {
                    // Then it must be an enemy, so switch to attacking
                    state = CompanionState.Attacking;
                    MoveToAttackRange();
                }
                // Otherwise
                else
                {
                    if (GetComponent<NavMeshAgent>().enabled)
                    {
                        // Follow the player
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

                        if (companionClass.currentClass == CharacterClass.Class.Paladin)
                        {
                            GameObject healTarget;
                            // If i have low health, heal myself
                            if (CheckAllyHealth(out healTarget, 1.0f) && companionActions.revitalizeCooldown <= 0)
                            {
                                state = CompanionState.Casting;
                                castTime = 3;
                                companionActions.BeginCasting(CharacterActions.Action.Revitalize, healTarget);
                            }
                        }
                        // Look in the same direction as the player
                        transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, Time.deltaTime * rotspeed);
                    }
                }


                if (GetComponent<Health>().canUseHealthPotions == true)
                {
                    if (GetComponent<Health>().health <= (GetComponent<Health>().maxHealth / 2))
                    {

                        foreach (var i in healthpotions)
                        {
                            if (Vector3.Distance(gameObject.transform.position, i.transform.position) < 20f)
                            {
                                if (i.GetComponent<PickupHealth>().isSpawned == true)
                                {
                                    GetComponent<CharacterActions>().DoAction(CharacterActions.Action.GetHealthPotion, i);
                                    break;
                                }

                            }
                        }
                    }
                }

                break;

            // If i'm idle
            case CompanionState.Idle:
                if (text != null)
                {
                    text.text = "Idle";
                }

                if (GetComponent<Health>().canUseHealthPotions == true)
                {
                    if (GetComponent<Health>().health <= (GetComponent<Health>().maxHealth / 2))
                    {

                        foreach (var i in healthpotions)
                        {
                            if (Vector3.Distance(gameObject.transform.position, i.transform.position) < 20f)
                            {
                                if (i.GetComponent<PickupHealth>().isSpawned == true)
                                {
                                    GetComponent<CharacterActions>().DoAction(CharacterActions.Action.GetHealthPotion, i);
                                    break;
                                }

                            }
                        }
                    }
                }

                state = CompanionState.Following;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Returns true if an ally needs healing
    /// </summary>
    /// <param name="toHeal">The GameObject that will be healed if its health is low</param>
    /// <param name="healLimit">What percentage of health the character must be under to be healed (Value 0 to 1)</param>
    /// <returns></returns>
    private bool CheckAllyHealth(out GameObject toHeal, float healLimit)
    {
        if (GetComponent<Health>().health < GetComponent<Health>().maxHealth * healLimit)
        {
            Debug.Log("I need to heal myself");
            toHeal = gameObject;
            return true;
        }
        if (player.GetComponent<Health>().health < player.GetComponent<Health>().maxHealth * healLimit)
        {
            Debug.Log("I need to heal the player");
            toHeal = player;
            return true;
        }
        foreach (GameObject i in allCompanions)
        {
            if (i.GetComponent<Health>().health < i.GetComponent<Health>().maxHealth * healLimit)
            {
                Debug.Log("I need to heal " + i.name);
                toHeal = i;
                return true;
            }
        }
        toHeal = gameObject;
        return false;
    }

    private void AttackMelee()
    {
        if (GetComponent<TargettingEnemies>().enabled)
        {
            if (GetComponent<TargettingEnemies>().target != null)
            {
                if (player.GetComponent<TargettingEnemies>().target != player)
                {
                    GetComponent<TargettingEnemies>().target = player.GetComponent<TargettingEnemies>().target;

                    // Look at the enemy
                    transform.LookAt(GetComponent<TargettingEnemies>().target.transform);

                    // Attack the enemy
                    if (text != null)
                    {
                        text.text = "I'm attacking " + GetComponent<TargettingEnemies>().target.name;
                    }

                    companionActions.DoAction(CharacterActions.Action.Basic, GetComponent<TargettingEnemies>().target);
                }
            }
        }
    }

    private void AttackRanged()
    {
        if (GetComponent<TargettingEnemies>().enabled)
        {
            if (GetComponent<TargettingEnemies>().target != null)
            {
                if (player.GetComponent<TargettingEnemies>().target != player)
                {
                    GetComponent<TargettingEnemies>().target = player.GetComponent<TargettingEnemies>().target;

                    // Look at the enemy
                    transform.LookAt(GetComponent<TargettingEnemies>().target.transform);

                    // Attack the enemy
                    if (text != null)
                    {
                        text.text = " I'm attacking " + GetComponent<TargettingEnemies>().target.name;
                    }

                    companionActions.DoAction(CharacterActions.Action.Basic, GetComponent<TargettingEnemies>().target);
                }
            }
        }
    }

    public void MoveToAttackRange()
    {
        float distance;

        if (companionClass.currentClass == CharacterClass.Class.Sorcerer)
        {
            distance = rangedDistance;
        }
        else
        {
            distance = meleeDistance;
        }

        Vector3 direction = transform.position - GetComponent<TargettingEnemies>().target.transform.position;
        direction.Normalize();
        Vector3 targetPos = GetComponent<TargettingEnemies>().target.transform.position + direction * distance;
        meshAgent.destination = targetPos;
    }



    #region Particles

    public void SpawnTargetCircle()
    {
        particle = Instantiate(friendlyTargetParticleEffect, new Vector3(gameObject.transform.position.x, 0.05f, gameObject.transform.position.z), Quaternion.Euler(-90,0,0));
        particle.GetComponent<ParticleFollow>().target = gameObject.transform;
       
    }

    public void DestroyTargetCircle()
    {
        Destroy(particle, 0f);
    }

    #endregion

}
