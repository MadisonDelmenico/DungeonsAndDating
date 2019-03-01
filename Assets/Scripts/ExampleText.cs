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

	// Use this for initialization
	void Start ()
    {
		Class = "fighter";
		Affiliation = "The Emerald Enclave";
		Deity = "Tyr";
		worship = "worship";
	}
	
	// Update is called once per frame
	void Update ()
    {
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
        
		UpdateText ();
	}

	public void UpdateText()
	{
		if (CharName == "") 
		{
			CharName = "Name";
		}
		exampleText.GetComponentInParent<Text> ().text = CharName + " is a " + Class + " of " + Affiliation + ". " + pronounSubjective +" " + worship + " the Deity " + Deity;
	}
}
