using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMouseOver : MonoBehaviour
{
	public GameObject toTurnOn;
	Ray ray;
	RaycastHit hit;
	public Camera camera;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			ray = camera.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.CompareTag (gameObject.tag)) {
					toTurnOn.SetActive (true);
				}
			}
		}
	}

	public void TurnOff()
	{
		toTurnOn.SetActive (false);
	}
}

