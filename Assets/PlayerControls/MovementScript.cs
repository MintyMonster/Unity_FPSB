using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Variables
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 6f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask turretMask;
    [SerializeField] LayerMask pathMask;
    [SerializeField] LayerMask fenceMask;
    [SerializeField] float jumpHeight = 1.5f;

    bool jump;
    bool isGrounded;
    Vector3 verticalVelo = Vector3.zero;
    Vector2 horizontalInput;

    private void Update()
    {
        // Check specific layers for grounded (jumping)
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask)
            || Physics.CheckSphere(transform.position, 0.1f, turretMask)
            || Physics.CheckSphere(transform.position, 0.1f, pathMask)
            || Physics.CheckSphere(transform.position, 0.1f, fenceMask);

        // Grounding check
        if (isGrounded) 
            verticalVelo.y = 0f;

        // Jumping
        Vector3 horizontalVelo = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelo * Time.deltaTime);

        if (jump)
        {
            if (isGrounded)
            {
                verticalVelo.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }

            jump = false;
        }


        verticalVelo.y += gravity * Time.deltaTime;
        controller.Move(verticalVelo * Time.deltaTime);
    }

    public void InputReceived(Vector2 _HI)
    {
        horizontalInput = _HI;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
}
