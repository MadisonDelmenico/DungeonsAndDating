using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityClass
{

    public string abilityName;
    public string userClass;
    public string abilityEffect;
    public float cooldownTime;
    public float useTime;


    public AbilityClass(string newAbilityName, string newUserClass, string newAbilityEffect, float newCooldownTime, float newUseTime)
    {
        abilityName = newAbilityName;
        userClass = newUserClass;
        abilityEffect = newAbilityEffect;
        cooldownTime = newCooldownTime;
        useTime = newUseTime;
    }
}
