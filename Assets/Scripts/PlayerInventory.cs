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

    public void AddItem(int item)    
    {

    }
}
