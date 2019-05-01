using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldMusic : MonoBehaviour
{
    public Transform player;
    public Transform inn;
    private FMOD.Studio.EventInstance overworldMusic;
    private float distanceToInn;
    private float innnumber;

    private void Start()
    {
        overworldMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/v1/Music_Overworld");
        overworldMusic.start(); //Overworld Music
    }
    // Update is called once per frame
    void Update()
    {
        distanceToInn = Vector3.Distance(inn.position, player.position);
        //Debug.Log(distanceToInn);
        overworldMusic.setParameterByName("DistanceToInn", distanceToInn);
    }

    private void OnDestroy()
    {
        overworldMusic.release();
    }
}
