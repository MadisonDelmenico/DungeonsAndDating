using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public enum Class { Ranged, Melee }

    public Class currentClass = Class.Melee;

    // Update is called once per frame 
    void Update()
    {

    }
}