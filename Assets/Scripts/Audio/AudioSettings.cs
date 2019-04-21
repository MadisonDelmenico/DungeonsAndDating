using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus UI;
    FMOD.Studio.Bus Master;
    float MusicVolume = 1f;
    float GameplaySFXVolume = 1f;
    float InterfaceSFXVolume = 1f;
    float MasterVolume = 1f;

    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/gameplay_sfx");
        UI = FMODUnity.RuntimeManager.GetBus("bus:/Master/ui_sfx");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");

        if (PlayerPrefs.HasKey("AudioBusSettings")) // Check player save for previous audio settings
        {
            GameplaySFXVolume = PlayerPrefs.GetFloat("AudioBusSettings_GameplaySFX");
            InterfaceSFXVolume = PlayerPrefs.GetFloat("AudioBusSettings_UISFX");
            MusicVolume = PlayerPrefs.GetFloat("AudioBusSettings_Music");
            MasterVolume = PlayerPrefs.GetFloat("AudioBusSettings_Master");
        }
        else { 
            PlayerPrefs.SetFloat("AudioBusSettings_Master", MasterVolume);
            PlayerPrefs.SetFloat("AudioBusSettings_Music", MusicVolume);
            PlayerPrefs.SetFloat("AudioBusSettings_GameplaySFX", GameplaySFXVolume);
            PlayerPrefs.SetFloat("AudioBusSettings_UISFX", InterfaceSFXVolume);
        }
    }

    void Update()
    {
        Music.setVolume(MusicVolume);
        SFX.setVolume(GameplaySFXVolume);
        UI.setVolume(InterfaceSFXVolume);
        Master.setVolume(MasterVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
        PlayerPrefs.SetFloat("AudioBusSettings_Master", newMasterVolume);
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
        PlayerPrefs.SetFloat("AudioBusSettings_Music", newMusicVolume);
    }

    public void GameplaySFXVolumeLevel(float newGameplaySFXVolume)
    {
        GameplaySFXVolume = newGameplaySFXVolume;
        PlayerPrefs.SetFloat("AudioBusSettings_GameplaySFX", newGameplaySFXVolume);
    }

    public void UISFXVolumeLevel(float newUISFXVolume)
    {
        InterfaceSFXVolume = newUISFXVolume;
        PlayerPrefs.SetFloat("AudioBusSettings_UISFX", newUISFXVolume);
    }

    public void SaveSlider()
    {
        PlayerPrefs.Save();
        Debug.Log("Saved");
    }

    public void AudioTest(string type)
    {
        if (type == "sfx")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/SFX_TestVolume", GetComponent<Transform>().position);
        if (type == "ui")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/UI_TestVolume", GetComponent<Transform>().position);
        if (type == "master")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/Master_TestVolume", GetComponent<Transform>().position);
    }
}