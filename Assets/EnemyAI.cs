﻿using System.Collections;
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

        //make a raycast, see if there are any players/companions around.
        /*RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("Companion"))
            {

                Debug.Log(gameObject.name + ": " + " hit " + transform.gameObject.name);
                target = hit.transform.gameObject;
                alert.Play();
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
        }*/

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

}
