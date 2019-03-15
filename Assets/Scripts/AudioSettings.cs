using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBusSettings : MonoBehaviour
{
    //Created for the use of an audio slider menu, which is yet to be implimented
    //Not too sure if PlayerPrefs will be needed, will see when interface is implimented
    //Missing PlayerPref.Save - Not sure when its best to use that function.

        // - Joel

    FMOD.Studio.EventInstance SFXVolumeTestEvent;

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus UI;
    FMOD.Studio.Bus Master;
    float MusicVolume = 1f;
    float GameplaySFXVolume = 1f;
    float InterfaceSFXVolume = 1f;
    float MasterVolume = 1f;

    void Start()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/gameplay_sfx");
        UI = FMODUnity.RuntimeManager.GetBus("bus:/Master/ui_sfx");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Interface/General/UI_TestVolume");

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

    private void SFXTestSound() //Activate the demo sound to check for levels
    {
        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }
    }

    public void GameplaySFXVolumeLevel(float newGameplaySFXVolume)
    {
        GameplaySFXVolume = newGameplaySFXVolume;
        SFXTestSound();
        PlayerPrefs.SetFloat("AudioBusSettings_GameplaySFX", newGameplaySFXVolume);
    }

    public void UISFXVolumeLevel(float newUISFXVolume)
    {
        InterfaceSFXVolume = newUISFXVolume;
        SFXTestSound();
        PlayerPrefs.SetFloat("AudioBusSettings_UISFX", newUISFXVolume);
    }
}