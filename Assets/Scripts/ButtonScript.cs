using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject manager;
    public Scene scene;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void She()
    {
        manager.GetComponent<Pronouns>().she = true;
        manager.GetComponent<Pronouns>().he = false;
        manager.GetComponent<Pronouns>().they = false;
    }

    public void He()
    {
        manager.GetComponent<Pronouns>().she = false;
        manager.GetComponent<Pronouns>().he = true;
        manager.GetComponent<Pronouns>().they = false;
    }

    public void They()
    {
        manager.GetComponent<Pronouns>().she = false;
        manager.GetComponent<Pronouns>().he = false;
        manager.GetComponent<Pronouns>().they = true;
    }

    public void Deity(string deityName)
    {
        ExampleText text = GetComponent<ExampleText>();
        text.deity = deityName;

        switch (deityName)
        {
            case "Torm":
                GetComponent<ExampleText>().affiliation = "The Order of the Gauntlet";
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTorm");
                break;
            case "Sseth":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesSseth");
                GetComponent<ExampleText>().affiliation = "The Vrael Olo";
                break;
            case "Bahamut":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesBahamut");
                GetComponent<ExampleText>().affiliation = "The Talons of Justice";
                break;
            case "The Traveler":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTheTraveler");
                GetComponent<ExampleText>().affiliation = "The Vassals of the Dark Six";
                break;
            case "Mielikki":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesMielikki");
                GetComponent<ExampleText>().affiliation = "The Emerald Enclave";
                break;
            case "Tyr":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTyr");
                GetComponent<ExampleText>().affiliation = "The Knights of Holy Judgment";
                break;
            default:
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTorm");
                break;
        }

    }

    public void Reload()
    {
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        print("Reloaded");
    }
    public void Confirm()
    {
        GetComponent<DataManager>().SaveData();
        SceneManager.LoadScene("Marketplace");
    }

    public void SetSkinColour1()
    {
        SettingSkin skin = GetComponent<SettingSkin>();
        skin.skincolour = 1;
        skin.UpdateSkinColour();
    }

    public void SetSkinColour2()
    {
        SettingSkin skin = GetComponent<SettingSkin>();
        skin.skincolour = 2;
        skin.UpdateSkinColour();
    }

    public void SetSkinColour3()
    {
        SettingSkin skin = GetComponent<SettingSkin>();
        skin.skincolour = 3;
        skin.UpdateSkinColour();
    }

    public void SetSkinColour4()
    {
        SettingSkin skin = GetComponent<SettingSkin>();
        skin.skincolour = 4;
        skin.UpdateSkinColour();
    }

    public void SetSkinColour5()
    {
        SettingSkin skin = GetComponent<SettingSkin>();
        skin.skincolour = 5;
        skin.UpdateSkinColour();
    }
}
