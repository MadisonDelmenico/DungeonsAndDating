using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass : MonoBehaviour
{
    public enum Class { Barbarian, Paladin, Polymath, Sorcerer }
    public Class currentClass = Class.Polymath;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string classText = "";

            switch (currentClass)
            {
                case Class.Barbarian:
                    classText = "Barbarian - Primal Combatant.";
                    break;
                case Class.Paladin:
                    classText = "Paladin - Warrior of Holy Light.";
                    break;
                case Class.Polymath:
                    classText = "Polymath - A little bit of everything.";
                    break;
                case Class.Sorcerer:
                    classText = "Sorcerer - Inate Magical Aptitude.";
                    break;
                default:
                    break;
            }

            Debug.Log("Current class is " + classText);
        }
    }

    public static Class StringToClass(string classString)
    {
        switch (classString)
        {
            case "Barbarian":
                return Class.Barbarian;
            case "Paladin":
                return Class.Paladin;
            case "Sorcerer":
                return Class.Sorcerer;
            default:
                return Class.Polymath;
        }
    }
}
