using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    public float healAmount;

    float respawnTimer = 0.0f;
    public float respawnTime = 0.0f;

    MeshRenderer meshRenderer;

    public bool isSpawned = true;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (respawnTimer <= 0.0f)
        {
            meshRenderer.enabled = true;
            isSpawned = true;
        }
        else
        {
            respawnTimer -= Time.deltaTime;
        }
    }

    public void PickUp()
    {
        respawnTimer = respawnTime;
        meshRenderer.enabled = false;
        isSpawned = false;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Interactables/Interact_Game_ItemPickup", GetComponent<Transform>().position);
    }
}
