using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGoldTracker : MonoBehaviour
{
    private Text displayText;
    private PlayerInventory player;

    // Start is called before the first frame update
    void Start()
    {
        displayText = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = (player.money + "gp");
    }
}
