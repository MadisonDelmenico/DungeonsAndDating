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

    public AbilityClass[] myAbilities = new AbilityClass[4];


    public AbilityClass[] allAbilities;
    // Start is called before the first frame update
    void Start()
    {
        UnpackAbilities();
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

    void UnpackAbilities()
    {
        // Abilities.txt input format
        // Name/Class/TargetRange/Effect/EnergyCost/Cooldown(sec)/CastTime(sec)
        TextAsset abilityList = Resources.Load<TextAsset>("Abilities");

        // Split the text from the file into lines
        string[] abilities = abilityList.text.Split('\n');

        // Create a local empty array of "AbilityClass" objects
        AbilityClass[] abilityArray = new AbilityClass[abilities.Length];

        for (int i = 0; i < abilities.Length; i++)
        {
            Debug.Log(abilities[i]);

            // Split the current line into strings for each value the AbilityClass has
            string[] abilityInfo = abilities[i].Split('/');

            // Create local variables for each value of the ability
            string name = abilityInfo[0];
            CharacterClass.Class useClass = CharacterClass.StringToClass(abilityInfo[1]);
            string range = abilityInfo[2];
            string effect = abilityInfo[3];
            float cost = float.Parse(abilityInfo[4]);
            float cooldown = float.Parse(abilityInfo[5]);
            float castTime = float.Parse(abilityInfo[6]);

            // Create a new instance of the AbilityClass object using the appropriate variables and assign it to the local
            AbilityClass ability = new AbilityClass(name, useClass, range, effect, cost, cooldown, castTime);
            abilityArray[i] = ability;
        }

        // Once the array is complete, assign the local array to the public one
        allAbilities = abilityArray;
    }
}
