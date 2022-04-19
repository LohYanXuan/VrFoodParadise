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
    public GvrEditorEmulator emulatorScript;
    //private float xRotation=0f;

    void Update()
    {
        //transform.Rotate(Vector3.up * playerCam.transform.rotation.y);
        //RotatePlayer();
        isGrounded = Physics.CheckSphere(groundCheck.position, groudnDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        //eularAngY = playerCam.transform.localEulerAngles.y;
        //var characterRotation = playerCam.transform.rotation;
        float deg = emulatorScript.copyMouseX * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, deg, 0);
        //characterRotation.x = 0;
        //characterRotation.z = 0;
        //float deg = emulatorScript.copyMouseX * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(deg, Vector3.up);
    }

    //private void RotatePlayer()
    //{
    //    float mouseX = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
    //    transform.Rotate(Vector3.up * mouseX);
    //} 
}
