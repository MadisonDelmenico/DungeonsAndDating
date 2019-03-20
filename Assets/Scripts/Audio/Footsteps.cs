using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string AudioLocation;
    public bool EnableFootstepsAudio = true;

    private LayerMask LM = 1 << 31; //AudioFloor
    private float distance = 2f;
    private float Material;
    private FMOD.Studio.EventInstance characterFootsteps;

    void Start()
    {
        characterFootsteps = FMODUnity.RuntimeManager.CreateInstance(AudioLocation);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(characterFootsteps, transform, GetComponent<Rigidbody>());
    }

    void FixedUpdate()
    {
        if (EnableFootstepsAudio)
        {
            MaterialCheck();
            //Debug.DrawRay(transform.position, Vector3.down * distance, Color.blue);
        }
    }

    void MaterialCheck()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, distance, LM))
        {
            if (hit.collider.tag == "Material: Stone")
                Material = 0f;
            else if (hit.collider.tag == "Material: Grass")
                Material = 2f;
            else if (hit.collider.tag == "Material: Wood")
                Material = 1f;
            else
                Material = 0f;
        }
    }
    //Function to use for the walking animation events.
    void PlayFootstepsEvent()
    {
        characterFootsteps.setParameterByName("Material", Material);
        characterFootsteps.start();
    }

    private void OnDestroy()
    {
        characterFootsteps.release();
    }
}
