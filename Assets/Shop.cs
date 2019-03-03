using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public PlayerInventory player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem(int value)
    {
        if (player.money >= value)
        {
            player.money -= value;
            Debug.Log("Player bought an item for " + value + "gp");
        }
        else
        {
            Debug.Log("Player does not have enough money for the item, it costs " + value + "gp, and the player has " + player.money + "gp");
        }
    }

    public void SellItem(int value)
    {
        player.money += value;
        Debug.Log("Player sold an item for " + value + "gp, and now has " + player.money + "gp.");
    }
}
