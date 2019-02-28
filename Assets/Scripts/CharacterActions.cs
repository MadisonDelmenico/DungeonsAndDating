using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterActions : MonoBehaviour
{
    public enum Action { Melee, Firebolt, Revitalize, WildSpin, ElementalSphere, None }
    public Action actionOne = Action.Melee;
    public Action actionTwo = Action.Firebolt;
    public Action actionThree = Action.Revitalize;
    public Action actionFour = Action.WildSpin;

    public KeyCode abilityOne = KeyCode.Alpha1;
    public KeyCode abilityTwo = KeyCode.Alpha2;
    public KeyCode abilityThree = KeyCode.Alpha3;
    public KeyCode abilityFour = KeyCode.Alpha4;

    public AbilityClass[] abilities = new AbilityClass[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(abilityOne))
        {
            DoAction(actionOne);
        }
        if (Input.GetKeyDown(abilityTwo))
        {
            DoAction(actionTwo);
        }
        if (Input.GetKeyDown(abilityThree))
        {
            DoAction(actionThree);
        }
        if (Input.GetKeyDown(abilityFour))
        {
            DoAction(actionFour);
        }
    }

    void DoAction(Action action)
    {
        switch (action)
        {
            case Action.Melee:
                Debug.Log("Boop!");
                break;
            case Action.Firebolt:
                Debug.Log("Pew Pew Firebolt!");
                break;
            case Action.Revitalize:
                Debug.Log("Healing Spell!");
                break;
            case Action.WildSpin:
                Debug.Log("Beyblade time!");
                break;
            case Action.ElementalSphere:
                Debug.Log("Elemental Sphere attack!");
                break;
            case Action.None:
                Debug.Log("No action assigned!");
                break;
            default:
                break;
        }
    }
}
