using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationScript : MonoBehaviour

{

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        //if the player is within my collider
        if (gameObject.GetComponent<Collider>().bounds
            .Contains(GameObject.FindGameObjectWithTag("Player").transform.position))
        {

            if (gameObject.name == "Crossroads")
            {
                if (mainCamera.GetComponent<Camera>().orthographicSize < 7.5)
                {
                    mainCamera.GetComponent<Camera>().orthographicSize += 0.05f;
                }

                if (mainCamera.GetComponent<Camera>().orthographicSize > 7.5)
                {
                    mainCamera.GetComponent<Camera>().orthographicSize = 7.5f;
                }
            }
            else
            {
                if (mainCamera.GetComponent<Camera>().orthographicSize > 5)
                {
                    mainCamera.GetComponent<Camera>().orthographicSize -= 0.05f;
                }

                if (mainCamera.GetComponent<Camera>().orthographicSize < 5)
                {
                    mainCamera.GetComponent<Camera>().orthographicSize = 5;
                }
            }
        }

    }
}

