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

    public void Deity(string deityName)
    {
        PlayerPrefs.SetString("PDeity", deityName);
        print(PlayerPrefs.GetString("PDeity"));

        switch (deityName)
        {
            case "Torm":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTorm");
              PlayerPrefs.SetString("PAffiliation", "The order of the Gauntlet");
                PlayerPrefs.SetString("PAffiliationText",
                    "at the behest of your order, you have traveled to the small town of Barovia. There is an evil plague upon this town, and it is your duty to squash it - Permitted or not.");
                   ;
                break;
            case "Sseth":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesSseth");
                PlayerPrefs.SetString("PAffiliation", "The Vrael Olo");
                PlayerPrefs.SetString("PAffiliationText",
                  "at the behest of your order, you have traveled to the small town of Barovia. The people of this desolate town will make great sacrifices for Sseth. If the other monsters that dwell here don't get them first, that is.");
                break;
            case "Bahamut":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesBahamut");
                PlayerPrefs.SetString("PAffiliation", "The Talons of Justice");
                PlayerPrefs.SetString("PAffiliationText",
                  "at the behest of your order, you have traveled to the small town of Barovia. You seek the lost artifacts of Bahamut, hidden deep within the ruins just outside of town. Reclaim them for the glory of the Platinum Dragon. ");
                break;
            case "The Traveler":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTheTraveler");
                PlayerPrefs.SetString("PAffiliation", "The Vassals of the Dark Six");
                PlayerPrefs.SetString("PAffiliationText",
                    "at the behest of your order, you have traveled to the small town of Barovia. The Travelers plans for you are yet to be revealed, though you know it will come to you in the form of great change.");
                break;
            case "Mielikki":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesMielikki");
                PlayerPrefs.SetString("PAffiliation", "The Emerald Enclave");
                PlayerPrefs.SetString("PAffiliationText",
                    "at the behest of your order, you have traveled to the small town of Barovia.  As conflict grows within Barovia, it is the duty of your order to maintain the balance between nature and civilisation.");
                break;
            case "Tyr":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTyr");
                PlayerPrefs.SetString("PAffiliation", "The Knights of Holy Judgment");
                PlayerPrefs.SetString("PAffiliationText",
                    "at the behest of your order, you have traveled to the small town of Barovia. Sent here with the purpose of hunting devils, the nightly raiding of the town has caught your attention. could the devil be behind this?");
                break;
            case null:
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTorm");
                break;
        }

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
