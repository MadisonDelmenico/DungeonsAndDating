using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterActions : MonoBehaviour
{
    public enum Action { Basic, Firebolt, Revitalize, WildSpin, ElementalSphere, None }

    [HideInInspector]
    public CharacterClass characterClass;

    [Header("My Abilities")]
    public Action actionOne = Action.Basic;
    public Action actionTwo = Action.Firebolt;
    public Action actionThree = Action.Revitalize;
    public Action actionFour = Action.WildSpin;
    [Header("Controls (PLAYER ONLY, set to none for companions)")]
    public KeyCode abilityOne = KeyCode.Alpha1;
    public KeyCode abilityTwo = KeyCode.Alpha2;
    public KeyCode abilityThree = KeyCode.Alpha3;
    public KeyCode abilityFour = KeyCode.Alpha4;

    [Header("Attack values")]
    public int attackValue;
    public int fireboltValue;
    public int wildspinValue;
    public int RevitalizeValue;
    public int elementalsphereValue;

    [Header("Cooldown Values")]
    public float abilityOneCooldown;
    public float abilityTwoCooldown;
    public float abilityThreeCooldown;
    public float abilityFourCooldown;

    [HideInInspector]
    public AffectionRating affectionRating;

    [HideInInspector]
    public int affectionLevel;

    [Header("Abilities")]
    public AbilityClass[] myAbilities = new AbilityClass[4];


    public AbilityClass[] allAbilities;
    // Start is called before the first frame update
    void Start()
    {
        affectionRating = GetComponent<AffectionRating>();
        affectionLevel = affectionRating.affectionLevel;
        characterClass = GetComponent<CharacterClass>();
        UnpackAbilities();
        switch (characterClass.currentClass)
        {
            case CharacterClass.Class.Barbarian:

                attackValue = 3;

                break;

            case CharacterClass.Class.Paladin:

                attackValue = 2;

                break;
                ;
            case CharacterClass.Class.Polymath:
                attackValue = 2;

                break;

            case CharacterClass.Class.Sorcerer:
                attackValue = 1;

                break;

            default:
                break;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(abilityOne))
        {
            DoAction(actionOne, gameObject.GetComponent<TargettingEnemies>().target);
        }
        if (Input.GetKeyDown(abilityTwo))
        {
            DoAction(actionTwo, gameObject.GetComponent<TargettingEnemies>().target);
        }
        if (Input.GetKeyDown(abilityThree))
        {
            DoAction(actionThree, gameObject.GetComponent<TargettingEnemies>().target);
        }
        if (Input.GetKeyDown(abilityFour))
        {
            DoAction(actionFour, gameObject.GetComponent<TargettingEnemies>().target);
        }
    }

    public void DoAction(Action action, GameObject target)
    {
        switch (action)
        {
            case Action.Basic:
                Debug.Log("Boop!");
                target.GetComponent<Health>().health -= affectionLevel * attackValue;
                Debug.Log(target.name);
                break;

            case Action.Firebolt:
                Debug.Log("Pew Pew Firebolt!");
                target.GetComponent<Health>().health -= affectionLevel * fireboltValue;
                break;

            case Action.Revitalize:
                Debug.Log("Healing Spell!");
                if (target.CompareTag("Enemy"))
                {
                    if (GetComponent<CompanionAIScript>().text)
                    {
                        gameObject.GetComponent<CompanionAIScript>().text.text = "I cant heal an enemy!";
                        Debug.Log("I cant heal an enemy!");

                    }
                }
                else
                {
                    target.GetComponent<Health>().health +=(affectionLevel * RevitalizeValue);

                }
                break;

            case Action.WildSpin:
                Debug.Log("Beyblade time!");
                target.GetComponent<Health>().health -=(affectionLevel * wildspinValue);
                break;
            case Action.ElementalSphere:
                Debug.Log("Elemental Sphere attack!");
                target.GetComponent<Health>().health -=(affectionLevel * elementalsphereValue);
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
