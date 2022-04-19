using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groudnDistance = 0.4f;
    public LayerMask groundMask;
    public GameObject playerCam;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groudnDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = playerCam.transform.right * x + playerCam.transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //if (Input.GetButton("Dpad_Up"))
        //{
        //    //transform.position += Vector3.forward * Time.deltaTime * speed;
        //    Vector3 forward = playerCam.transform.TransformDirection(Vector3.forward);

        //    controller.SimpleMove(forward * speed);
        //}
    }
}
