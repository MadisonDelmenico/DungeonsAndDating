using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float energy;
    public float maxEnergy;
    private float targetEnergy;

    public bool isPlayer;
    public bool isReducing = false;

    // Start is called before the first frame update
    void Start()
    {
        // Checks if the gameObject is the player
        isPlayer = (gameObject.tag == "Player") ? true : false;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReducing)
        {
            if (energy > targetEnergy)
            {
                energy -= 0.5f;
            }
            else
            {
                isReducing = false;
            }
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += Time.deltaTime;
            }
            else
            {
                energy = maxEnergy;
            }
        }
    }

    public void SetEnergy(float target)
    {
        targetEnergy = target;
        isReducing = true;
    }
}
