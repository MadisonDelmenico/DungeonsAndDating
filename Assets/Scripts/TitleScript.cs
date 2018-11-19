using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManager;
using System.Collections;

public class TitleScript : MonoBehaviour {

    // Use this for initialization
    void ChangeScene(string sceneName)
    {
        Application.LoadLevel(sceneName);

    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
