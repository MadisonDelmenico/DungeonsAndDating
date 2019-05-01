using UnityEngine;
using System.Collections;

public class PlayAnimationSound : MonoBehaviour
{
    public void PlaySound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}