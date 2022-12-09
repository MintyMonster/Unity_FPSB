using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookScript : MonoBehaviour
{
    // Variables
    [SerializeField] float sensX = 3f;
    [SerializeField] float sensY = 0.5f;
    [SerializeField] Transform cam;
    [SerializeField] float camClampX = 85f;
    float xRotation = 0f;

    float mouseX, mouseY;

    private void Update()
    {

        Cursor.lockState = CursorLockMode.Locked; // Set state to locked
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime); // Rotation

        // Rotation logic
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -camClampX, camClampX);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;

        cam.eulerAngles = targetRotation;
    }

    // Input handling
    public void InputRecieved(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensX;
        mouseY = mouseInput.y * sensY;
    }
}
