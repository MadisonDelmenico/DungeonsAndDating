using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassSymbolScript : MonoBehaviour
{
    public Sprite[] DeitySprites;

    public Image ShieldImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerPrefs.GetString("PDeity"))
        {
            case "Bahamut":
               ShieldImage.sprite = DeitySprites[0];
                break;
            case "Tyr":
                ShieldImage.sprite = DeitySprites[1];
                break;
            case "Torm":
                ShieldImage.sprite = DeitySprites[2];
                break;
            case "Mielikki":
                ShieldImage.sprite = DeitySprites[3];
                break;
            case "The Traveler":
                ShieldImage.sprite = DeitySprites[4];
                break;
            case "Sseth":
                ShieldImage.sprite = DeitySprites[5];
                break;
            default:
                ShieldImage.sprite = DeitySprites[2];
                break;
        }
    }
}
