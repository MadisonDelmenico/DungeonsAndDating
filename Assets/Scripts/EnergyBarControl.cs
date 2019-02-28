using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarControl : MonoBehaviour
{
    public Energy target;
    Image energyBar;

    // Start is called before the first frame update
    void Start()
    {
        energyBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        energyBar.fillAmount = (target.energy / target.maxEnergy);
    }
}
