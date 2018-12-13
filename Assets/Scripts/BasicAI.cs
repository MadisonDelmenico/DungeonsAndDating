using Invector.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    protected vThirdPersonController cc;                // access the ThirdPersonController component                

    // Use this for initialization
    public GameObject Player;
    public float step;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Player.transform.LookAt(Player.transform, new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y, Player.transform.localPosition.z));
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, step);
            cc.input.x = 1f;
            cc.input.y = 1f;
        }
    }


    protected void Start()
    {
        CharacterInit();
    }

    protected void CharacterInit()
    {
        cc = GetComponent<vThirdPersonController>();
        if (cc != null)
            cc.Init();
    }

    protected void LateUpdate()
    {
        if (cc == null) return;             // returns if didn't find the controller	

    }

    protected void FixedUpdate()
    {
        cc.AirControl();
    }

    protected void Update()
    {
        cc.UpdateMotor();                   // call ThirdPersonMotor methods               
        cc.UpdateAnimator();                // call ThirdPersonAnimator methods	
        step = 0.3f * Time.deltaTime;
    }

}
