using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
	public GameObject manager;
	public Scene scene;
	public Image characterpic;
	public Sprite skin1;
	public Sprite skin2;
	public Sprite skin3;
	public Sprite skin4;
	public Sprite skin5;
	public int skincolour;

	// Use this for initialization
	void Start ()
	{
		characterpic.GetComponent<Image> ().sprite = skin1;
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
		characterpic.GetComponent<Image> ().sprite = skin1;
		skincolour = 1;
	}

	public void SetSkinColour2 ()
	{
		characterpic.GetComponent<Image> ().sprite = skin2;
		skincolour = 2;
	}

	public void SetSkinColour3 ()
	{
		characterpic.GetComponent<Image> ().sprite = skin3;
		skincolour = 3;
	}

	public void SetSkinColour4 ()
	{
		characterpic.GetComponent<Image> ().sprite = skin4;
		skincolour = 4;
	}

	public void SetSkinColour5 ()
	{
		characterpic.GetComponent<Image> ().sprite = skin5;
		skincolour = 5;
	}
}
