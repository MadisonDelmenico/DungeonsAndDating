using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public PlayerInventory player;
    
    public int commonTreasureValue;
    public int uncommonTreasureValue;
    public int rareTreasureValue;

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

    public void SellItem(int rarity)
    {
        switch (rarity)
        {
            // Common Treasure
            case 0:
                if (player.commonTreasure > 0)
                {
                    player.commonTreasure--;
                    player.money += commonTreasureValue;
                    Debug.Log("Player sold an item for " + commonTreasureValue + "gp, and now has " + player.money + "gp.");
                }
                break;
            // Uncommon Treasure
            case 1:
                if (player.uncommonTreasure > 0)
                {
                    player.uncommonTreasure--;
                    player.money += uncommonTreasureValue;
                    Debug.Log("Player sold an item for " + uncommonTreasureValue + "gp, and now has " + player.money + "gp.");
                }
                break;
            // Rare Treasure
            case 2:
                if (player.rareTreasure > 0)
                {
                    player.rareTreasure--;
                    player.money += rareTreasureValue;
                    Debug.Log("Player sold an item for " + rareTreasureValue + "gp, and now has " + player.money + "gp.");
                }
                break;
            default:
                break;
        }
    }
}
