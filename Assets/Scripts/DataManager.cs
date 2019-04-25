using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const string fileName = "PlayerData.json";
    private string filePath;

    public enum CurrentScene
    {
        Title,
        Creation,
        Market,
        Dungeon
    };
    public CurrentScene scene;

    public PlayerData playerData = new PlayerData();

    public bool manualReset;
    public bool manualSave;
    public bool manualLoad;

    private GameObject player;
    private GameObject[] companions;

    private bool started;

    // Start is called before the first frame update
    private void Start()
    {
        // DontDestroyOnLoad(this.gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
        companions = GameObject.FindGameObjectsWithTag("Companion");

        filePath = Application.persistentDataPath + "/" + fileName;
        Debug.Log(filePath);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!started)
        {
            started = true;

            if (System.IO.File.Exists(filePath))
            {
                ReadData();
            }
            else
            {
                ResetSaveData();
            }
        }

        if (manualLoad)
        {
            manualLoad = false;
            ReadData();
        }

        if (manualSave)
        {
            manualSave = false;
            SaveData();
        }

        if (manualReset)
        {
            manualReset = false;
            ResetSaveData();
        }
    }


    public void SaveData()
    {
        Debug.Log("Saving Data...");
        if (player.GetComponent<PlayerInventory>())
        {
            playerData.inventoryData = player.GetComponent<PlayerInventory>().SaveToPlayerData();
        }

        foreach (GameObject companion in companions)
        {
            if (companion.GetComponent<CompanionAIScript>())
            {
                playerData.companionData = companion.GetComponent<CompanionAIScript>().SaveToPlayerData();
            }
        }

        if (player.GetComponent<CharacterClass>())
        {
            playerData.playerClass = player.GetComponent<CharacterClass>().currentClass.ToString();
        }

        if (GetComponent<ExampleText>())
        {
            ExampleText text = GetComponent<ExampleText>();
            playerData.name = text.charName;
            playerData.deity = text.deity;
            playerData.subjectivePronoun = text.pronounSubjective;
            playerData.affiliation = text.affiliation;
        }

        if (GetComponent<SettingSkin>())
        {
            SettingSkin skin = GetComponent<SettingSkin>();
            playerData.skinTone = skin.skincolour;
        }

        PlayerWrapper wrapper = new PlayerWrapper
        {
            data = playerData
        };

        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(filePath, contents);
    }

    public void ResetSaveData()
    {
        Debug.Log("Resetting Save Data...");
        playerData = new PlayerData();
        PlayerWrapper wrapper = new PlayerWrapper
        {
            data = playerData
        };

        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(filePath, contents);
        ReadData();
        SaveData();
    }

    public void ReadData()
    {
        ReadDataLocal();

        if (player.GetComponent<PlayerInventory>())
        {
            player.GetComponent<PlayerInventory>().ReadFromPlayerData(playerData.inventoryData);
        }

        foreach (GameObject companion in companions)
        {
            if (companion.GetComponent<CompanionAIScript>())
            {
                companion.GetComponent<CompanionAIScript>().ReadFromPlayerData(playerData.companionData);
            }
        }

        if (player.GetComponent<CharacterClass>())
        {
            player.GetComponent<CharacterClass>().currentClass = CharacterClass.StringToClass(playerData.playerClass);
        }

        if (GetComponent<ExampleText>())
        {
            ExampleText text = GetComponent<ExampleText>();
            text.charName = playerData.name;
            text.deity = playerData.deity;
            text.pronounSubjective = playerData.subjectivePronoun;
            text.affiliation = playerData.affiliation;
        }

        if (GetComponent<Pronouns>())
        {
            switch (playerData.subjectivePronoun)
            {
                case "They":
                    GetComponent<Pronouns>().they = true;
                    break;
                case "He":
                    GetComponent<Pronouns>().he = true;
                    break;
                case "She":
                    GetComponent<Pronouns>().she = true;
                    break;
            }
        }

        if (GetComponent<SettingSkin>())
        {
            SettingSkin skin = GetComponent<SettingSkin>();
            skin.skincolour = playerData.skinTone;
            skin.UpdateSkinColour();
            skin.UpdateRobeColour((playerData.deity));
        }
    }

    private void ReadDataLocal()
    {
        Debug.Log("Reading Save Data...");

        string contents = System.IO.File.ReadAllText(filePath);
        PlayerWrapper wrapper = JsonUtility.FromJson<PlayerWrapper>(contents);

        playerData = wrapper.data;
    }
}
