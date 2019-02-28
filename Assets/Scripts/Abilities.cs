using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public TextAsset abilityList;
    public AbilityClass[] allAbilities;

    // Start is called before the first frame update
    void Start()
    {
        UnpackAbilities();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UnpackAbilities()
    {
        // Abilities.txt input format
        // Name/Class/TargetRange/Effect/EnergyCost/Cooldown(sec)/CastTime(sec)

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
