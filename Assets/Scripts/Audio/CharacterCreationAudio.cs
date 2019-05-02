using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationAudio : MonoBehaviour
{
    private FMOD.Studio.EventInstance music;
    public GameObject mainCamera;

    void Awake()
    {
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/v1/Music_Inn");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(music, mainCamera.transform, mainCamera.GetComponent<Rigidbody>());
        music.setParameterByName("inn_music", 1);
        music.setVolume(0.4f);
    }

    private void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Environment/Env_RoomTone_Inn", mainCamera);
        music.start();
    }

    private void OnDestroy()
    {
        music.release();
    }
}
