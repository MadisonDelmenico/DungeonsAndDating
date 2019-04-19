using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const string fileName = "PlayerData.json";
    private string filePath;

    public PlayerData localPlayerData = new PlayerData();

    private GameObject player;
    
    private GameObject[] companions;

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
        companions = GameObject.FindGameObjectsWithTag("Companion");

        filePath = Application.persistentDataPath + "/" + fileName;
        Debug.Log(filePath);
        ReadDataLocal();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ReadData();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData();
        }
    }


    private void SaveData()
    {
        if (player.GetComponent<PlayerInventory>())
        {
            localPlayerData.inventoryData = player.GetComponent<PlayerInventory>().SaveToPlayerData();
        }

        foreach (GameObject companion in companions)
        {
            if (companion.GetComponent<CompanionAIScript>())
            {
                localPlayerData.companionData = companion.GetComponent<CompanionAIScript>().SaveToPlayerData();
            }
        }

        PlayerWrapper wrapper = new PlayerWrapper
        {
            playerData = localPlayerData
        };

        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(filePath, contents);
    }

    private void ReadData()
    {
        ReadDataLocal();

        if (player.GetComponent<PlayerInventory>())
        {
            player.GetComponent<PlayerInventory>().ReadFromPlayerData(localPlayerData.inventoryData);
        }

        foreach (GameObject companion in companions)
        {
            if (companion.GetComponent<CompanionAIScript>())
            {
                companion.GetComponent<CompanionAIScript>().ReadFromPlayerData(localPlayerData.companionData);
            }
        }
    }

    private void ReadDataLocal()
    {
        string contents = System.IO.File.ReadAllText(filePath);
        PlayerWrapper wrapper = JsonUtility.FromJson<PlayerWrapper>(contents);

        localPlayerData = wrapper.playerData;
    }
}
