using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleScript : MonoBehaviour, IPointerClickHandler
{
    // Use this for initialization
    public Scene StartScene;
    public string type;

    public void Startgamebutton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AudioButton(string type)
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

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(name + " Game Object Clicked!");
    }

}
