using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonAudio : MonoBehaviour
{
    public GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Music/v1/Music_Dungeon", mainCamera);
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Environment/Env_RoomTone_Dungeon", mainCamera);
    }
}
