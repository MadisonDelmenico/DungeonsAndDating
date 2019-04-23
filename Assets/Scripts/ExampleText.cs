using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleText : MonoBehaviour
{
	public GameObject exampleText;
	public string CharName;
	public string Class;
	public string Affiliation;
	public string Deity;
	public GameObject characterNameInput;
	public string pronounSubjective;
	public string worship;
    public GameObject affiliation;
    public string text;
    public string affiliationtext;

	// Use this for initialization
	void Start ()
    {
		Class = "Polymath";
		Affiliation = "The Emerald Enclave";
        
		worship = "worship";

        switch (PlayerPrefs.GetString("PDeity"))
        {
            case "Bahamut":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesBahamut");
                break;
            case "Tyr":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTyr");
                break;
            case "Torm":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTorm");
                break;
            case "Mielikki":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesMielikki");
                break;
            case "The Traveler":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTheTraveler");
                break;
            case "Sseth":
                GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesSseth");
                break;
                default:
                    GameObject.FindGameObjectWithTag("Robes").GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("RobesTorm");
                break;
        }
    
	}
	
	// Update is called once per frame
	void Update ()
    {
        Deity = PlayerPrefs.GetString("PDeity");
        Affiliation = PlayerPrefs.GetString("PAffiliation");
        pronounSubjective = GetComponentInParent<Pronouns> ().pronounSubjective;
		CharName = characterNameInput.GetComponent<Text> ().text;
        if (CharName == "")
        {
            CharName = PlayerPrefs.GetString("Pname");
        }

		if (pronounSubjective == "null") 
		{
			pronounSubjective = "they";
		}
		if (pronounSubjective == "He") 
		{
			worship = "worships";
		} 
		if (pronounSubjective == "She") 
		{
			worship = "worships";
		} 
		if (pronounSubjective == "They") 
		{
			worship = "worship";
		}

        affiliation.GetComponent<Text>().text = Affiliation;
        affiliationtext = PlayerPrefs.GetString("PAffiliationText");

		UpdateText ();
	}

    public void UpdateText()
	{
		if (CharName == "") 
		{
			CharName = "Name";
		}
        text = CharName + " is a " + Class + " of " + Affiliation + ". " + pronounSubjective + " " + worship + " the Deity " + Deity + "." + '$' + affiliationtext;
        text = text.Replace('$', '\n');

        exampleText = GameObject.Find("ExampleText");
        exampleText.GetComponentInParent<Text>().text = text;
           
        
        
	}
}
