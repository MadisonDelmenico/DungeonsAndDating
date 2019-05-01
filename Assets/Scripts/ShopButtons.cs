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
    public GameObject sceneManager;


    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager");
    }

    // Update is called once per frame
    void Update()
    {
        //if this button isn't an object that can be sold, don't worry about the next part
        if (gameObject.name != "sell")
        {
            //if the player doesn't have enough money, they shouldn't be able to click the button
            if (sceneManager.GetComponent<DataManager>().playerData.inventoryData.money < gameObject.GetComponent<ShopButtons>().objectPrice)
            {
                gameObject.GetComponent<Button>().interactable = false;
            }

            if (sceneManager.GetComponent<DataManager>().playerData.inventoryData.money >= gameObject.GetComponent<ShopButtons>().objectPrice)
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
                    if (sceneManager.GetComponent<DataManager>().playerData.inventoryData.commonTreasure < 1)
                    {
                        gameObject.GetComponent<Button>().interactable = false;
                        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                       GameObject.Find("swordimage").GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                    }

                    if (sceneManager.GetComponent<DataManager>().playerData.inventoryData.commonTreasure >= 1)
                    {
                        gameObject.GetComponent<Button>().interactable = true;
                        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                        GameObject.Find("swordimage").GetComponent<UnityEngine.UI.Image>().color = Color.white;
                    }
                    break;
                             
                case "Average jewell":
                    if (sceneManager.GetComponent<DataManager>().playerData.inventoryData.uncommonTreasure < 1)
                    {
                        gameObject.GetComponent<Button>().interactable = false;
                        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                        GameObject.Find("gemimage").GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                    }

                    if (sceneManager.GetComponent<DataManager>().playerData.inventoryData.uncommonTreasure >= 1)
                    {
                        gameObject.GetComponent<Button>().interactable = true;
                        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                        GameObject.Find("gemimage").GetComponent<UnityEngine.UI.Image>().color = Color.white;
                    }
                    break;
                case "Magical Artifact":
                    if (sceneManager.GetComponent<DataManager>().playerData.inventoryData.rareTreasure < 1)
                    {
                        gameObject.GetComponent<Button>().interactable = false;
                        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                        GameObject.Find("ringimage").GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                    }

                    if (sceneManager.GetComponent<DataManager>().playerData.inventoryData.rareTreasure >= 1)
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
                if (sceneManager.GetComponent<DataManager>().playerData.inventoryData.commonTreasure >= 1)
                {
                    sceneManager.GetComponent<DataManager>().playerData.inventoryData.money +=
                        objectPrice;
                    sceneManager.GetComponent<DataManager>().playerData.inventoryData.commonTreasure--;
                }

                break;
            case "Average jewell":
                if (sceneManager.GetComponent<DataManager>().playerData.inventoryData.uncommonTreasure >= 1)
                {
                    sceneManager.GetComponent<DataManager>().playerData.inventoryData.money +=
                        objectPrice;
                    sceneManager.GetComponent<DataManager>().playerData.inventoryData.uncommonTreasure--;
                }

                break;
            case "Magical Artifact":
                if (sceneManager.GetComponent<DataManager>().playerData.inventoryData.rareTreasure >= 1)
                {
                    sceneManager.GetComponent<DataManager>().playerData.inventoryData.money +=
                        objectPrice;
                    sceneManager.GetComponent<DataManager>().playerData.inventoryData.rareTreasure--;
                }

                break;
        }

    }

    public void Buy()
    {

        switch (gameObject.GetComponent<ShopButtons>().objectName)
        {
            case "Steak of Strength":
                sceneManager.GetComponent<DataManager>().playerData.inventoryData.steakOfStrength++;
                break;
            case "Snake Shaped Wand":
                sceneManager.GetComponent<DataManager>().playerData.inventoryData.snakeShapedWand++;
                break;
            case "Holy Order Enlistment":
                sceneManager.GetComponent<DataManager>().playerData.inventoryData.holyOrderEnlistment++;
                break;
            case "Halberd Whetstone":
                sceneManager.GetComponent<DataManager>().playerData.inventoryData.halberdWhetstone++;
                break;
            case "Spiked Boots":
                sceneManager.GetComponent<DataManager>().playerData.inventoryData.spikedBoots++;
                break;
            case "Ranger Scout cookies":
                sceneManager.GetComponent<DataManager>().playerData.inventoryData.rangerScoutCookies++;
                break;
            case "Scale Repair Kit":
                sceneManager.GetComponent<DataManager>().playerData.inventoryData.scaleRepairKit++;
                break;
            case "Haunted Dagger":
                sceneManager.GetComponent<DataManager>().playerData.inventoryData.hauntedDagger++;
                break;
            case "Local Charity Receipt":
                sceneManager.GetComponent<DataManager>().playerData.inventoryData.localCharityReciept++;
                break;
        }
        sceneManager.GetComponent<DataManager>().playerData.inventoryData.money -=
            gameObject.GetComponent<ShopButtons>().objectPrice;
        print("I just bought a " + gameObject.GetComponent<ShopButtons>().objectName);

    }


}
