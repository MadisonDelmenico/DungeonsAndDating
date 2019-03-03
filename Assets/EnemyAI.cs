using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public enum State { Patrolling, Attacking, Idle }
    public State currentState;
    public GameObject target;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<EnemyPatrols>().Waypoints.Length > 0)
        {
            currentState = State.Patrolling;
        }

        text.text = currentState.ToString();
    }

    // Update is called once per frame
    void Update()
    {
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
            else
            {
                if (currentState == State.Patrolling)
                {
                    target = gameObject;
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.green);
            target = gameObject;
        }
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Patrolling:
                GetComponent<EnemyPatrols>().Patrol();
                target = gameObject;
                break;
            case State.Attacking:
                if (target != gameObject)
                {
                    GetComponent<NavMeshMovement>().meshAgent.destination = target.transform.position;
                    MoveTo(target.transform);
                    Attack(target);
                }


                break;
        }
    }

    public void Attack(GameObject targetGameObject)
    {
        //is the enemy is greater than 10m from the area im supposed to patrol
        if (Vector3.Distance(GetComponent<EnemyPatrols>().Waypoints[GetComponent<EnemyPatrols>().waypointNumber].transform.position, target.transform.position) > 10f)
        {
            currentState = State.Patrolling;
        }

        //if the enemy is over 1m but less than 10m away from me
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) > 1f && Vector3.Distance(gameObject.transform.position, target.transform.position) <= 10f)
        {
            //move towards the target
            MoveTo(target.transform);
        }


    }

    public void MoveTo(Transform targetTransform)
    {
        GetComponent<NavMeshMovement>().meshAgent.destination = targetTransform.position;
    }
}
