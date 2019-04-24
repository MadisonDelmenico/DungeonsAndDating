using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pronouns : MonoBehaviour
{
	public bool she;
	public bool he;
	public bool they;
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
		if(she == true)
		{
			pronounSubjective = "She";
			pronounObjective = "Her";
			pronounPossessive = "Hers";
			he = false;
			they = false;
		}

		if (he == true) 
		{
			pronounSubjective = "He";
			pronounObjective = "Him";
			pronounPossessive = "His";
			she = false;
			they = false;
		}

		if (they == true) 
		{
			pronounSubjective = "They";
			pronounObjective = "Them";
			pronounPossessive = "Theirs";
			she = false;
			he = false;
		}
	}

	public void Null()
	{
		//if an option remains null, deny the user from proceeding, display an error message
	}
}


