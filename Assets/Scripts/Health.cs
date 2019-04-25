using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [HideInInspector]
    public bool isPlayer;
    [Header("My Health")]
    public bool canUseHealthPotions;
    public float health;
    public float maxHealth;

    [Header("Status")]
    public bool fainted;

    // Start is called before the first frame update
    void Start()
    {
        // Checks if the gameObject is the player
        if (gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (isPlayer == true)
            {
                GetComponent<animationscript>().Die();
                fainted = true;
                SceneManager.LoadScene("Map");
            }
            else
            {
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
        if (gameObject.CompareTag("Companion") == false)
        {
            GameObject[] companions = GameObject.FindGameObjectsWithTag("Companion");
            foreach (var i in companions)
            {
                i.GetComponent<NavMeshMovement>().Disengage();
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshMovement>().Disengage();

            //if the enemy dying is currently targeted by the player
            if (gameObject.GetComponent<EnemyAI>().imTargetted == true)
            {
                gameObject.GetComponent<EnemyAI>().DestroyTargetCircle();

                //the enemy is dying, set the target back to the player to avoid null reference
                player.GetComponent<TargettingEnemies>().target = player;
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickupHealth>())
        {
            if (canUseHealthPotions == true)
            {
                if (health < maxHealth)
                {
                    if (other.GetComponent<PickupHealth>().isSpawned)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Interactables/Interact_Game_Healing", GetComponent<Transform>().position);
                        health += other.GetComponent<PickupHealth>().healAmount;
                        other.GetComponent<PickupHealth>().PickUp();
                    }
                }
            }
        }
    }
}
