using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public GameObject player;
    public bool isAttacking;
    public float Distance;
    public GameObject target;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        player = gameObject;
        target = gameObject;
        Distance = 0;
        timer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(target.transform.position, player.transform.position);

    }

    public void UpdateTarget(GameObject newTarget)
    {
        target = newTarget;
    }


}
