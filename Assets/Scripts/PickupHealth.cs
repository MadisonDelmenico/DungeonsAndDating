using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour
{

  
    public float healAmount;
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
        
        if (other.GetComponent<Health>().health < other.GetComponent<Health>().maxHealth)
        {
            other.GetComponent<Health>().health += healAmount;
            Destroy(this.gameObject);
        }
    }
}
