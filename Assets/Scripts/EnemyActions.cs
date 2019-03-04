using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public enum Action { Basic, Firebolt, None }

    [HideInInspector]
    public EnemyClass enemyClass;

    [Header("My Abilities")]
    public Action actionOne = Action.Basic;
    public Action actionTwo = Action.Firebolt;

    [Header("Attack values")]
    public float attackValue;
    public float fireboltValue;

    [Header("Cooldown Values")]
    public float basicCooldown;
    public float fireboltCooldown;


    private float basicCooldownReset;
    private float fireboltCooldownReset;



    // Start is called before the first frame update 
    void Start()
    {
        enemyClass = GetComponent<EnemyClass>();

        basicCooldownReset = basicCooldown;
        fireboltCooldownReset = fireboltCooldown;


        switch (enemyClass.currentClass)
        {
            case EnemyClass.Class.Melee:
                attackValue = 3;
                break;

            case EnemyClass.Class.Ranged:
                attackValue = 2;
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