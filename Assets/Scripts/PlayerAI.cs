using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public GameObject player;
    public bool isAttacking;
    public GameObject target;
    public float targetDistance;
    public bool beingattacked;
    public GameObject[] enemies;
    private GameObject[] companions;
    [HideInInspector]
    public bool isTalking;
    [SerializeField]
    private float castTime;
    private CharacterActions characterActions;

    [Header("My Abilities")]
    public CharacterActions.Action actionOne;
    public CharacterActions.Action actionTwo;
    public CharacterActions.Action actionThree;

    [Header("Ability Controls")]
    public KeyCode abilityOne = KeyCode.Alpha1;
    public KeyCode abilityTwo = KeyCode.Alpha2;
    public KeyCode abilityThree = KeyCode.Alpha3;

    [Header("Ability Cast Times")]
    public float defaultCastTime;
    public float fireboltCastTime;
    public float revitalizeCastTime;
    public float wildSpinCastTime;

    public string name;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        companions = GameObject.FindGameObjectsWithTag("Companion");
        isAttacking = false;
        player = gameObject;
        target = gameObject;
        targetDistance = 0;
        isTalking = false;
        name = PlayerPrefs.GetString("PName");
        characterActions = GetComponent<CharacterActions>();

    }

    // Update is called once per frame
    void Update()
    {
        if (castTime > 0)
        {
            GetComponent<NavMeshMovement>().enabled = false;
            castTime -= Time.deltaTime;
        }
        else
        {
            GetComponent<NavMeshMovement>().enabled = true;
            if (characterActions.preparedAction != CharacterActions.Action.None)
            {
                characterActions.FinishCasting();
            }
        }

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
        foreach (var i in enemies)
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

        targetDistance = Vector3.Distance(target.transform.position, player.transform.position);

        // Check the ability inputs
        if (Input.GetKeyDown(abilityOne))
        {
            characterActions.BeginCasting(actionOne, gameObject.GetComponent<TargettingEnemies>().target);
            SetCastTime(actionOne);
        }
        if (Input.GetKeyDown(abilityTwo))
        {
            characterActions.BeginCasting(actionTwo, gameObject.GetComponent<TargettingEnemies>().target);
            SetCastTime(actionTwo);
        }
        if (Input.GetKeyDown(abilityThree))
        {
            characterActions.BeginCasting(actionThree, gameObject.GetComponent<TargettingEnemies>().target);
            SetCastTime(actionThree);
        }
    }

    private void SetCastTime(CharacterActions.Action action)
    {
        switch (action)
        {
            case CharacterActions.Action.Firebolt:
                castTime = fireboltCastTime;
                break;
            case CharacterActions.Action.Revitalize:
                castTime = revitalizeCastTime;
                break;
            case CharacterActions.Action.WildSpin:
                castTime = wildSpinCastTime;
                break;
            default:
                castTime = defaultCastTime;
                break;
        }
    }

    public void CallForHelp(GameObject attacker)
    {
        foreach (var i in companions)
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
        foreach (var i in companions)
        {
            i.GetComponent<CompanionAIScript>().state = CompanionAIScript.CompanionState.Attacking;
        }
    }

    public void Talking()
    {
        if (isTalking == true)
        {
            isTalking = false;
        }
        else
        {
            isTalking = true;
        }
        GameObject.Find("DialogueManager").GetComponent<Template_UIManager>().Blur();
    }
}

