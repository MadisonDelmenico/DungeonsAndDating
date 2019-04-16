using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Patrolling,
        Attacking,
        Idle
    }

    public State currentState;
    public GameObject target;
    public Text text;
    public GameObject[] Enemies;
    public float[] Distances;
    public int enemiesArrayPosition;
    public bool helpMe;
    private float idleTimer;
    [FMODUnity.EventRef]
    public string alertSound;
    public GameObject player;
    public GameObject[] companions;
    public float[] companionDistances;
    public int companionArrayPosition;
    public float rangedDistance;
    public float meleeDistance;

    public GameObject[] healthpotions;

    private GameObject enemyTargetParticleEffect;
    [HideInInspector] public bool imTargetted;
    private GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        //Enemies are aware of the healthpotion locations
        healthpotions = GameObject.FindGameObjectsWithTag("Healthpotion");

        //Enemies are aware of all other enemies in the scene
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Distances = new float[Enemies.Length];
        companions = GameObject.FindGameObjectsWithTag("Companion");
        companionDistances = new float[companions.Length];

        //patrol on start if you have patrol points
        if (GetComponent<EnemyPatrols>().Waypoints.Length > 0)
        {
            currentState = State.Patrolling;
        }

        enemiesArrayPosition = 0;
        companionArrayPosition = 0;
        idleTimer = 0;
        text.text = currentState.ToString();
        player = GameObject.FindGameObjectWithTag("Player");


        //setting particle effects
        enemyTargetParticleEffect = Resources.Load<GameObject>("ParticleEffects/ParticleEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        idleTimer -= Time.deltaTime;

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
                            GetComponent<EnemyActions>().DoAction(EnemyActions.Action.GetHealthPotion, i);
                            break;
                        }

                    }
                }
            }
        }
        //figuring out how far companions are from me
        foreach (var i in companions)
        {
            for (companionArrayPosition = 0; companionArrayPosition < companions.Length; companionArrayPosition++)
            {
                if (companions[companionArrayPosition] != null)
                {
                    companionDistances[companionArrayPosition] = Vector3.Distance(transform.position, companions[companionArrayPosition].transform.position);
                }
            }
        }
        //set all the distances from this enemy and the other enemies in the scene
        foreach (var i in Enemies)
        {
            for (enemiesArrayPosition = 0; enemiesArrayPosition < Enemies.Length; enemiesArrayPosition++)
            {
                if (Enemies[enemiesArrayPosition] != null)
                {
                    Distances[enemiesArrayPosition] = Vector3.Distance(transform.position, Enemies[enemiesArrayPosition].transform.position);
                    if (i != null)
                    {
                        if (i.gameObject.GetComponent<EnemyAI>().helpMe == true && Distances[enemiesArrayPosition] < 5f)
                        {
                            MoveToAttackRange(i.gameObject.GetComponent<EnemyAI>().target);
                        }
                    }
                }
            }
        }
        switch (currentState)
        {
            case State.Idle:
                if (idleTimer <= 0)
                {
                    currentState = State.Patrolling;
                }

                text.text = currentState.ToString();
                break;
            case State.Patrolling:
                GetComponent<EnemyPatrols>().Patrol();
                target = gameObject;
                GetComponent<NavMeshAgent>().speed = 1.5f;
                checkForEnemies();

                text.text = currentState.ToString();
                break;
            case State.Attacking:
                if (target != gameObject)
                {
                    // GetComponent<NavMeshMovement>().meshAgent.destination = target.transform.position;

                    target.gameObject.transform.LookAt(target.transform);

                    // Is the enemy is greater than 5m from where im patrolling
                    if (Vector3.Distance(GetComponent<EnemyPatrols>().Waypoints[GetComponent<EnemyPatrols>().waypointNumber].transform.position, target.transform.position) > 20f)
                    {
                        currentState = State.Patrolling;

                    }

                    GetComponent<NavMeshAgent>().speed = 2.5f;
                    if (target.GetComponent<Health>().health <= 0)
                    {
                        helpMe = false;
                        currentState = State.Idle;
                        idleTimer = 3f;
                    }
                    if (GetComponent<EnemyClass>().currentClass == EnemyClass.Class.Melee)
                    {
                        MoveToAttackRange(target);
                    }
                    // Look at the enemy
                    transform.LookAt(target.transform);
                    //do the basic attack on the target
                    switch (GetComponent<EnemyClass>().currentClass)
                    {
                        case EnemyClass.Class.Ranged:
                            if (Vector3.Distance(transform.position, target.transform.position) <= rangedDistance + 1)
                            {
                                GetComponent<EnemyActions>().DoAction(EnemyActions.Action.Basic, target);
                            }

                            break;
                        case EnemyClass.Class.Melee:
                            if (Vector3.Distance(transform.position, target.transform.position) <= meleeDistance + 1)
                            {
                                GetComponent<EnemyActions>().DoAction(EnemyActions.Action.Basic, target);
                            }
                            break;
                        default:
                            break;
                    }
                }

                text.text = currentState.ToString();
                break;
        }
    }

    public void ImBeingAttacked(GameObject attacker)
    {
        if (helpMe == false)
        {
            target = attacker;
            currentState = State.Attacking;
            MoveToAttackRange(target);
        }
        helpMe = true;

    }

    public void checkForEnemies()
    {
        foreach (var i in companions)
        {
            if (i != null)
            {
                if (Vector3.Distance(gameObject.transform.position, i.transform.position) < 5f)
                {
                    target = i;
                    FMODUnity.RuntimeManager.PlayOneShot(alertSound, GetComponent<Transform>().position); // play test sound
                    currentState = State.Attacking;
                    MoveToAttackRange(target);
                }
            }
        }
        //if the player is within 5m of me, attack them
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 5f)
        {
            target = player;
            FMODUnity.RuntimeManager.PlayOneShot(alertSound, GetComponent<Transform>().position); // play test sound    
            currentState = State.Attacking;
            MoveToAttackRange(target);
        }
    }
    public void MoveToAttackRange(GameObject attackTarget)
    {
        float distance;

        if (GetComponent<EnemyClass>().currentClass == EnemyClass.Class.Melee)
        {
            distance = meleeDistance;
        }
        else
        {
            distance = rangedDistance;
        }
        Debug.Log("Moving to distance");

        Vector3 direction = transform.position - attackTarget.transform.position;
        direction.Normalize();
        Vector3 targetPos = attackTarget.transform.position + direction * distance;
        GetComponent<NavMeshMovement>().meshAgent.destination = targetPos;
    }

    #region Particles

    public void SpawnTargetCircle()
    {
        particle = Instantiate(enemyTargetParticleEffect, new Vector3(gameObject.transform.position.x, 0.05f, gameObject.transform.position.z), Quaternion.Euler(-90, 0, 0));
        particle.GetComponent<ParticleFollow>().target = gameObject.transform;
        imTargetted = true;
    }

    public void DestroyTargetCircle()
    {
        Destroy(particle, 0f);
        imTargetted = false;
    }

    #endregion


}
