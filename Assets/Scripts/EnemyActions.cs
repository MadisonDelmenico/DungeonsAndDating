using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public enum Action { Basic, Firebolt, None }

    [HideInInspector]
    public CharacterClass characterClass;

    [Header("My Abilities")]
    public Action actionOne = Action.Basic;
    public Action actionTwo = Action.Firebolt;

    [Header("Attack values")]
    public float attackValue;
    public float fireboltValue;
    public float wildspinValue;

    [Header("Cooldown Values")]
    public float basicCooldown;
    public float fireboltCooldown;


    private float basicCooldownReset;
    private float fireboltCooldownReset;


    [Header("Abilities")]
    public AbilityClass[] myAbilities = new AbilityClass[4];
    public AbilityClass[] allAbilities;


    // Start is called before the first frame update 
    void Start()
    {
        characterClass = GetComponent<CharacterClass>();

        basicCooldownReset = basicCooldown;
        fireboltCooldownReset = fireboltCooldown;


        switch (characterClass.currentClass)
        {
            case CharacterClass.Class.Barbarian:
                attackValue = 3;
                break;

            case CharacterClass.Class.Paladin:
                attackValue = 2;
                break;
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
    }

    public void DoAction(Action action, GameObject target)
    {
        switch (action)
        {
            case Action.Basic:
                if (basicCooldown <= 0)
                {
                    Debug.Log(gameObject.name + ": just attacked " + target.name);

                    Debug.Log(target.name);
                    target.GetComponent<Health>().health -= attackValue;
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
                        Debug.Log(gameObject.name + ": just cast firebolt on  " + target.name);
                        target.GetComponent<Health>().health -= fireboltValue;
                        fireboltCooldown = fireboltCooldownReset;

                    }
                    else
                    {
                        Debug.Log("I don't want to hurt the " + target.name);
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

}