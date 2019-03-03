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
    public int arrayPosition;
    public bool helpMe;
    private float idleTimer;

    // Start is called before the first frame update
    void Start()
    {
        //Enemies are aware of all other enemies in the scene
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Distances = new float[Enemies.Length];
        if (GetComponent<EnemyPatrols>().Waypoints.Length > 0)
        {
            currentState = State.Patrolling;
        }

        arrayPosition = 0;
        idleTimer = 0;
        text.text = currentState.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        idleTimer -= Time.deltaTime;
        //set all the distances from this enemy and the other enemies in the scene
        foreach (var i in Enemies)
        {
            for (arrayPosition = 0; arrayPosition < Enemies.Length; arrayPosition++)
            {
                if (Enemies[arrayPosition] != null)
                {
                    Distances[arrayPosition] = Vector3.Distance(transform.position, Enemies[arrayPosition].transform.position);
                    if (i != null)
                    {
                        if (i.gameObject.GetComponent<EnemyAI>().helpMe == true && Distances[arrayPosition] < 5f)
                        {
                            Attack(i.gameObject.GetComponent<EnemyAI>().target);
                        }
                    }

                }


            }


        }

        //make a raycast, see if there are any players/companions around.
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("Companion"))
            {
                Debug.Log(hit.transform.gameObject.name + " hit");
                target = hit.transform.gameObject;
                currentState = State.Attacking;
            }
            //  else
            //  {
            //    if (currentState == State.Patrolling)
            //   {
            //       target = gameObject;
            //   }
            //  }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.green);
            target = gameObject;
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
                break;
            case State.Attacking:
                text.text = currentState.ToString();
                if (target != gameObject)
                {
                    GetComponent<NavMeshMovement>().meshAgent.destination = target.transform.position;
                    MoveTo(target.transform);
                    Attack(target);
                    GetComponent<NavMeshAgent>().speed = 2f;
                    if (target.GetComponent<Health>().health <= 0)
                    {
                        helpMe = false;
                        currentState = State.Idle;
                        idleTimer = 3f;
                    }
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

}
