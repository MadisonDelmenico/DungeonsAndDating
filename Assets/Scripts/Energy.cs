using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float energy;
    public float maxEnergy;

    public bool isPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // Checks if the gameObject is the player
        isPlayer = (gameObject.tag == "Player") ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
