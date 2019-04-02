using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventoryData
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
}
