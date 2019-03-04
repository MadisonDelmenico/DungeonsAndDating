using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;

public class CompanionAIScript : MonoBehaviour
{
    [HideInInspector]
    NavMeshAgent meshAgent;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    private GameObject[] NumberofCompanions;
    [HideInInspector]
    public int companionNumber;

    [HideInInspector]
    public GameObject companionTarget;
    [HideInInspector]
    private Transform companionTargetTransform;
    [HideInInspector]
    public float rotspeed;
    [HideInInspector]
    CharacterClass companionClass;
    [HideInInspector]
    private float myMaxHealth;
    [HideInInspector]
    private float playerMaxHealth;

    public float castTime;

    public enum CompanionState { Attacking, Casting, Following, Idle }

    [Header("Status")]
    public CompanionState state;
    public bool isFollowingPlayer;
    public bool isAttacking;

    [Header("Distances")]
    public float distanceFromPlayer;
    public float meleeDistance;
    public float rangedDistance;
    public float disengageDistance;
    [Header("Debug text item")]
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        isFollowingPlayer = true;
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
        playerMaxHealth = player.GetComponent<Health>().maxHealth;
        companionClass = GetComponent<CharacterClass>();

        // Finding the number of companions currently in the dungeon, adding them to an array of companions
        NumberofCompanions = GameObject.FindGameObjectsWithTag("Companion");

        // Setting the companion number for each companion, 0 by default
        for (int i = 0; i < NumberofCompanions.Length; i++)
        {
            if (NumberofCompanions[i] == gameObject)
            {
                companionNumber = i;
                break;
            }
        }

        // Setting the transforms for companion target for quick referencing
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

        // If the player is being attacked, attack
        if (player.GetComponent<PlayerAI>().beingattacked == true)
        {
            state = CompanionState.Attacking;
            GetComponent<NavMeshMovement>().Attack();
        }

        // Check the distance between myself and the player
        distanceFromPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

        switch (state)
        {
            // If i'm attacking
            case CompanionState.Attacking:
                text.text = "Attacking";

                // If the player is past my disengage distance
                if (distanceFromPlayer > disengageDistance)
                {
                    // Stop fighting, go back to following the player
                    GetComponent<NavMeshMovement>().Disengage();
                }

                // If i have a target
                if (GetComponent<TargettingEnemies>().target != null)
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
                                AttackMelee();
                            }
                            break;
                        case CharacterClass.Class.Paladin:
                            //  ___  _   _      _   ___ ___ _  _     _   ___ ___ _    ___ _____ ___ ___ ___ 
                            // | _ \/_\ | |    /_\ |   \_ _| \| |   /_\ | _ )_ _| |  |_ _|_   _|_ _| __/ __|
                            // |  _/ _ \| |__ / _ \| |) | || .` |  / _ \| _ \| || |__ | |  | |  | || _|\__ \
                            // |_|/_/ \_\____/_/ \_\___/___|_|\_| /_/ \_\___/___|____|___| |_| |___|___|___/

                            // If i have low health, heal myself
                            if (GetComponent<Health>().health <= (myMaxHealth / 4))
                            {
                                state = CompanionState.Casting;
                                castTime = 3;
                                GetComponent<CharacterActions>().DoAction(CharacterActions.Action.Revitalize, gameObject);
                            }
                            // If i dont have low health, but the player does, heal the player
                            else if (player.GetComponent<Health>().health <= (playerMaxHealth / 4))
                            {
                                state = CompanionState.Casting;
                                castTime = 3;
                                GetComponent<CharacterActions>().DoAction(CharacterActions.Action.Revitalize, player.gameObject);
                            }
                            // If neither the player or myself have low health, attack
                            else
                            {
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
                                AttackRanged();
                            }
                            break;
                        default:
                            // If i have more than 1/4 of my max health, ATTACK!
                            if (GetComponent<Health>().health > (myMaxHealth / 4))
                            {
                                AttackMelee();
                            }
                            break;
                    }
                }
                // If the player is targeting somehting other than itself
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
                break;

            // If i'm casting
            case CompanionState.Casting:
                text.text = "Casting";
                if (castTime <= 0)
                {
                    state = CompanionState.Attacking;
                }
                break;

            // If i'm following the player
            case CompanionState.Following:
                text.text = "Following";
                // If my target isnt the player, then it must be an enemy
                if (GetComponent<TargettingEnemies>().target != player)
                {
                    // So switch to attacking
                    state = CompanionState.Attacking;
                }
                // Otherwise
                else
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

                    // Look in the same direction as the player
                    transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, Time.deltaTime * rotspeed);
                }
                break;

            // If i'm idle
            case CompanionState.Idle:
                text.text = "Idle";
                state = CompanionState.Following;
                break;
            default:
                break;
        }
    }

    public void AttackMelee()
    {
        if (GetComponent<TargettingEnemies>().enabled)
        {
            if (GetComponent<TargettingEnemies>().target != null)
            {
                if (player.GetComponent<TargettingEnemies>().target != player)
                {
                    GetComponent<TargettingEnemies>().target = player.GetComponent<TargettingEnemies>().target;
                    state = CompanionState.Attacking;

                    // Move to x meters away from the target
                    MoveToDistance(meleeDistance);

                    // Look at the enemy
                    transform.LookAt(GetComponent<TargettingEnemies>().target.transform);

                    // Attack the enemy
                    text.text = "I'm attacking " + GetComponent<TargettingEnemies>().target.name;
                    GetComponent<CharacterActions>().DoAction(CharacterActions.Action.Basic, GetComponent<TargettingEnemies>().target);
                }
            }
        }
    }

    public void AttackRanged()
    {
        if (GetComponent<TargettingEnemies>().enabled)
        {
            if (GetComponent<TargettingEnemies>().target != null)
            {
                if (player.GetComponent<TargettingEnemies>().target != player)
                {
                    GetComponent<TargettingEnemies>().target = player.GetComponent<TargettingEnemies>().target;
                    state = CompanionState.Attacking;

                    // Move to x meters away from the target
                    MoveToDistance(rangedDistance);

                    // Look at the enemy
                    transform.LookAt(GetComponent<TargettingEnemies>().target.transform);

                    // Attack the enemy
                    text.text = " I'm attacking " + GetComponent<TargettingEnemies>().target.name;
                    GetComponent<CharacterActions>().DoAction(CharacterActions.Action.Basic, GetComponent<TargettingEnemies>().target);
                }
            }
        }
    }

    public void MoveToDistance(float distance)
    {
        text.text = "Moving for Attack";
        Vector3 direction = transform.position - GetComponent<TargettingEnemies>().target.transform.position;
        direction.Normalize();
        Vector3 targetPos = GetComponent<TargettingEnemies>().target.transform.position + direction * distance;
        meshAgent.destination = targetPos;
    }
}
