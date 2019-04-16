﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutButton : MonoBehaviour
{
    public bool isSelected;
    public Vector3 unselectedPos;
    public Vector3 selectedPos;
    public CompanionAIScript companion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (companion.isRecruited)
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            isSelected = false;
            GetComponent<Image>().color = Color.gray;
        }

        if (isSelected)
        {
            transform.localPosition = selectedPos;
        }
        else
        {
            transform.localPosition = unselectedPos;
        }
    }

    public void Toggle()
    {
        if (companion.isRecruited)
        {
            if (isSelected)
            {
                isSelected = false;
            }
            else
            {
                isSelected = true;
            }
        }
        else
        {
            print("Companion Not Recruited");
        }
    }
}
