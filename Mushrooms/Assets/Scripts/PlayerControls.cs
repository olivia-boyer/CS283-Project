using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls: MonoBehaviour { 
    public float moveSpeed;
    public float rotationSpeed;
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float jumpHeight;
    private float gravity = -9.81f;

    void Start()
    {

    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;     //all the jump stuff is just directly from the Unity Api
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        //Debug.Log(groundedPlayer);

        Vector3 move = Input.GetAxis("Vertical") * this.transform.forward;
        controller.Move(move * Time.deltaTime * moveSpeed);


        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed/10, 0);

        if (Input.GetKeyDown("space") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        playerVelocity.y += 5 * gravity * Time.deltaTime;  
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
