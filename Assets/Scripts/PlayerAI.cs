using System.Collections;
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
    [HideInInspector]
    public bool isTalking;

    public string name;

    // Start is called before the first frame update
    void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Companions = GameObject.FindGameObjectsWithTag("Companion");
        isAttacking = false;
        player = gameObject;
        target = gameObject;
        Distance = 0;
        isTalking = false;
        name = PlayerPrefs.GetString("PName");


    }

    // Update is called once per frame
    void Update()
    {
        #region talking

        //disable movement when talking
        if (isTalking)
        {
            GetComponent<NavMeshMovement>().enabled = false;
        }
        //enable movement if not talking
        if (isTalking == false)
        {
            GetComponent<NavMeshMovement>().enabled = true;
        }

        #endregion



        if (target == null)
        {
            target = gameObject;
        }
        foreach (var i in Enemies)
        {
            if (i != null)
            {
                if (i.GetComponent<EnemyAI>().target == gameObject)
                {
                    beingattacked = true;
                    gameObject.GetComponent<TargettingEnemies>().enabled = true;
                    gameObject.GetComponent<TargettingEnemies>().target = i;
                    target = i;
                    CallForHelp(i);
                }
            }
        }
        Distance = Vector3.Distance(target.transform.position, player.transform.position);
    }

    public void CallForHelp(GameObject attacker)
    {
        foreach (var i in Companions)
        {
            if (i != null)
            {
                i.GetComponent<TargettingEnemies>().enabled = true;
                i.GetComponent<TargettingEnemies>().target = attacker;
                OrderToAttack();
            }
        }
    }

    public void OrderToAttack()
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
