﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public GameObject player;
    public bool isAttacking;
    public float Distance;
    public GameObject target;
    public bool beingattacked;
    public GameObject[] Enemies;
    private GameObject[] Companions;

    // Start is called before the first frame update
    void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Companions = GameObject.FindGameObjectsWithTag("Companion");
        isAttacking = false;
        player = gameObject;
        target = gameObject;
        Distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var i in Enemies)
        {
            if (i != null)
            {
                if (i.GetComponent<EnemyAI>().target == gameObject)
                {
                    beingattacked = true;
                    callForHelp(i);
                    gameObject.GetComponent<TargettingEnemies>().enabled = true;
                    target = i;
                }
            }
        }
        if (target == null)
        {
            target = gameObject;
        }
        Distance = Vector3.Distance(target.transform.position, player.transform.position);
    }

    public void callForHelp(GameObject attacker)
    {
        foreach (var i in Companions)
        {
            if (i != null)
            {
                i.GetComponent<TargettingEnemies>().enabled = true;
                i.GetComponent<TargettingEnemies>().target = attacker;
                target = attacker;
                orderToAttack();

            }
        }
    }

    public void orderToAttack()
    {
        foreach (var i in Companions)
        {
            switch (i.GetComponent<CharacterClass>().currentClass)
            {
                case CharacterClass.Class.Barbarian:
                    i.GetComponent<CompanionAIScript>().AttackMelee();
                    break;
                case CharacterClass.Class.Paladin:
                    i.GetComponent<CompanionAIScript>().AttackMelee();
                    break;
                case CharacterClass.Class.Sorcerer:
                    i.GetComponent<CompanionAIScript>().AttackRanged();
                    break;
            }
        }
    }
}
