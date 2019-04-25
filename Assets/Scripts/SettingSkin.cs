using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSkin : MonoBehaviour
{
    public GameObject manager;
    public int skincolour;
    public Material[] skins;

    // Use this for initialization
    void Start()
    {
        skincolour = 1;
        UpdateSkinColour();
        UpdateRobeColour("");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSkinColour()
    {
        GameObject.FindGameObjectWithTag("PlayerSkin").GetComponent<SkinnedMeshRenderer>().material = skins[skincolour - 1];
    }

    public void UpdateRobeColour(string deity)
    {
        switch (deity)
        {
            case "Torm":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTorm");
                break;
            case "Sseth":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesSseth");
                break;
            case "Bahamut":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesBahamut");
                break;
            case "The Traveler":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTheTraveler");
                break;
            case "Mielikki":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesMielikki");
                break;
            case "Tyr":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTyr");
                break;
            default:
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTorm");
                break;
        }
    }
}
