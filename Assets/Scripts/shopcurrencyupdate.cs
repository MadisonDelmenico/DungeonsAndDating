using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopcurrencyupdate : MonoBehaviour
{
    public Text swords;
    public Text gems;
    public Text artifacts;
    public Text money;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        swords.text = "x" + player.GetComponent<PlayerInventory>().commonTreasure;
        gems.text = "x" + player.GetComponent<PlayerInventory>().uncommonTreasure;
        artifacts.text = "x" + player.GetComponent<PlayerInventory>().rareTreasure;
        money.text = "x" + player.GetComponent<PlayerInventory>().money;

    }
}
