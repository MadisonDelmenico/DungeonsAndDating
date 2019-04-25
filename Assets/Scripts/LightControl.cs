using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    private Light myLight;
    private Transform player;
    private float distanceToPlayer;
    private string lanternSoundLocation = "event:/Environment/Env_Torches_Burning_Loop";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        FMODUnity.RuntimeManager.PlayOneShot(lanternSoundLocation, this.transform.position); //Add sound to lantern

        if (gameObject.name != "Lantern")
        {
            myLight = gameObject.GetComponent<Light>();
            myLight.enabled = false;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", (Color.black));
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position, player.position);
        if (distanceToPlayer <= 4f)
        {
            if (gameObject.name != "Lantern")
            {
                myLight.enabled = true;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(191, 154, 0, 100));
            }
        }
    }
}
