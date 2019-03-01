using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingGenerator : MonoBehaviour
{
   
    public Texture2D lightingMap;
    public ColourToPrefab[] colorMappings;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < lightingMap.width; x++)
        {
            for (int y = 0; y < lightingMap.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = lightingMap.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            //the pixel is transparent, lets ignore it!
            return;
        }

        foreach (ColourToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.Color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x, 0, y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}
