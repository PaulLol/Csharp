using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    CharacterController charControl;
    
    const float walkSpeedBasic = 2f;
    float walkSpeed=2f;
    public float sprintSpeed;
    public float jumpForce = 30.0f;
    public float gravityJump = 14.0f;
    public float gravity = -9.8f;

    private float verticalVelocity;

    void Start()
    {
        charControl = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            walkSpeed = walkSpeed + sprintSpeed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            walkSpeed = walkSpeedBasic;
        }

        Vector3 moveDirSide= transform.right * horiz * walkSpeed;
        Vector3 moveDirForward= transform.forward * vert * walkSpeed;

        charControl.SimpleMove(moveDirSide);
        charControl.SimpleMove(moveDirForward);

        float deltaX = Input.GetAxis("Horizontal") * walkSpeed;
        float deltaZ = Input.GetAxis("Vertical") * walkSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, walkSpeed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charControl.Move(movement);


        if (charControl.isGrounded)
        {
            verticalVelocity = -gravityJump * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravityJump * Time.deltaTime;
        }

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        charControl.Move(jumpVector * Time.deltaTime);

    }

}


