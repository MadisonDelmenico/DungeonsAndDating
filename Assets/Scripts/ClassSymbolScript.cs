using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassSymbolScript : MonoBehaviour
{
    public Sprite[] deitySprites;
    public Image shieldImage;
    public ExampleText exampleText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (exampleText.deity)
        {
            case "Bahamut":
               shieldImage.sprite = deitySprites[0];
                break;
            case "Tyr":
                shieldImage.sprite = deitySprites[1];
                break;
            case "Torm":
                shieldImage.sprite = deitySprites[2];
                break;
            case "Mielikki":
                shieldImage.sprite = deitySprites[3];
                break;
            case "The Traveler":
                shieldImage.sprite = deitySprites[4];
                break;
            case "Sseth":
                shieldImage.sprite = deitySprites[5];
                break;
            default:
                shieldImage.sprite = deitySprites[2];
                break;
        }
    }
}
