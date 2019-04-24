using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name = "Name";
    public string subjectivePronoun = "They";
    public string playerClass = "Polymath";
    public string deity = "Torm";
    public string affiliation = "The Order of the Gauntlet";
    public int skinTone = 1;
    public PlayerInventoryData inventoryData = new PlayerInventoryData();
    public PlayerCompanionData companionData = new PlayerCompanionData();
}
