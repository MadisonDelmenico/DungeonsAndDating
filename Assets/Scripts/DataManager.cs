using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    string fileName = "player.json";
    string filePath;

    public PlayerData playerData = new PlayerData();

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        filePath = Application.persistentDataPath + "/" + fileName;
        Debug.Log(filePath);
    }

    // Update is called once per frame
    void Update()
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


    void SaveData()
    {
        PlayerWrapper wrapper = new PlayerWrapper();
        wrapper.playerData = playerData;

        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(filePath, contents);
    }

    void ReadData()
    {
        string contents = System.IO.File.ReadAllText(filePath);
        PlayerWrapper wrapper = JsonUtility.FromJson<PlayerWrapper>(contents);

        playerData = wrapper.playerData;

        if (player.GetComponent<PlayerInventory>())
        {
            player.GetComponent<PlayerInventory>().ReadFromPlayerData(playerData.inventoryData);
        }
    }
}
