using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int money;

    [Header("Treasure")]
    public int commonTreasure;
    public int uncommonTreasure;
    public int rareTreasure;

    [Header("Tier 1 Gifts")]
    public int steakOfStrength;
    public int snakeShapedWand;
    public int holyOrderEnlistment;

    [Header("Tier 2 Gifts")]
    public int halberdWhetstone;
    public int spikedBoots;
    public int rangerScoutCookies;

    [Header("Tier 3 Gifts")]
    public int scaleRepairKit;
    public int hauntedDagger;
    public int localCharityReciept;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickupCoin>())
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interactables/Coin_Pickup_Test", GetComponent<Transform>().position); // play test sound
            money += other.GetComponent<PickupCoin>().value;
            Destroy(other.gameObject);

        }
        if (other.GetComponent<PickupTreasure>())
        {
            switch (other.GetComponent<PickupTreasure>().rarity)
            {
                case PickupTreasure.TreasureRarity.Common:
                    commonTreasure++;
                    break;
                case PickupTreasure.TreasureRarity.Uncommon:
                    uncommonTreasure++;
                    break;
                case PickupTreasure.TreasureRarity.Rare:
                    rareTreasure++;
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Add an item to the players inventory
    /// </summary>
    /// <param name="item"> Item code of the item to add: 
    ///                     1 = Steak of Strength, 2 = Snake Shaped Wand, 3 = Holy Order Enlistment, 
    ///                     4 = Halberd Whetstone, 5 = Spiked Boots, 6 = Ranger Scout Cookies, 
    ///                     7 = Scale Repair Kit, 8 = Haunted Dagger, 9 = Local Charity Reciept</param>
    public void AddItem(int item)
    {
        switch (item)
        {
            case 1:
                steakOfStrength++;
                break;
            case 2:
                snakeShapedWand++;
                break;
            case 3:
                holyOrderEnlistment++;
                break;
            case 4:
                halberdWhetstone++;
                break;
            case 5:
                spikedBoots++;
                break;
            case 6:
                rangerScoutCookies++;
                break;
            case 7:
                scaleRepairKit++;
                break;
            case 8:
                hauntedDagger++;
                break;
            case 9:
                localCharityReciept++;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Removes an item from the players inventory
    /// </summary>
    /// <param name="item"> Item code of the item to add: 
    ///                     1 = Steak of Strength, 2 = Snake Shaped Wand, 3 = Holy Order Enlistment, 
    ///                     4 = Halberd Whetstone, 5 = Spiked Boots, 6 = Ranger Scout Cookies, 
    ///                     7 = Scale Repair Kit, 8 = Haunted Dagger, 9 = Local Charity Reciept</param>
    public void RemoveItem(int item)
    {
        switch (item)
        {
            case 1:
                if (steakOfStrength > 0)
                {
                    steakOfStrength--;
                }
                break;
            case 2:
                if (snakeShapedWand > 0)
                {
                    snakeShapedWand--;
                }
                break;
            case 3:
                if (holyOrderEnlistment > 0)
                {
                    holyOrderEnlistment--;
                }
                break;
            case 4:
                if (halberdWhetstone > 0)
                {
                    halberdWhetstone--;
                }
                break;
            case 5:
                if (spikedBoots > 0)
                {
                    spikedBoots--;
                }
                break;
            case 6:
                if (rangerScoutCookies > 0)
                {
                    rangerScoutCookies--;
                }
                break;
            case 7:
                if (scaleRepairKit > 0)
                {
                    scaleRepairKit--;
                }
                break;
            case 8:
                if (hauntedDagger > 0)
                {
                    hauntedDagger--;
                }
                break;
            case 9:
                if (localCharityReciept > 0)
                {
                    localCharityReciept--;
                }
                break;
            default:
                break;
        }
    }

    public void LoadFromPlayerPrefs()
    {
        money = PlayerPrefs.GetInt("Money");

        string giftItems = PlayerPrefs.GetString("Gifts");
        string[] items = giftItems.Split(',');

        steakOfStrength = int.Parse(items[0]);
        snakeShapedWand = int.Parse(items[1]);
        holyOrderEnlistment = int.Parse(items[2]);
        halberdWhetstone = int.Parse(items[3]);
        spikedBoots = int.Parse(items[4]);
        rangerScoutCookies = int.Parse(items[5]);
        scaleRepairKit = int.Parse(items[6]);
        hauntedDagger = int.Parse(items[7]);
        localCharityReciept = int.Parse(items[8]);
    }

    public void SaveToPlayerPrefs()
    {
        string giftItems = "";
        giftItems += steakOfStrength.ToString() + ",";
        giftItems += snakeShapedWand.ToString() + ",";
        giftItems += holyOrderEnlistment.ToString() + ",";
        giftItems += halberdWhetstone.ToString() + ",";
        giftItems += spikedBoots.ToString() + ",";
        giftItems += rangerScoutCookies.ToString() + ",";
        giftItems += scaleRepairKit.ToString() + ",";
        giftItems += hauntedDagger.ToString() + ",";
        giftItems += localCharityReciept.ToString();
        Debug.Log(giftItems);

        string treasure = "";
        treasure += commonTreasure + ",";
        treasure += uncommonTreasure + ",";
        treasure += rareTreasure;
        Debug.Log(treasure);

        // PlayerPrefs.SetString("Gifts", giftItems);
        // PlayerPrefs.SetString("Treasure", treasure);
        // PlayerPrefs.SetInt("Money", money);
    }
}
