using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleText : MonoBehaviour
{
    public GameObject exampleText;
    public string charName;
    public string playerClass;
    public string affiliation;
    public string deity;
    public GameObject characterNameInput;
    public string pronounSubjective;
    public string worship;
    public Text affiliationText;
    public string text;
    public string affiliationtext;

    // Use this for initialization
    void Start()
    {
        playerClass = "Polymath";
        affiliation = "The Emerald Enclave";
        worship = "worship";
    }

    // Update is called once per frame
    void Update()
    {
        pronounSubjective = GetComponentInParent<Pronouns>().pronounSubjective;
        charName = characterNameInput.GetComponent<Text>().text;
        if (charName == "")
        {
            charName = "Player";
        }

        if (pronounSubjective == "null")
        {
            pronounSubjective = "they";
        }
        if (pronounSubjective == "He")
        {
            worship = "worships";
        }
        if (pronounSubjective == "She")
        {
            worship = "worships";
        }
        if (pronounSubjective == "They")
        {
            worship = "worship";
        }

        affiliationText.text = affiliation;
        affiliationtext = SetAffiliationText(affiliation);

        UpdateText();
    }

    public void UpdateText()
    {
        if (charName == "")
        {
            charName = "Name";
        }
        text = charName + " is a " + playerClass + " of " + affiliation + ". " + pronounSubjective + " " + worship + " the Deity " + deity + "." + '$' + affiliationtext;
        text = text.Replace('$', '\n');

        exampleText = GameObject.Find("ExampleText");
        exampleText.GetComponentInParent<Text>().text = text;
    }

    public void SetAffiliation(string _affiliation)
    {
        affiliation = _affiliation;
    }

    public void SetDeity(string _deity)
    {
        deity = _deity;
    }

    private string SetAffiliationText(string _affiliation)
    {
        switch (_affiliation)
        {
            case "The Order of the Gauntlet":
                return
                    "At the behest of your order, you have traveled to the small town of Barovia. There is an evil plague upon this town, and it is your duty to squash it - Permitted or not.";

            case "The Vrael Olo":
                return
                    "At the behest of your order, you have traveled to the small town of Barovia. The people of this desolate town will make great sacrifices for Sseth. If the other monsters that dwell here don't get them first, that is.";

            case "The Talons of Justice":
                return
                "At the behest of your order, you have traveled to the small town of Barovia. You seek the lost artifacts of Bahamut, hidden deep within the ruins just outside of town. Reclaim them for the glory of the Platinum Dragon. ";

            case "The Vassals of the Dark Six":
                return
                    "At the behest of your order, you have traveled to the small town of Barovia. The Travelers plans for you are yet to be revealed, though you know it will come to you in the form of great change.";

            case "The Emerald Enclave":
                return
                    "At the behest of your order, you have traveled to the small town of Barovia.  As conflict grows within Barovia, it is the duty of your order to maintain the balance between nature and civilisation.";

            case "The Knights of Holy Judgment":
                return
                    "At the behest of your order, you have traveled to the small town of Barovia. Sent here with the purpose of hunting devils, the nightly raiding of the town has caught your attention. could the devil be behind this?";

            default:
                return
                    "Default Affiliation Text";
        }
    }
}
