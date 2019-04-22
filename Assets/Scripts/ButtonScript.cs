using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
	public GameObject manager;
	public Scene scene;
	public GameObject character;
	public Material skin1;
	public Material skin2;
	public Material skin3;
	public Material skin4;
	public Material skin5;
	public int skincolour;

	// Use this for initialization
	void Start ()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SkinnedMeshRenderer>().material = skin1;
        character.GetComponentInChildren<SkinnedMeshRenderer>().material = skin1;
//            .GetComponent<Material>().mainTexture = skin1;
		skincolour = 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void She ()
	{
		manager.GetComponent<Pronouns> ().She = true;
		manager.GetComponent<Pronouns> ().He = false;
		manager.GetComponent<Pronouns> ().They = false;
	}

	public void He ()
	{
		manager.GetComponent<Pronouns> ().She = false;
		manager.GetComponent<Pronouns> ().He = true;
		manager.GetComponent<Pronouns> ().They = false;
	}

	public void They ()
	{
		manager.GetComponent<Pronouns> ().She = false;
		manager.GetComponent<Pronouns> ().He = false;
		manager.GetComponent<Pronouns> ().They = true;
	}

	public void Reload ()
	{
		scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
		print ("Reloaded");
	}
	public void Confirm()
	{
		SceneManager.LoadScene ("Marketplace");
        print("Player Skin is" + PlayerPrefs.GetInt("PSkin"));
    }

	public void SetSkinColour1 ()
	{
        GameObject.FindGameObjectWithTag("Player").GetComponent<SkinnedMeshRenderer>().material = skin1;
        //		character.GetComponent<Material>().mainTexture = skin1;
        skincolour = 1;
	}

	public void SetSkinColour2 ()
	{
        GameObject.FindGameObjectWithTag("Player").GetComponent<SkinnedMeshRenderer>().material = skin2;
        //        character.GetComponent<Material>().mainTexture = skin2;
        skincolour = 2;
	}

	public void SetSkinColour3 ()
	{
        GameObject.FindGameObjectWithTag("Player").GetComponent<SkinnedMeshRenderer>().material = skin3;
        //        character.GetComponent<Material>().mainTexture = skin3;
        skincolour = 3;
	}

	public void SetSkinColour4 ()
	{
        GameObject.FindGameObjectWithTag("Player").GetComponent<SkinnedMeshRenderer>().material = skin4;
        //        character.GetComponent<Material>().mainTexture = skin4;
        skincolour = 4;
	}

	public void SetSkinColour5 ()
	{
        GameObject.FindGameObjectWithTag("Player").GetComponent<SkinnedMeshRenderer>().material = skin5;
        //        character.GetComponent<Material>().mainTexture = skin5;
        skincolour = 5;
	}
}
