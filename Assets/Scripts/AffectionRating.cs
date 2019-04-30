using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AffectionRating : MonoBehaviour
{
    public int affectionLevel;
    public int currentXP;
    public float currentlevelTotalXP;
    public float levelOneXP;
    public float levelTwoXP;
    public float levelThreeXP;
    public float levelFourXP;
    public float quarters;
    public float finalValue;
    public int floatToInt;

    // Start is called before the first frame update
    void Start()
    {
        affectionLevel = 1;
        currentXP = 0;
        levelOneXP = 100;
        levelTwoXP = 250;
        levelThreeXP = 500;
        levelFourXP = 1000;
        quarters = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentXP >= currentlevelTotalXP && currentXP > 0) 
        {
            affectionLevel++;
        }

        switch (affectionLevel)
        {
            case 1:
                currentlevelTotalXP = levelOneXP;
                break;
            case 2:
                currentlevelTotalXP = levelTwoXP;
                break;
            case 3:
                currentlevelTotalXP = levelThreeXP;
                break;
            case 4:
                currentlevelTotalXP = levelFourXP;
                break;
        }
        CalculateQuarters();

    }

    public void CalculateQuarters()
    {

        if (currentXP > 0)
        {
            quarters = ((currentXP / currentlevelTotalXP) * 100);
            if (quarters <= 25)
            {
                finalValue = 0;
            }

            if (quarters > 25 && quarters <= 50)
            {
                finalValue = 1;
            }

            if (quarters > 50 && quarters <= 75)
            {
                finalValue = 2;
            }

            if (quarters > 75 && quarters < 100)
            {
                finalValue = 3;
            }
        }

        quarters = finalValue;
        floatToInt = (int) quarters;
    }
}
