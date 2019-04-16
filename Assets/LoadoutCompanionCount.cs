using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutCompanionCount : MonoBehaviour
{
    public LoadoutButton[] buttons;
    private int count = 0;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        count = 0;

        foreach (LoadoutButton i in buttons)
        {
            if (i.isSelected)
            {
                count++;
            }
        }

        text.text = ("Companions Selected: " + count);
    }
}
