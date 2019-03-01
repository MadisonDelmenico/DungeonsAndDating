using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerPrefs : MonoBehaviour
{
    public string PlayerName;
    public string PlayerGender;
    public string PlayerClass;
    public string PlayerDeity;
    public int PlayerSkin;

	// Use this for initialization
	void Start ()
    {
        PlayerName = PlayerPrefs.GetString("PName");
        PlayerGender = PlayerPrefs.GetString("PGender");
        PlayerClass = PlayerPrefs.GetString("Pclass");
        PlayerDeity = PlayerPrefs.GetString("PDeity");
        PlayerSkin = PlayerPrefs.GetInt("PSkin");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
