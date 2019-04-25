using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlurToggle : MonoBehaviour
{
    private SuperBlur.SuperBlur blur;

    private float targetBlur = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        blur = GetComponent<SuperBlur.SuperBlur>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetBlur == 0 && blur.interpolation > 0.0f)
        {
            blur.interpolation -= Time.deltaTime;
        }
        if (targetBlur == 1 && blur.interpolation < 1.0f)
        {
            blur.interpolation += Time.deltaTime;
            blur.downsample = 1;
        }
        if (blur.interpolation < 0)
        {
            blur.interpolation = 0;
            blur.downsample = 0;
            blur.iterations = 1;
        }
        if (blur.interpolation > 1)
        {
            blur.interpolation = 1;
        }
    }

    public void ToggleBlur()
    {
        if (targetBlur == 1.0f)
        {
            targetBlur = 0.0f;
           // GameObject.Find("NPC_Sprite").SetActive(false);
        }
        else
        {
            targetBlur = 1.0f;
            blur.iterations = 2;
           // GameObject.Find("NPC_Sprite").SetActive(true);

        }
    }
}
