using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterActions : MonoBehaviour
{
    public enum Action { Melee, Firebolt, Shield, WildSpin}
    public Action actionOne = Action.Melee;
    public Action actionTwo = Action.Firebolt;
    public Action actionThree = Action.Shield;
    public Action actionFour = Action.WildSpin;

    public KeyCode abilityOne = KeyCode.Alpha1;
    public KeyCode abilityTwo = KeyCode.Alpha2;
    public KeyCode abilityThree = KeyCode.Alpha3;
    public KeyCode abilityFour = KeyCode.Alpha4;

    public Button buttonOne;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(abilityOne))
        {
            DoActionOne();
            buttonOne.Select();
        }
        if (Input.GetKeyDown(abilityTwo))
        {
            DoActionTwo();
        }
        if (Input.GetKeyDown(abilityThree))
        {
            DoActionThree();
        }
        if (Input.GetKeyDown(abilityFour))
        {
            DoActionFour();
        }
    }

    public void DoActionOne()
    {
        DoAction(actionOne);
    }
    public void DoActionTwo()
    {
        DoAction(actionTwo);
    }
    public void DoActionThree()
    {
        DoAction(actionThree);
    }
    public void DoActionFour()
    {
        DoAction(actionFour);
    }

    void DoAction(Action action)
    {
        switch (action)
        {
            case Action.Melee:
                Debug.Log("CAM SMASH!");
                break;
            case Action.Firebolt:
                Debug.Log("Pew Pew Firebolt");
                break;
            case Action.Shield:
                Debug.Log("Bing Bong Shield");
                break;
            case Action.WildSpin:
                Debug.Log("Beyblade time!");
                break;
            default:
                break;
        }
    }
}
