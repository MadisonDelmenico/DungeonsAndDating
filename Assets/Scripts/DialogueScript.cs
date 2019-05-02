using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VIDE_Data;

public class DialogueScript : MonoBehaviour
{
    private GameObject player;

    #region affection lines
    [Header("Affection Level Lines")]
    public string levelOneBelow25;
    public string levelOneBelow50;
    public string levelOneBelow75;
    public string levelOneBelow100;

    public string levelTwoBelow25;
    public string levelTwoBelow50;
    public string levelTwoBelow75;
    public string levelTwoBelow100;

    public string levelThreeBelow25;
    public string levelThreeBelow50;
    public string levelThreeBelow75;
    public string levelThreeBelow100;

    public string levelFourBelow25;
    public string levelFourBelow50;
    public string levelFourBelow75;
    public string levelFourBelow100;

    #endregion

    private int currentXP;
    private int totalXP;
    public GameObject cameraSheva;
    public GameObject cameraSendar;
    public GameObject cameraKallista;
    public GameObject cameraStrannik;

    public GameObject giftUI;
    public Object heart;
    public bool affectionLevelUp;
    public GameObject shop;

    // Start is called before the first frame update
    void Start()
    {
        currentXP = 0;
        totalXP = 0;
        player = GameObject.FindGameObjectWithTag("Player");
       
    }

    // Update is called once per frame
    void Update()
    {
        //if the shop is open, dont move
        if (shop.activeSelf == true)
        {

            player.GetComponent<NavMeshMovement>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player moves into my collider
        if (other.gameObject == GameObject.FindWithTag("Player"))
        {
            //tell the player to stop moving
            player.GetComponent<NavMeshMovement>().meshAgent.destination = player.transform.position;

            //tell the player to look at me
            player.GetComponent<PlayerAI>().LookAt(transform.position);

            //tell the player to talk to me
            player.GetComponent<PlayerAI>().Talking();

            switch (gameObject.name)
            {
                case "Sheeva_Idle":
                    cameraSheva.GetComponent<RawImage>().enabled = true;
                    break;
                case "Strannik_Finished":
                    cameraStrannik.GetComponent<RawImage>().enabled = true;
                    ;
                    break;
                case "Kallista_Idle":
                    cameraKallista.GetComponent<RawImage>().enabled = true;
                    break;
                case "Sendar":
                    cameraSendar.GetComponent<RawImage>().enabled = true;
                    print("Sendar");
                    break;

            }

           

            if (affectionLevelUp)
            {
                switch (GetComponent<AffectionRating>().affectionLevel)
                {
                    case 2:
                        GetComponent<VIDE_Assign>().overrideStartNode = 61;
                        //VD.SetNode(61);
                      
                        break;
                    case 3:
                        GetComponent<VIDE_Assign>().overrideStartNode = 65;
                        //  VD.SetNode(65);

                        break;
                    case 4:
                        GetComponent<VIDE_Assign>().overrideStartNode = 68;
                        //  VD.SetNode(68);
                        break;

                }
                affectionLevelUp = false;
            }
            GameObject.Find("DialogueManager").GetComponent<Template_UIManager>().Interact(GetComponent<VIDE_Assign>());

            //initiate my dialogue script





        }
    }
    public void Hobbies()
    {
        string newText;
        Dictionary<string, object> options = VD.GetExtraVariables(VD.assigned.assignedDialogue, 17);
        List<string> keys = new List<string>(options.Keys);
        int randomPick = Random.Range(0, keys.Count);
        newText = (string)options[keys[randomPick]];
        VD.SetComment(VD.assigned.assignedDialogue, 17, 0, newText);

    }
    public void LikesDislikes()
    {
        string newText;
        Dictionary<string, object> options = VD.GetExtraVariables(VD.assigned.assignedDialogue, 36);
        List<string> keys = new List<string>(options.Keys);
        int randomPick = Random.Range(0, keys.Count);
        newText = (string)options[keys[randomPick]];
        VD.SetComment(VD.assigned.assignedDialogue, 36, 0, newText);

    }

    //if im able to be recruited via the dialogue
    public void RecruitCompanion()
    {
        //i am recruited, set my affection from 0 to 1
        gameObject.GetComponent<CompanionAIScript>().isRecruited = true;
        GetComponent<AffectionRating>().affectionLevel = 1;
        EndConversation();
    }

    //if the dialogue chain has ended, stop talking to me
    public void EndConversation()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().Talking();
        GameObject.Find("Camera_Sendar").GetComponent<RawImage>().enabled = false;
        GameObject.Find("Camera_Sheva").GetComponent<RawImage>().enabled = false;
        GameObject.Find("Camera_Strannik").GetComponent<RawImage>().enabled = false;
        GameObject.Find("Camera_Kallista").GetComponent<RawImage>().enabled = false;

    }

