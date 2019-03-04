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
    public AudioSource alert;
    public GameObject player;
    public GameObject[] companions;
    public float[] companionDistances;
    public int companionArrayPosition;
    public float rangedDistance;
    public float meleeDistance;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        idleTimer -= Time.deltaTime;

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
                            Attack(i.gameObject.GetComponent<EnemyAI>().target);
                        }
                    }
                }
            }
        }
        switch (currentState)
        {
            case State.Idle:
                text.text = currentState.ToString();
                if (idleTimer <= 0)
                {
                    currentState = State.Patrolling;
                }
                break;
            case State.Patrolling:
                text.text = currentState.ToString();
                GetComponent<EnemyPatrols>().Patrol();
                target = gameObject;
                GetComponent<NavMeshAgent>().speed = 1.5f;

                checkForEnemies();


                break;
            case State.Attacking:
                text.text = currentState.ToString();
                if (target != gameObject)
                {
                    GetComponent<NavMeshMovement>().meshAgent.destination = target.transform.position;
                    MoveTo(target.transform);
                    Attack(target);
                    GetComponent<NavMeshAgent>().speed = 3f;
                    if (target.GetComponent<Health>().health <= 0)
                    {
                        helpMe = false;
                        currentState = State.Idle;
                        idleTimer = 3f;
                    }

                    if (GetComponent<EnemyClass>().currentClass == EnemyClass.Class.Melee )
                    {
                        MoveToDistance(meleeDistance);
                    }
                    else
                    {
                        MoveToDistance(rangedDistance);
                    }

                    // Look at the enemy
                    transform.LookAt(target.transform);
                    //do the basic attack on the target
                    GetComponent<EnemyActions>().DoAction(EnemyActions.Action.Basic, target);
                }


                break;
        }
    }

    public void Attack(GameObject targetGameObject)
    {
        target.gameObject.transform.LookAt(target.transform);

        //is the enemy is greater than 10m from the area im supposed to patrol
        if (Vector3.Distance(
                GetComponent<EnemyPatrols>().Waypoints[GetComponent<EnemyPatrols>().waypointNumber].transform.position,
                target.transform.position) > 5f)
        {
            currentState = State.Patrolling;
        }

        //if the enemy is over 1m but less than 10m away from me
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) > 1f &&
            Vector3.Distance(gameObject.transform.position, target.transform.position) <= 10f)
        {
            //move towards the target
            MoveTo(target.transform);
        }


    }

    public void MoveTo(Transform targetTransform)
    {
        GetComponent<NavMeshMovement>().meshAgent.destination = targetTransform.position;
    }

    public void ImBeingAttacked(GameObject attacker)
    {
        if (helpMe == false)
        {
            target = attacker;
            currentState = State.Attacking;

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
                    alert.Play();
                    currentState = State.Attacking;
                }
            }
        }
       //if the player is within 5m of me, attack them
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 5f)
        {
            target = player;
            alert.Play();
            currentState = State.Attacking;
        }
    }
    public void MoveToDistance(float distance)
    {
        text.text = "Moving for Attack";
        Vector3 direction = transform.position - target.transform.position;
        direction.Normalize();
        Vector3 targetPos = target.transform.position + direction * distance;
        GetComponent<NavMeshMovement>().meshAgent.destination = targetPos;
    }

}
