using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject character;
    public GameObject Anchor;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(45, 0, 0);
    }
    private void LateUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Anchor.transform.position, 0.5f * Time.deltaTime);
        gameObject.transform.LookAt(character.transform);
    }
}