    //Give a response in the dialogue based on my current affection level
    public void AffectionLevel()
    {
        GetComponent<AffectionRating>().CalculateQuarters();
        //GameObject.FindGameObjectWithTag("Player").GetComponent<AffectionRating>().CalculateQuarters();
        switch (GetComponent<AffectionRating>().floatToInt)

        //switch (GameObject.FindGameObjectWithTag("Player").GetComponent<AffectionRating>().quarters)
        {

            case 0:
                VD.SetNode(19);

                break;
            case 1:
                VD.SetNode(20);
                break;
            case 2:
                VD.SetNode(21);
                break;
            case 3:
                VD.SetNode(22);
                break;
        }
    }

    public void TellMeAboutYourPast()
    {
        switch (gameObject.GetComponent<AffectionRating>().affectionLevel)
        {
            case 1:
                VD.SetNode(30);
                break;
            case 2:
                VD.SetNode(31);
                break;
            case 3:
                VD.SetNode(32);
                break;
            case 4:
                VD.SetNode(33);
                break;
        }
    }

    public void TellMeAboutYourAbilities()
    {
        string newText;
        //newText = "Health :" + GetComponent<Health>().maxHealth + "$" + "Attack :" + GetComponent<CharacterActions>().attackValue + "$" + "Firebolt :" + GetComponent<CharacterActions>().fireboltValue + "$" + "Wild Spin :" + GetComponent<CharacterActions>().wildSpinValue + "$" + "Revitalize :" + GetComponent<CharacterActions>().revitalizeValue;
        //newText = newText.Replace("$", "\n");

        switch (VD.assigned.assignedDialogue)
        {
            case "Kallista":
                newText = "Health :" + GetComponent<Health>().maxHealth + "$" + "Attack:" + GetComponent<CharacterActions>().attackValue + "$" +  "Revitalize:" + GetComponent<CharacterActions>().revitalizeValue;
                newText = newText.Replace("$", "\n");
                VD.SetComment(VD.assigned.assignedDialogue, 40, 0, newText);
                break;
            case "Sheva":
                newText = "Health :" + GetComponent<Health>().maxHealth + "$" + "Attack:" + GetComponent<CharacterActions>().attackValue + "$" + "Firebolt:" + GetComponent<CharacterActions>().fireboltValue;
                newText = newText.Replace("$", "\n");
                VD.SetComment(VD.assigned.assignedDialogue, 40, 0, newText);
                break;
            case "Strannik":
                newText = "Health :" + GetComponent<Health>().maxHealth + "$" + "Attack:" + GetComponent<CharacterActions>().attackValue + "$" +  "Wild Spin:" + GetComponent<CharacterActions>().wildSpinValue;
                newText = newText.Replace("$", "\n");
                VD.SetComment(VD.assigned.assignedDialogue, 40, 0, newText);
                break;          
        }
    }


    public void GiftUI()
    {
        if (giftUI.activeSelf == true)
        {
            giftUI.SetActive(false);
        }
        if (giftUI.activeSelf == false)
        {
            giftUI.SetActive(true);
        }


    }

    public void Steak()
    {
        VD.BeginDialogue("Kalista");
        VD.SetNode(44);
    }

