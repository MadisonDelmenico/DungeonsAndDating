using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	public GameObject manager;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void She()
	{
		manager.GetComponent<Pronouns> ().She = true;
		manager.GetComponent<Pronouns> ().He = false;
		manager.GetComponent<Pronouns> ().They = false;
	}
	public void He()
	{
		manager.GetComponent<Pronouns> ().She = false;
		manager.GetComponent<Pronouns> ().He = true;
		manager.GetComponent<Pronouns> ().They = false;
	}
	public void They()
	{
		manager.GetComponent<Pronouns> ().She = false;
		manager.GetComponent<Pronouns> ().He = false;
		manager.GetComponent<Pronouns> ().They = true;
	}
}
