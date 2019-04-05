using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : MonoBehaviour
{
    public int value;
    bool pickedUp = false;
    Transform lerpTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            transform.position = Vector3.Lerp(transform.position, lerpTarget.position, Time.deltaTime * 1.5f);
            if (Vector3.Distance(transform.position, lerpTarget.position) < 0.2f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void PickUp(Transform target)
    {
        pickedUp = true;
        lerpTarget = target;
    }
}
