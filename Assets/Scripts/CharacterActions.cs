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
    public float attackValue;
    public float fireboltValue;
    public float wildspinValue;
    public float RevitalizeValue;
    public float elementalsphereValue;



    [Header("Cooldown Values")]
    public float basicCooldown;
    public float fireboltCooldown;
    public float revitalizeCooldown;
    public float wildSpinCooldown;
    public float elementalSphereCooldown;

    private float basicCooldownReset;
    private float fireboltCooldownReset;
    private float revitalizeCooldownReset;
    private float wildSpinCooldownReset;
    private float elementalSphereCooldownReset;

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

        basicCooldownReset = basicCooldown;
        fireboltCooldownReset = fireboltCooldown;
        revitalizeCooldownReset = revitalizeCooldown;
        wildSpinCooldownReset = wildSpinCooldown;
        elementalSphereCooldownReset = elementalSphereCooldown;

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
        basicCooldown -= Time.deltaTime;
        fireboltCooldown -= Time.deltaTime;
        revitalizeCooldown -= Time.deltaTime;
        wildSpinCooldown -= Time.deltaTime;
        elementalSphereCooldown -= Time.deltaTime;


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
                if (basicCooldown <= 0)
                {
                    Debug.Log("Boop!");
                    target.GetComponent<Health>().health -= affectionLevel * attackValue;
                    Debug.Log(target.name);
                    basicCooldown = basicCooldownReset;
                    break;
                }
                else
                {
                    break;

                }



            case Action.Firebolt:
                if (fireboltCooldown <= 0)
                {
                    if (target.CompareTag("Enemy"))
                    {
                        Debug.Log("Pew Pew Firebolt!");
                        target.GetComponent<Health>().health -= affectionLevel * fireboltValue;
                        fireboltCooldown = fireboltCooldownReset;
                    }
                    else
                    {
                        Debug.Log("I don't want to hurt the " + target.name);
                    }
                }


                break;

            case Action.Revitalize:
                if (revitalizeCooldown <= 0)
                {
                    Debug.Log("Healing Spell!");
                    if (target.CompareTag("Enemy"))
                    {
                        if (GetComponent<CompanionAIScript>())
                        {
                            gameObject.GetComponent<CompanionAIScript>().text.text = "I cant heal an enemy!";

                        }
                        Debug.Log("I cant heal an enemy!");

                    }
                    else
                    {
                        target.GetComponent<Health>().health += affectionLevel * RevitalizeValue;
                        Debug.Log(target.name);
                        revitalizeCooldown = revitalizeCooldownReset;


                    }
                }


                break;

            case Action.WildSpin:
                if (wildSpinCooldown <= 0)
                {
                    if (target.CompareTag("Enemy"))
                    {
                        Debug.Log("Beyblade time!");
                        target.GetComponent<Health>().health -= (affectionLevel * wildspinValue);
                        wildSpinCooldown = wildSpinCooldownReset;
                    }
                }


                break;

            case Action.ElementalSphere:
                if (elementalSphereCooldown <= 0)
                {
                    if (target.CompareTag("Enemy"))
                    {
                        Debug.Log("Elemental Sphere attack!");
                        target.GetComponent<Health>().health -= (affectionLevel * elementalsphereValue);
                        elementalSphereCooldown = elementalSphereCooldownReset;
                    }
                }


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
