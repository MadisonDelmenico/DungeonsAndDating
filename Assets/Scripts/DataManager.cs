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
    public GameObject[] companions;
    public LoadoutButton[] companionButtons;
    private bool started;

    // Start is called before the first frame update
    private void Start()
    {
        // DontDestroyOnLoad(this.gameObject);

        player = GameObject.FindGameObjectWithTag("Player");

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

        // Save Player Inventory
        if (player.GetComponent<PlayerInventory>())
        {
            playerData.inventoryData = player.GetComponent<PlayerInventory>().SaveToPlayerData();
        }

        // Save Companion Data
        foreach (GameObject companion in companions)
        {
            switch (companion.GetComponent<CharacterClass>().currentClass)
            {
                case CharacterClass.Class.Barbarian:
                    playerData.companionData.strannikRecruited = companion.GetComponent<CompanionAIScript>().SaveToPlayerData();
                    break;
                case CharacterClass.Class.Paladin:
                    playerData.companionData.kalistaRecruited = companion.GetComponent<CompanionAIScript>().SaveToPlayerData();
                    break;
                case CharacterClass.Class.Sorcerer:
                    playerData.companionData.sheevaRecruited = companion.GetComponent<CompanionAIScript>().SaveToPlayerData();
                    break;
            }

        }

        // Save Loadout Data
        foreach (LoadoutButton button in companionButtons)
        {
            switch (button.companionClass)
            {
                case CharacterClass.Class.Barbarian:
                    playerData.loadoutData.strannikSelected = button.isSelected;
                    break;
                case CharacterClass.Class.Paladin:
                    playerData.loadoutData.kallistaSelected = button.isSelected;
                    break;
                case CharacterClass.Class.Sorcerer:
                    playerData.loadoutData.sheevaSelected = button.isSelected;
                    break;
            }
        }

        // Save Player Class
        if (player.GetComponent<CharacterClass>())
        {
            playerData.playerClass = player.GetComponent<CharacterClass>().currentClass.ToString();
        }

        // Save Player Preferences
        if (GetComponent<ExampleText>())
        {
            ExampleText text = GetComponent<ExampleText>();
            playerData.name = text.charName;
            playerData.deity = text.deity;
            playerData.subjectivePronoun = text.pronounSubjective;
            playerData.affiliation = text.affiliation;
        }

        // Save Player Skin Colour
        if (GetComponent<SettingSkin>())
        {
            SettingSkin skin = GetComponent<SettingSkin>();
            playerData.skinTone = skin.skincolour;
        }

        // Add save data to a wrapper
        PlayerWrapper wrapper = new PlayerWrapper
        {
            data = playerData
        };

        // Write wrapper data to JSON file
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
