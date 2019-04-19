using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMouseOver : MonoBehaviour
{
	public GameObject toTurnOn;
	private Ray ray;
	private RaycastHit hit;
    private GameObject player;

	// Use this for initialization
	private void Start ()
	{
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	private void Update ()
	{
		
	}

	public void TurnOff()
	{
		toTurnOn.SetActive (false);
        player.GetComponent<PlayerAI>().UIPopUp();
    }

    public void TurnOn()
    {
        toTurnOn.SetActive(true);
        player.GetComponent<PlayerAI>().UIPopUp();
    }
}