    public void Wand()
    {
        VD.SetNode(45);
    }
    public void HolyOrderEnlistment()
    {

        VD.SetNode(46);
    }
    public void Boots()
    {
        VD.SetNode(47);
    }

    public void Whetstone()
    {
        VD.SetNode(48);
    }
    public void Cookies()
    {
        VD.SetNode(49);
    }
    public void Receipt()
    {
        VD.SetNode(50);
    }
    public void Dagger()
    {
        VD.SetNode(51);
    }
    public void ScaleKit()
    {
        VD.SetNode(52);
    }


    public void EnableShopUI()
    {
       
       
        shop.SetActive(true);
        GameObject.Find("Player").GetComponent<NavMeshMovement>().enabled = false;
        Camera.main.GetComponent<UIBlurToggle>().ToggleBlur();


    }

    public void HasSteak()
    {
        if (GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.steakOfStrength >=1)
        {
            VD.Next();
            GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.steakOfStrength--;
            switch (VD.assigned.assignedDialogue)
            {
                case "Kallista":
                    GetComponent<AffectionRating>().currentXP += 25;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Kallista_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(33, 0.5f, -12);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Sheva":
                    GetComponent<AffectionRating>().currentXP += 25;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Sheeva_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(8.8f, -0.25f, 3.2f);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Strannik":
                    GetComponent<AffectionRating>().currentXP += 50;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Strannik_Finished").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(55, -0.25f, -25);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
            }

        }
        else
        {
            VD.SetNode(75);
        }

       
    }

