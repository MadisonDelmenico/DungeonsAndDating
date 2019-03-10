using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            blur.downsample = 0;
        }
        if (targetBlur == 1 && blur.interpolation < 1.0f)
        {
            blur.interpolation += Time.deltaTime;
            blur.downsample = 1;
        }
    }

    public void ToggleBlur()
    {
        if (targetBlur == 1.0f)
        {
            targetBlur = 0.0f;
        }
        else
        {
            targetBlur = 1.0f;
        }
    }
}
