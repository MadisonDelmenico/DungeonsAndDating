﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public bool fainted;
    public bool isPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        // Checks if the gameObject is the player
        isPlayer = (gameObject.tag == "Player") ? true : false;
    }

    // Update is called once per frame
    void Update()
    {


        if (health <= 0)
        {
            if (isPlayer == true)
            {
                fainted = true;
                SceneManager.LoadScene("Map");
            }
            else {
                Die();
            }
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

    }

    public void Die()
    {
        if (gameObject.tag != "Companion")
        {
            Destroy(this.gameObject);
        }
    }
}
