using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]

public class AbilityClass
{
    public string abilityName;
    public CharacterClass.Class usableBy;
    public string abilityRange;
    public string abilityEffect;
    public float energyCost;
    public float abilityCooldown;
    public float abilityCastTime;

    public AbilityClass(string name, CharacterClass.Class class_, string range, string effect, float cost, float cooldown, float castTime)
    {
        abilityName = name;
        usableBy = class_;
        abilityRange = range;
        abilityEffect = effect;
        energyCost = cost;
        abilityCooldown = cooldown;
        abilityCastTime = castTime;
    }
}


