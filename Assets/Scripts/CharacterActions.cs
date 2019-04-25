using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    public enum Action { Basic, Firebolt, Revitalize, WildSpin, ElementalSphere, GetHealthPotion, None }

    [HideInInspector]
    public CharacterClass characterClass;

    private GameObject[] enemies;

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

    public Action preparedAction = Action.None;
    public GameObject preparedTarget;

    private float basicCooldownReset;
    private float fireboltCooldownReset;
    private float revitalizeCooldownReset;
    private float wildSpinCooldownReset;
    private float elementalSphereCooldownReset;

    private AffectionRating affectionRating;
    private int affectionLevel;

    private GameObject revitalizeParticleEffect;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

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

        LoadParticleEffects();
    }

    // Update is called once per frame
    void Update()
    {
        basicCooldown -= Time.deltaTime;
        fireboltCooldown -= Time.deltaTime;
        revitalizeCooldown -= Time.deltaTime;
        wildSpinCooldown -= Time.deltaTime;
        elementalSphereCooldown -= Time.deltaTime;

        if (GetComponent<PlayerAI>())
        {
            if (fireboltCooldown <= 1)
            {
                GetComponent<animationscript>().casting = false;
            }

            if (revitalizeCooldown <= 1)
            {
                GetComponent<animationscript>().casting = false;
            }
            if (wildSpinCooldown <= 2)
            {
                GetComponent<animationscript>().whirlwinding = false;
            }
        }
       

    }

    public void DoAction(Action action, GameObject target)
    {
        Transform targetTransform = target.transform;
        Vector3 lookPos = new Vector3(targetTransform.position.x, this.transform.position.y, targetTransform.transform.position.z);
        transform.LookAt(lookPos);

        switch (action)
        {
            case Action.Basic:
                if (basicCooldown <= 0)
                {
                    if (target != null)
                    {
                        if (target.CompareTag("Enemy"))
                        {
                           // Debug.Log("Boop!");

                            if (GetComponent<CompanionAIScript>())
                            {
                                target.GetComponent<Health>().health -= affectionLevel * attackValue;
                                GetComponentInChildren<CompanionAnimations>().Attack();

                            }
                            else
                            {
                                target.GetComponent<Health>().health -= attackValue;
                                GetComponent<animationscript>().Attack();
                            }
                           // Debug.Log(target.name);
                            basicCooldown = basicCooldownReset;
                            SendAttackerInfo(target);
                            Debug.Log("basic attack");

                        }
                    }
                }
                break;
            case Action.Firebolt:
                if (fireboltCooldown <= 0)
                {
                    if (target != null)
                    {
                        if (target.CompareTag("Enemy"))
                        {
                            Debug.Log("Pew Pew Firebolt!");
                            if (GetComponent<CompanionAIScript>())
                            {
                                target.GetComponent<Health>().health -= affectionLevel * fireboltValue;
                                GetComponentInChildren<CompanionAnimations>().Cast();

                            }
                            else
                            {
                                target.GetComponent<Health>().health -= fireboltValue;
                                GetComponent<PlayerAI>().LookAt(target.transform.position);
                                GetComponent<animationscript>().Cast();
                                GetComponent<animationscript>().casting = true;
                            }

                            SendAttackerInfo(target);
                            fireboltCooldown = fireboltCooldownReset;

                        }
                        else
                        {
                            Debug.Log("I don't want to hurt the " + target.name);
                        }
                    }
                }
                break;
            case Action.Revitalize:
                if (revitalizeCooldown <= 0)
                {
                    if (target != null)
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
                                GetComponentInChildren<CompanionAnimations>().Cast();

                            }
                            else
                            {
                                target.GetComponent<Health>().health += revitalizeValue;
                                GetComponent<PlayerAI>().LookAt(target.transform.position);
                                GetComponent<animationscript>().Cast();
                                GetComponent<animationscript>().casting = true;
                            }

                            revitalizeCooldown = revitalizeCooldownReset;
                            
                            Debug.Log(target.name);
                           
                            GameObject particle = Instantiate(revitalizeParticleEffect, target.transform.position, target.transform.rotation);
                            particle.GetComponent<ParticleFollow>().target = target.transform;
                            Destroy(particle, 5.5f);
                        }
                    }
                }
                break;
            case Action.WildSpin:
                if (wildSpinCooldown <= 0)
                {
                    //Debug.Log("Beyblade time!");
                    if (GetComponent<CompanionAIScript>())
                    {
                        foreach (GameObject enemy in enemies)
                        {
                            if (enemy != null)
                            {
                                if (Vector3.Distance(gameObject.transform.position, enemy.transform.position) < 1.0f)
                                {
                                    GetComponentInChildren<CompanionAnimations>().WildSpin();
                                    enemy.GetComponent<Health>().health -= (affectionLevel * wildSpinValue);
                                    SendAttackerInfo(enemy);
                                  
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (GameObject enemy in enemies)
                        {
                            if (enemy != null)
                            {
                                if (Vector3.Distance(gameObject.transform.position, enemy.transform.position) < 1.0f)
                                {
                                    enemy.GetComponent<Health>().health -= (wildSpinValue);
                                    SendAttackerInfo(enemy);
                                    GetComponent<animationscript>().WildSpin();
                                    GetComponent<animationscript>().whirlwinding = true;
                                }
                            }
                        }
                    }
                    wildSpinCooldown = wildSpinCooldownReset;

                }
                break;
            case Action.ElementalSphere:
                if (elementalSphereCooldown <= 0)
                {
                    if (target.CompareTag("Enemy"))
                    {
                        Debug.Log("Elemental Sphere attack!");

                        if (GetComponentInChildren<CompanionAIScript>())
                        {
                            target.GetComponent<Health>().health -= (affectionLevel * elementalSphereValue);
                            GetComponentInChildren<CompanionAnimations>().Cast();
                        }
                        else
                        {
                            target.GetComponent<Health>().health -= (elementalSphereValue);
                        }

                        SendAttackerInfo(target);
                        elementalSphereCooldown = elementalSphereCooldownReset;
                    }
                }
                break;

            case Action.GetHealthPotion:
                if (target != null)
                {
                    GetComponent<NavMeshMovement>().meshAgent.destination = target.transform.position;
                }
                

                break;

            case Action.None:
                Debug.Log("No action assigned!");
                break;
            default:
                break;
        }
    }

    public void FinishCasting()
    {
        DoAction(preparedAction, preparedTarget);
        switch (preparedAction)
        {
            case Action.Basic:
                break;
            case Action.Firebolt:
                fireboltCooldown = fireboltCooldownReset;
                break;
            case Action.Revitalize:
                revitalizeCooldown = revitalizeCooldownReset;
                break;
            case Action.WildSpin:
                wildSpinCooldown = wildSpinCooldownReset;
                break;
            case Action.ElementalSphere:
                elementalSphereCooldown = elementalSphereCooldownReset;
                break;
            case Action.None:
                break;
            default:
                break;
        }
        GetComponent<Energy>().SetEnergy(0);
        preparedAction = Action.None;
        preparedTarget = gameObject;

    }

    public void BeginCasting(Action action, GameObject target)
    {
        preparedAction = action;
        preparedTarget = target;
    }

    public void SendAttackerInfo(GameObject TargetEnemy)
    {
        TargetEnemy.GetComponent<EnemyAI>().ImBeingAttacked(gameObject);
    }

    private void LoadParticleEffects()
    {
        revitalizeParticleEffect = Resources.Load<GameObject>("ParticleEffects/RevitalizeParticle");
    }


    public void Button1()
    {
        PlayAudio("click");
        DoAction(Action.Revitalize, GetComponent<TargettingEnemies>().friendlyTarget);
        GetComponent<animationscript>().Cast();
    }

    public void Button2()
    {
        PlayAudio("click");
        DoAction(Action.Firebolt, GetComponent<TargettingEnemies>().target);
        GetComponent<animationscript>().Cast();

    }

    public void Button3()
    {
        PlayAudio("click");
        DoAction(Action.WildSpin, GetComponent<TargettingEnemies>().target);
        GetComponent<animationscript>().WildSpin();
    }

    public void PlayAudio(string type)
    {
        if (type == "click")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/UI_Main_Button_Click", GetComponent<Transform>().position);
        else
            FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Polymath/Polymath_Ability_Firebolt_Fired", GetComponent<Transform>().position); //temp till animations in
    }
}
