using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int money;

    public int commonTreasure;
    public int uncommonTreasure;
    public int rareTreasure;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickupCoin>())
        {
            money += other.GetComponent<PickupCoin>().value;
            Destroy(other.gameObject);
        }   
    }
}
