using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public PlayerInventory player;

    public int tier1ItemPrice;
    public int tier2ItemPrice;
    public int tier3ItemPrice;

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

    /// <summary>
    /// Buying an item from the shop
    /// </summary>
    /// <param name="item">Item tier (1, 2, 3) and item class (B, P, S) as a string. (eg: "3B" or "1P")</param>
    public void BuyItem(string item)
    {
        int value = 0;
        int itemCode = 0;

        foreach (char i in item)
        {
            switch (i)
            {
                case '1':
                    value = tier1ItemPrice;
                    break;
                case '2':
                    value = tier2ItemPrice;
                    itemCode += 3;
                    break;
                case '3':
                    value = tier3ItemPrice;
                    itemCode += 6;
                    break;
                case 'B':
                    itemCode += 1;
                    break;
                case 'S':
                    itemCode += 2;
                    break;
                case 'P':
                    itemCode += 3;
                    break;
                default:
                    break;
            }
        }

        Debug.Log("ItemCode = " + itemCode);

        if (player.money >= value)
        {
            player.money -= value;
            player.AddItem(itemCode);
            Debug.Log("Player bought item " + item + " for " + value + "gp");
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
