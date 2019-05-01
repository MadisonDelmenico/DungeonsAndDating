using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class ShopMenuButtons : MonoBehaviour
{
    public GameObject shopUIMenu;
    public GameObject shopUIBuy;
    public GameObject shopUISell;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Buy()
    {
        shopUIMenu.SetActive(false);
        shopUIBuy.SetActive(true);
        shopUISell.SetActive(false);
    }

    public void Sell()
    {
        shopUIMenu.SetActive(false);
        shopUIBuy.SetActive(false);
        shopUISell.SetActive(true);
    }

    public void Menu()
    {
        shopUIMenu.SetActive(true);
        shopUIBuy.SetActive(false);
        shopUISell.SetActive(false);
    }

    public void Leave()
    {
        shopUIMenu.SetActive(true);
        shopUIBuy.SetActive(false);
        shopUISell.SetActive(false);

        Camera.main.GetComponent<UIBlurToggle>().ToggleBlur();

        GameObject.Find("ShopUI").SetActive(false);
        GameObject.Find("Player").GetComponent<NavMeshMovement>().enabled = true;

    }
}
