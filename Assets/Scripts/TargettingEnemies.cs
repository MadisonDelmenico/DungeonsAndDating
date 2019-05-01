using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingEnemies : MonoBehaviour
{
    [Header("Target")]
    public GameObject target;
    public GameObject friendlyTarget;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PlayerAI>())
        {
            target = gameObject;
            friendlyTarget = gameObject;
        }

        if (GetComponent<CompanionAIScript>())
        {
            target = gameObject;
            friendlyTarget = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
