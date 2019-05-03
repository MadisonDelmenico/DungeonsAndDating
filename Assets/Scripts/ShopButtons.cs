using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.Experimental.UIElements.Image;

public class ShopButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text name;
    public Text description;
    public Text price;
    public string objectDescription;
    public int objectPrice;
    public string objectName;
    public GameObject container;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if this button isn't an object that can be sold, don't worry about the next part
        if (gameObject.name != "sell")
        {
            //if the player doesn't have enough money, they shouldn't be able to click the button
            if (player.GetComponent<PlayerInventory>().money < gameObject.GetComponent<ShopButtons>().objectPrice)
            {
                gameObject.GetComponent<Button>().interactable = false;
            }

            if (player.GetComponent<PlayerInventory>().money >= gameObject.GetComponent<ShopButtons>().objectPrice)
            {
                gameObject.GetComponent<Button>().interactable = true;
            }
        }

        //if the button is an object that can be sold
        if (gameObject.name == "sell")
        {
            //check to see if you have any of that object. If you don't, disable the button.
            switch (gameObject.GetComponent<ShopButtons>().objectName)
            {
                case "Rusted sword":
                    if (player.GetComponent<PlayerInventory>().commonTreasure < 1)
                    {
                        gameObject.GetComponent<Button>().interactable = false;
                        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                       GameObject.Find("swordimage").GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                    }

                    if (player.GetComponent<PlayerInventory>().commonTreasure >= 1)
                    {
                        gameObject.GetComponent<Button>().interactable = true;
                        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                        GameObject.Find("swordimage").GetComponent<UnityEngine.UI.Image>().color = Color.white;
                    }
                    break;
                             
                case "Average jewell":
                    if (player.GetComponent<PlayerInventory>().uncommonTreasure < 1)
                    {
                        gameObject.GetComponent<Button>().interactable = false;
                        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                        GameObject.Find("gemimage").GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                    }

                    if (player.GetComponent<PlayerInventory>().uncommonTreasure >= 1)
                    {
                        gameObject.GetComponent<Button>().interactable = true;
                        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                        GameObject.Find("gemimage").GetComponent<UnityEngine.UI.Image>().color = Color.white;
                    }
                    break;
                case "Magical Artifact":
                    if (player.GetComponent<PlayerInventory>().rareTreasure < 1)
                    {
                        gameObject.GetComponent<Button>().interactable = false;
                        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                        GameObject.Find("ringimage").GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                    }

                    if (player.GetComponent<PlayerInventory>().rareTreasure >= 1)
                    {
                        gameObject.GetComponent<Button>().interactable = true;
                        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                        GameObject.Find("ringimage").GetComponent<UnityEngine.UI.Image>().color = Color.white;
                    }
                    break;
            }
        }

    }

    //when you mouse over the object
    public void OnPointerEnter(PointerEventData eventData)
    {
        container.SetActive(true);
        name.text = objectName;
        price.text = objectPrice + "GP";
        description.text = objectDescription;
    }

    //when your mouse leaves the object
    public void OnPointerExit(PointerEventData eventData)
    {

        name.text = "";
        price.text = "";
        description.text = "";
    }

    public void Sell()
    {


        switch (gameObject.GetComponent<ShopButtons>().objectName)
        {
            case "Rusted sword":
                if (player.GetComponent<PlayerInventory>().commonTreasure >= 1)
                {
                    player.GetComponent<PlayerInventory>().money += objectPrice;
                    player.GetComponent<PlayerInventory>().commonTreasure--;
                }

                break;
            case "Average jewell":
                if (player.GetComponent<PlayerInventory>().uncommonTreasure >= 1)
                {
                    player.GetComponent<PlayerInventory>().money += objectPrice;
                    player.GetComponent<PlayerInventory>().uncommonTreasure--;
                }

                break;
            case "Magical Artifact":
                if (player.GetComponent<PlayerInventory>().rareTreasure >= 1)
                {
                    player.GetComponent<PlayerInventory>().money += objectPrice;
                    player.GetComponent<PlayerInventory>().rareTreasure--;
                }

                break;
        }

    }

    public void Buy()
    {

        switch (gameObject.GetComponent<ShopButtons>().objectName)
        {
            case "Steak of Strength":
                player.GetComponent<PlayerInventory>().AddItem(1);
                break;
            case "Snake Shaped Wand":
                player.GetComponent<PlayerInventory>().AddItem(2);
                break;
            case "Holy Order Enlistment":
                player.GetComponent<PlayerInventory>().AddItem(3);
                break;
            case "Halberd Whetstone":
                player.GetComponent<PlayerInventory>().AddItem(4);
                break;
            case "Spiked Boots":
                player.GetComponent<PlayerInventory>().AddItem(5);
                break;
            case "Ranger Scout cookies":
                player.GetComponent<PlayerInventory>().AddItem(6);
                break;
            case "Scale Repair Kit":
                player.GetComponent<PlayerInventory>().AddItem(7);
                break;
            case "Haunted Dagger":
                player.GetComponent<PlayerInventory>().AddItem(8);
                break;
            case "Local Charity Receipt":
                player.GetComponent<PlayerInventory>().AddItem(9);
                break;
        }
        player.GetComponent<PlayerInventory>().money -= gameObject.GetComponent<ShopButtons>().objectPrice;
        print("I just bought a " + gameObject.GetComponent<ShopButtons>().objectName);

    }


}
