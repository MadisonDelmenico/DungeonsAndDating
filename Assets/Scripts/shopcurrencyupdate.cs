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
    public GameObject sceneManager;


    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager");
    }

    // Update is called once per frame
    void Update()
    {
        swords.text = "x" + sceneManager.GetComponent<DataManager>().playerData.inventoryData.commonTreasure;
        gems.text = "x" + sceneManager.GetComponent<DataManager>().playerData.inventoryData.uncommonTreasure;
        artifacts.text = "x" + sceneManager.GetComponent<DataManager>().playerData.inventoryData.rareTreasure;
        money.text = "x" + sceneManager.GetComponent<DataManager>().playerData.inventoryData.money;

    }
}
