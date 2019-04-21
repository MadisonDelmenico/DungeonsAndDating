using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour
{

    public void playSound(string type)
    {
        if (type == "hover")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/UI_Main_Mouse_Hover", GetComponent<Transform>().position);
        if (type == "confirm")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/UI_Main_Button_Confirm", GetComponent<Transform>().position);
        if (type == "decline")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/UI_Main_Button_Decline", GetComponent<Transform>().position);
        else
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/UI_Main_Button_Click", GetComponent<Transform>().position);
    }
}
