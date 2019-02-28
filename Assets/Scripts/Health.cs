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
            Destroy(this.gameObject);
        }
    }

}
