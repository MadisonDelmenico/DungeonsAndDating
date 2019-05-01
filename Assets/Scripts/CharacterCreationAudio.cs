using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationAudio : MonoBehaviour
{
    private FMOD.Studio.EventInstance music;

    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Music/v1/Music_Inn", GetComponent<Transform>().position);
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/v1/Music_Inn");
        //music.setParameterByName("inn_music", 1);
        music.start();
    }

    private void OnDestroy()
    {
        music.release();
    }
}
