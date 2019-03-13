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
}