    public void HasWand()
    {
        if (GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.snakeShapedWand >= 1)
        {
            VD.Next();
            
            GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.snakeShapedWand--;
            switch (VD.assigned.assignedDialogue)
            {
                case "Kallista":
                    GetComponent<AffectionRating>().currentXP += 5;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Kallista_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(33, 0.5f, -12);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);

                    break;
                case "Sheva":
                    GetComponent<AffectionRating>().currentXP += 50;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Sheeva_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(8.8f, -0.25f, 3.2f);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Strannik":
                    GetComponent<AffectionRating>().currentXP += 20;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Strannik_Finished").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(55, -0.25f, -25);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
            }

           
        }
        else
        {
            VD.SetNode(75);
        }
    }

    public void HasEnlistment()
    {
        if (GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.holyOrderEnlistment >= 1)
        {
            VD.Next();
            GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.holyOrderEnlistment--;
            switch (VD.assigned.assignedDialogue)
            {
                case "Kallista":
                    GetComponent<AffectionRating>().currentXP += 50;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Kallista_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(33, 0.5f, -12);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Sheva":
                    GetComponent<AffectionRating>().currentXP += 5;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Sheeva_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(8.8f, -0.25f, 3.2f);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Strannik":
                    GetComponent<AffectionRating>().currentXP += 20;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Strannik_Finished").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(55, -0.25f, -25);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
            }
           
        }
        else
        {
            VD.SetNode(75);
        }
    }

    public void HasWhetstone()
    {
        if (GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.halberdWhetstone >= 1)
        {
            VD.Next();
            GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.halberdWhetstone--;
            switch (VD.assigned.assignedDialogue)
            {
                case "Kallista":
                    GetComponent<AffectionRating>().currentXP += 75;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Kallista_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(33, 0.5f, -12);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Sheva":
                    GetComponent<AffectionRating>().currentXP += 50;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Sheeva_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(8.8f, -0.25f, 3.2f);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Strannik":
                    GetComponent<AffectionRating>().currentXP += 100;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Strannik_Finished").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(55, -0.25f, -25);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
            }

        }
        else
        {
            VD.SetNode(75);
        }
    }

    public void HasBoots()
    {
        if (GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.spikedBoots >= 1)
        {
            VD.Next();
            GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.spikedBoots--;
            switch (VD.assigned.assignedDialogue)
            {
                case "Kallista":
                    GetComponent<AffectionRating>().currentXP += 50;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Kallista_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(33, 0.5f, -12);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Sheva":
                    GetComponent<AffectionRating>().currentXP += 100;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Sheeva_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(8.8f, -0.25f, 3.2f);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Strannik":
                    GetComponent<AffectionRating>().currentXP += 75;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Strannik_Finished").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(55, -0.25f, -25);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
            }

        }
        else
        {
            VD.SetNode(75);
        }

    }
    public void HasCookies()
    {
        if (GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.rangerScoutCookies >= 1)
        {
            VD.Next();
            GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.rangerScoutCookies--;
            switch (VD.assigned.assignedDialogue)
            {
                case "Kallista":
                    GetComponent<AffectionRating>().currentXP += 100;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Kallista_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(33, 0.5f, -12);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Sheva":
                    GetComponent<AffectionRating>().currentXP += 50;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Sheeva_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(8.8f, -0.25f, 3.2f);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Strannik":
                    GetComponent<AffectionRating>().currentXP += 75;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Strannik_Finished").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(55, -0.25f, -25);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
            }

        }
        else
        {
            VD.SetNode(75);
        }

    }
    public void HasReceipt()
    {
        if (GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.localCharityReciept >= 1)
        {
            VD.Next();
            GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.localCharityReciept--;
            switch (VD.assigned.assignedDialogue)
            {
                case "Kallista":
                    GetComponent<AffectionRating>().currentXP += 200;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Kallista_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(33, 0, -11);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Sheva":
                    GetComponent<AffectionRating>().currentXP += 75;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Sheeva_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(8.8f, -0.25f, 3.2f);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Strannik":
                    GetComponent<AffectionRating>().currentXP += 100;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Strannik_Finished").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(55, -0.25f, -25);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
            }
        }
        else
        {
            VD.SetNode(75);
        }

    }
    public void HasDagger()
    {
        if (GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.hauntedDagger >= 1)
        {
            VD.Next();
            GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.hauntedDagger--;
            switch (VD.assigned.assignedDialogue)
            {
                case "Kallista":
                    GetComponent<AffectionRating>().currentXP += 50;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Kallista_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(33, 0, -11);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Sheva":
                    GetComponent<AffectionRating>().currentXP += 200;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Sheeva_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(8.8f, -0.25f, 3.2f);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Strannik":
                    GetComponent<AffectionRating>().currentXP += 100;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Strannik_Finished").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(55, -0.25f, -25);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
            }

        }
        else
        {
            VD.SetNode(75);
        }

    }
    public void HasRepairKit()
    {
        if (GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.scaleRepairKit >= 1)
        {
            VD.Next();
            GameObject.Find("SceneManager").GetComponent<DataManager>().playerData.inventoryData.scaleRepairKit--;
            switch (VD.assigned.assignedDialogue)
            {
                case "Kallista":
                    GetComponent<AffectionRating>().currentXP += 100;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Kallista_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(33, 0, -11);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Sheva":
                    GetComponent<AffectionRating>().currentXP += 150;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Sheeva_Idle").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(8.8f, -0.25f, 3.2f);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
                case "Strannik":
                    GetComponent<AffectionRating>().currentXP += 200;
                    heart = Instantiate(Resources.Load("ParticleEffects/ParticleHeart"), GameObject.Find("Strannik_Finished").transform);
                    GameObject.Find("ParticleHeart(Clone)").transform.position = new Vector3(55, -0.25f, -25);
                    Destroy(GameObject.Find("ParticleHeart(Clone)"), 5f);
                    break;
            }

        }
        else
        {
            VD.SetNode(75);
        }

    }



    /*
    public void l()
    {

        switch (GetComponent<AffectionRating>().affectionLevel)
        {

            case 1:
                currentXP = GetComponent<AffectionRating>().currentXP;
                totalXP = GetComponent<AffectionRating>().levelOneXP;


                //if your current affection level xp is lower than 25% of the limit for this level
                if (currentXP <= (totalXP / 2) && currentXP > (totalXP / 4))
                {

                    VD.SetComment(VD.assigned.assignedDialogue, 19, 0, levelOneBelow25);
                }
                //if your current affection level xp is lower than 50%  but  higher than 25% of the limit for this level
                if (currentXP <= (totalXP / 2) && currentXP > (totalXP / 4))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 19, 0, levelOneBelow50);

                }
                //if your current affection level xp is lower than 75%  but  higher than 50% of the limit for this level
                if (currentXP <= ((totalXP / 2) + (totalXP / 4)) && currentXP > (totalXP / 2))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 19, 0, levelOneBelow75);

                }
                //if your current affection level xp is lower than 100%  but  higher than 75% of the limit for this level
                if (currentXP > ((totalXP / 2) + (totalXP / 4)))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 19, 0, levelOneBelow100);
                }


                VD.SetNode(19);
                break;

            case 2:
                currentXP = GetComponent<AffectionRating>().currentXP;
                totalXP = GetComponent<AffectionRating>().levelTwoXP;


                //if your current affection level xp is lower than 25% of the limit for this level
                if (currentXP <= (totalXP / 2) && currentXP > (totalXP / 4))
                {

                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelTwoBelow25);
                }
                //if your current affection level xp is lower than 50%  but  higher than 25% of the limit for this level
                if (currentXP <= (totalXP / 2) && currentXP > (totalXP / 4))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelTwoBelow50);

                }
                //if your current affection level xp is lower than 75%  but  higher than 50% of the limit for this level
                if (currentXP <= ((totalXP / 2) + (totalXP / 4)) && currentXP > (totalXP / 2))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelTwoBelow75);

                }

                //if your current affection level xp is lower than 100%  but  higher than 75% of the limit for this level
                if (currentXP > ((totalXP / 2) + (totalXP / 4)))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelTwoBelow100);
                }

                VD.SetNode(20);
                break;

            case 3:
                currentXP = GetComponent<AffectionRating>().currentXP;
                totalXP = GetComponent<AffectionRating>().levelThreeXP;


                //if your current affection level xp is lower than 25% of the limit for this level
                if (currentXP <= (totalXP / 2) && currentXP > (totalXP / 4))
                {

                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelThreeBelow25);
                }
                //if your current affection level xp is lower than 50%  but  higher than 25% of the limit for this level
                if (currentXP <= (totalXP / 2) && currentXP > (totalXP / 4))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelThreeBelow50);

                }
                //if your current affection level xp is lower than 75%  but  higher than 50% of the limit for this level
                if (currentXP <= ((totalXP / 2) + (totalXP / 4)) && currentXP > (totalXP / 2))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelThreeBelow50);

                }
                //if your current affection level xp is lower than 100%  but  higher than 75% of the limit for this level
                if (currentXP > ((totalXP / 2) + (totalXP / 4)))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelThreeBelow100);
                }

                VD.SetNode(21);
                break;
            case 4:
                currentXP = GetComponent<AffectionRating>().currentXP;
                totalXP = GetComponent<AffectionRating>().levelFourXP;


                //if your current affection level xp is lower than 25% of the limit for this level
                if (currentXP <= (totalXP / 2) && currentXP > (totalXP / 4))
                {

                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelFourBelow25);
                }
                //if your current affection level xp is lower than 50%  but  higher than 25% of the limit for this level
                if (currentXP <= (totalXP / 2) && currentXP > (totalXP / 4))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelFourBelow50);

                }
                //if your current affection level xp is lower than 75%  but  higher than 50% of the limit for this level
                if (currentXP <= ((totalXP / 2) + (totalXP / 4)) && currentXP > (totalXP / 2))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelFourBelow75);

                }
                //if your current affection level xp is lower than 100%  but  higher than 75% of the limit for this level
                if (currentXP > ((totalXP / 2) + (totalXP / 4)))
                {
                    VD.SetComment(VD.assigned.assignedDialogue, 20, 0, levelFourBelow100);
                }
                VD.SetNode(22);
                break;

        }
    }
*/

}
