using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraMovement : MonoBehaviour
{
    Transform player;
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, player.position, Time.deltaTime * speed);
    }
}
