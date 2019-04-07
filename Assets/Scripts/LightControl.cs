using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    private Light myLight;
    private Transform player;
    private float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        myLight = gameObject.GetComponent<Light>();
        myLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position, player.position);

        if (distanceToPlayer <= 5.0f)
        {
            myLight.enabled = true;
        }
    }
}
