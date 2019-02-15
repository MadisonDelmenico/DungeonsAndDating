using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    // Start is called before the first frame update
    List<AbilityClass> abilities = new List<AbilityClass>();
    void Start()
    {


        abilities.Add(new AbilityClass("melee", "all", "The user attacks one enemy with their weapon", 1, 0));
        abilities.Add(new AbilityClass("shield", "Paladin & Polymath", "A magic barrier protects the Player from damage", 5, 0));
        abilities.Add(new AbilityClass("wild spin", "Barbarian & Polymath", "The user spins, twirling their weapon around them and dealing damage to any enemies in range", 5, 3));
        abilities.Add(new AbilityClass("firebolt", "Enemy & Polymath", "A ball of fire is flung in the direction the caster is facing, dealing fire damage on contact with an enemy", 3, 1));
        abilities.Add(new AbilityClass("Element Sphere", "Sorcerer", "Fires an elemental ball of magic in a straight line, exploding on contact with an enemy", 3, 1));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (AbilityClass i in abilities)
            {
                Debug.Log(i.abilityName + ". " + i.abilityEffect + ". Usable by " + i.userClass);
            }

                
        }

    }
}
