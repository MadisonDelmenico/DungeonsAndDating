using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class EnemyPatrols : MonoBehaviour
{
    [Header("My patrol Route")]
    public GameObject[] Waypoints;

    private NavMeshMovement navMesh;

    [Header("The waypoint I'm moving to")]
    public int waypointNumber;

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshMovement>();
        waypointNumber = 0;
        navMesh.meshAgent.destination = Waypoints[waypointNumber].transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var waypoint in Waypoints)
        {

            if (Vector3.Distance(gameObject.transform.position, Waypoints[waypointNumber].transform.position) <= 0.5f)
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
        navMesh.meshAgent.destination = Waypoints[waypointNumber].transform.position;
    }
}
