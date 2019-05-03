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
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interactables/Interact_Game_Gold_Collect", GetComponent<Transform>().position);
            money += other.GetComponent<PickupCoin>().value;
            other.GetComponent<PickupCoin>().PickUp(this.gameObject.transform);

            int randomTreasure = Random.Range(0, 20);

            if (randomTreasure < 5)
            {
                Debug.Log("Treasure Obtained!");
                commonTreasure += 3;
                uncommonTreasure += 2;
                rareTreasure += 1;
            }
        }
        if (other.GetComponent<PickupTreasure>())
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interactables/Interact_Game_ItemPickup", GetComponent<Transform>().position);
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

    public void ReadFromPlayerData(PlayerInventoryData data)
    {
        // Read money value
        money = data.money;

        // Read treasure item values
        commonTreasure = data.commonTreasure;
        uncommonTreasure = data.uncommonTreasure;
        rareTreasure = data.rareTreasure;

        // Read tier 1 item values
        steakOfStrength = data.steakOfStrength;
        snakeShapedWand = data.steakOfStrength;
        holyOrderEnlistment = data.holyOrderEnlistment;

        // Read tier 2 item values
        halberdWhetstone = data.halberdWhetstone;
        spikedBoots = data.spikedBoots;
        rangerScoutCookies = data.rangerScoutCookies;

        // Read tier 3 item values
        scaleRepairKit = data.scaleRepairKit;
        hauntedDagger = data.hauntedDagger;
        localCharityReciept = data.localCharityReciept;
    }

    public PlayerInventoryData SaveToPlayerData()
    {
        PlayerInventoryData data = new PlayerInventoryData();

        data.money = money;

        data.commonTreasure = commonTreasure;
        data.uncommonTreasure = uncommonTreasure;
        data.rareTreasure = rareTreasure;

        data.steakOfStrength = steakOfStrength;
        data.snakeShapedWand = snakeShapedWand;
        data.holyOrderEnlistment = holyOrderEnlistment;

        data.halberdWhetstone = halberdWhetstone;
        data.spikedBoots = spikedBoots;
        data.rangerScoutCookies = rangerScoutCookies;

        data.scaleRepairKit = scaleRepairKit;
        data.hauntedDagger = hauntedDagger;
        data.localCharityReciept = localCharityReciept;

        return data;
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
}
