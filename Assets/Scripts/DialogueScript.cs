using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
           
            //initiate my dialogue script
            GameObject.Find("DialogueManager").GetComponent<Template_UIManager>().Interact(GetComponent<VIDE_Assign>());

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
   
    //if im able to be recruited via the dialogue
    public void RecruitCompanion()
    {
        //i am recruited, set my affection from 0 to 1
        gameObject.GetComponent<CompanionAIScript>().isRecruited = true;
        GetComponent<AffectionRating>().affectionLevel = 1;
        //tell the player to stop talking to me
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().Talking();
    }
   
    //if the dialogue chain has ended, stop talking to me
    public void EndConversation()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().Talking();
    }
   
    //Give a response in the dialogue based on my current affection level
    public void AffectionLevel()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<AffectionRating>().CalculateQuarters();
        switch (GameObject.FindGameObjectWithTag("Player").GetComponent<AffectionRating>().quarters)
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
    public void EnableShopUI()
    {
        GetComponent<Shop>();
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
