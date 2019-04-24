using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrols : MonoBehaviour
{
    [Header("My patrol Route")]
    public GameObject[] Waypoints;

    private NavMeshAgent meshAgent;

    [Header("The waypoint I'm moving to")]
    public int waypointNumber;

    // Start is called before the first frame update
    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        waypointNumber = 0;
        if (gameObject.CompareTag("Villager"))
        {
            Patrol();
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Villager"))
        {
            Patrol();
        }

    }

    public void Patrol()
    {
        meshAgent.destination = Waypoints[waypointNumber].transform.position;
        //foreach (var waypoint in Waypoints)
        {

            if (Vector3.Distance(gameObject.transform.position, Waypoints[waypointNumber].transform.position) <= 1.5f)
            {
                NextWaypoint();
            }


        }
    }

    public void NextWaypoint()
    {

        waypointNumber++;
        if (waypointNumber >= Waypoints.Length)
        {
            waypointNumber = 0;
        }
       
        meshAgent.destination = Waypoints[waypointNumber].transform.position;

       
    }

}
