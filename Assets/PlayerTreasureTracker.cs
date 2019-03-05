using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTreasureTracker : MonoBehaviour
{
    public enum TreasureType { Common, Uncommon, Rare }
    public TreasureType type;

    public PlayerInventory player;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case TreasureType.Common:
                text.text = ("Common Treasure: " + player.commonTreasure);
                break;
            case TreasureType.Uncommon:
                text.text = ("Uncommon Treasure: " + player.uncommonTreasure);
                break;
            case TreasureType.Rare:
                text.text = ("Rare Treasure: " + player.rareTreasure);
                break;
            default:
                break;
        }
    }
}
