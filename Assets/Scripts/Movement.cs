using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 6.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection;
    private CharacterController controller;
    private Vector3 moveRot;
    public float rotSpeed;


    void Start()
    {
        controller = GetComponent<CharacterController>();

        //lets the gameobject fall down
        gameObject.transform.position = new Vector3(0, 5, 0);
    }


    // Update is called once per frame
    void Update()
    {

        if (controller.isGrounded)
        {

            //rotate the player
            transform.Rotate(0, Input.GetAxis("Rotate") * rotSpeed * Time.deltaTime, 0);

            //we are grounded, so recalculate
            //move directly from axis

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
           // moveRot = new Vector3(moveRot.x, moveRot.y + Input.GetAxis("Horizontal"), moveRot.z);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
        }
        //apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        //move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}

