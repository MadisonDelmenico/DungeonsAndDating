using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pronouns : MonoBehaviour
{
	public bool She;
	public bool He;
	public bool They;
	public string pronounSubjective;
	public string pronounObjective;
	public string pronounPossessive;

	public string nullError;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(She == true)
		{
			pronounSubjective = "She";
			pronounObjective = "Her";
			pronounPossessive = "Hers";
			He = false;
			They = false;
		}

		if (He == true) 
		{
			pronounSubjective = "He";
			pronounObjective = "Him";
			pronounPossessive = "His";
			She = false;
			They = false;
		}

		if (They == true) 
		{
			pronounSubjective = "They";
			pronounObjective = "Them";
			pronounPossessive = "Theirs";
			She = false;
			He = false;
		}
	}

	public void Null()
	{
		//if an option remains null, deny the user from proceeding, display an error message
	}
}


