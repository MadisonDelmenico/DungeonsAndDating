using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMouseOver : MonoBehaviour
{
	public GameObject toTurnOn;
	Ray ray;
	RaycastHit hit;
    GameObject player;

	// Use this for initialization
	void Start ()
	{
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject == this.gameObject) {
					toTurnOn.SetActive (true);
                    player.GetComponent<PlayerAI>().UIPopUp();
				}
			}
		}
	}

	public void TurnOff()
	{
		toTurnOn.SetActive (false);
        player.GetComponent<PlayerAI>().UIPopUp();
    }
}

