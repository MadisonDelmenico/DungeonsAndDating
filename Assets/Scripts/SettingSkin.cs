using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSkin : MonoBehaviour
{
    public GameObject manager;
    public int skincolour;
    public GameObject skin;
    public Material[] skins;
    // Use this for initialization
    void Start()
    {
        skincolour = PlayerPrefs.GetInt("PSkin") -1;


        skin.GetComponent<SkinnedMeshRenderer>().material = skins[skincolour];


    }

    // Update is called once per frame
    void Update()
    {

    }
}
