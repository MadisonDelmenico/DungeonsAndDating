using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingEnemies : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    public float Distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(target.transform.position, player.transform.position);
    }
}
