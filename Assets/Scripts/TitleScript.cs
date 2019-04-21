using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    // Use this for initialization
    public Scene StartScene;
    public Canvas SettingsMenu;
    public Canvas MainMenu;

    public void Startgamebutton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToggleSettings()
    {
        if (MainMenu.gameObject.activeInHierarchy)
        {
            MainMenu.gameObject.SetActive(false);
            SettingsMenu.gameObject.SetActive(true);
        } 
        else  {
            MainMenu.gameObject.SetActive(true);
            SettingsMenu.gameObject.SetActive(false);
        }  
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
