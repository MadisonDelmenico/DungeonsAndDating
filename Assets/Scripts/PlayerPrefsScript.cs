using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsScript : MonoBehaviour
{
    public GameObject Manager;
    public string PName;
    public string PGender;
    public string PClass;
    public string PDeity;
    public int PSkin;
    public int PlayerSkintone;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePlayerPrefs()
    {
        string PlayerName = Manager.GetComponent<ExampleText>().charName;
        string PlayerGender = "They";
        switch (Manager.GetComponent<Pronouns>().pronounSubjective)
        {
            case "She":
                PlayerGender = "Female";
                print("Player is Female");
                break;
            case "He":
                PlayerGender = "Male";
                print("Player is Male");
                break;
            case "They":
                PlayerGender = "Other";
                print("Player is nonbinary/other");
                break;
        }

        string PlayerClass = Manager.GetComponent<ExampleText>().playerClass;
        string PlayerDeity = Manager.GetComponent<ExampleText>().deity;
        
        PlayerPrefs.SetString("PName", PlayerName);
        PlayerPrefs.SetString("PGender", PlayerGender);
        PlayerPrefs.SetString("PClass", PlayerClass);
        PlayerPrefs.SetString("PDeity", PlayerDeity);
        PlayerPrefs.SetInt("PSkin", PlayerSkintone);

        PName = PlayerName;
        PGender = PlayerGender;
        PClass = PlayerClass;
        PDeity = PlayerDeity;
        PSkin = PlayerSkintone;
    }
}

