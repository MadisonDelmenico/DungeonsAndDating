using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    public enum Action { Basic, Firebolt, Revitalize, WildSpin, ElementalSphere, None }

    [HideInInspector]
    public CharacterClass characterClass;

    [Header("My Abilities")]
    public Action actionOne = Action.Basic;
    public Action actionTwo = Action.Firebolt;
    public Action actionThree = Action.Revitalize;
    public Action actionFour = Action.WildSpin;

    [Header("Controls (PLAYER ONLY, set to none for companions)")]
    public KeyCode abilityOne = KeyCode.Alpha1;
    public KeyCode abilityTwo = KeyCode.Alpha2;
    public KeyCode abilityThree = KeyCode.Alpha3;
    public KeyCode abilityFour = KeyCode.Alpha4;

    [Header("Attack Values")]
    public float attackValue;
    public float fireboltValue;
    public float wildSpinValue;
    public float revitalizeValue;
    public float elementalSphereValue;
    
    [Header("Cooldown Values")]
    public float basicCooldown;
    public float fireboltCooldown;
    public float revitalizeCooldown;
    public float wildSpinCooldown;
    public float elementalSphereCooldown;

    private float basicCooldownReset;
    private float fireboltCooldownReset;
    private float revitalizeCooldownReset;
    private float wildSpinCooldownReset;
    private float elementalSphereCooldownReset;
    
    private AffectionRating affectionRating;
    private int affectionLevel;

    // Start is called before the first frame update
    void Start()
    {
        // If the character is a companion
        if (GetComponent<CompanionAIScript>())
        {
            // Assign the AffectionRating variables
            affectionRating = GetComponent<AffectionRating>();
            affectionLevel = affectionRating.affectionLevel;
        }

        characterClass = GetComponent<CharacterClass>();

        basicCooldownReset = basicCooldown;
        fireboltCooldownReset = fireboltCooldown;
        revitalizeCooldownReset = revitalizeCooldown;
        wildSpinCooldownReset = wildSpinCooldown;
        elementalSphereCooldownReset = elementalSphereCooldown;

        switch (characterClass.currentClass)
        {
            case CharacterClass.Class.Barbarian:
                attackValue = 3;
                GetComponent<Energy>().maxEnergy = wildSpinCooldownReset;
                break;
            case CharacterClass.Class.Paladin:
                attackValue = 2;
                GetComponent<Energy>().maxEnergy = revitalizeCooldownReset;
                break;
            case CharacterClass.Class.Polymath:
                attackValue = 2;
                break;
            case CharacterClass.Class.Sorcerer:
                attackValue = 1;
                GetComponent<Energy>().maxEnergy = elementalSphereCooldownReset;
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
        revitalizeCooldown -= Time.deltaTime;
        wildSpinCooldown -= Time.deltaTime;
        elementalSphereCooldown -= Time.deltaTime;

        // If the character is the player,
        if (gameObject.CompareTag("Player"))
        {
            // Check the ability inputs

            if (Input.GetKeyDown(abilityOne))
            {
                DoAction(actionOne, gameObject.GetComponent<TargettingEnemies>().target);
            }
            if (Input.GetKeyDown(abilityTwo))
            {
                DoAction(actionTwo, gameObject.GetComponent<TargettingEnemies>().target);
            }
            if (Input.GetKeyDown(abilityThree))
            {
                DoAction(actionThree, gameObject.GetComponent<TargettingEnemies>().target);
            }
            if (Input.GetKeyDown(abilityFour))
            {
                DoAction(actionFour, gameObject.GetComponent<TargettingEnemies>().target);
            }
        }

    }

    public void DoAction(Action action, GameObject target)
    {
        switch (action)
        {
            case Action.Basic:
                if (basicCooldown <= 0)
                {
                    Debug.Log("Boop!");

                    if (GetComponent<CompanionAIScript>())
                    {
                        target.GetComponent<Health>().health -= affectionLevel * attackValue;
                    }
                    else
                    {
                        target.GetComponent<Health>().health -= attackValue;
                    }
                    Debug.Log(target.name);
                    basicCooldown = basicCooldownReset;
                    SendAttackerInfo(target);
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
                        Debug.Log("Pew Pew Firebolt!");
                        if (GetComponent<CompanionAIScript>())
                        {
                            target.GetComponent<Health>().health -= affectionLevel * fireboltValue;
                        }
                        else
                        {
                            target.GetComponent<Health>().health -= fireboltValue;
                        }

                        fireboltCooldown = fireboltCooldownReset;
                        GetComponent<Energy>().energy = 0;
                        SendAttackerInfo(target);
                    }
                    else
                    {
                        Debug.Log("I don't want to hurt the " + target.name);
                    }
                }
                break;
            case Action.Revitalize:
                if (revitalizeCooldown <= 0)
                {
                    Debug.Log("Healing Spell!");
                    if (target.CompareTag("Enemy"))
                    {
                        if (GetComponent<CompanionAIScript>())
                        {
                            gameObject.GetComponent<CompanionAIScript>().text.text = "I cant heal an enemy!";
                        }
                        Debug.Log("I cant heal an enemy!");
                    }
                    else
                    {
                        if (GetComponent<CompanionAIScript>())
                        {
                            target.GetComponent<Health>().health += affectionLevel * revitalizeValue;
                        }
                        else
                        {
                            target.GetComponent<Health>().health += revitalizeValue;
                        }

                        Debug.Log(target.name);
                        revitalizeCooldown = revitalizeCooldownReset;
                        GetComponent<Energy>().energy = 0;
                    }
                }
                break;
            case Action.WildSpin:
                if (wildSpinCooldown <= 0)
                {
                    if (target.CompareTag("Enemy"))
                    {
                        Debug.Log("Beyblade time!");
                        if (GetComponent<CompanionAIScript>())
                        {
                            target.GetComponent<Health>().health -= (affectionLevel * wildSpinValue);
                        }
                        else
                        {
                            target.GetComponent<Health>().health -= (wildSpinValue);
                        }

                        wildSpinCooldown = wildSpinCooldownReset;
                        GetComponent<Energy>().energy = 0;
                        SendAttackerInfo(target);
                    }
                }
                break;
            case Action.ElementalSphere:
                if (elementalSphereCooldown <= 0)
                {
                    if (target.CompareTag("Enemy"))
                    {
                        Debug.Log("Elemental Sphere attack!");
                        if (GetComponent<CompanionAIScript>())
                        {
                            target.GetComponent<Health>().health -= (affectionLevel * elementalSphereValue);
                        }
                        else
                        {
                            target.GetComponent<Health>().health -= (elementalSphereValue);
                        }

                        elementalSphereCooldown = elementalSphereCooldownReset;
                        GetComponent<Energy>().energy = 0;
                        SendAttackerInfo(target);
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

    public void SendAttackerInfo(GameObject TargetEnemy)
    {
        TargetEnemy.GetComponent<EnemyAI>().ImBeingAttacked(gameObject);
    }
}
