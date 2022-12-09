using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [SerializeField] MovementScript movement;
    [SerializeField] MouseLookScript mouseLook;
    [SerializeField] ShootScript shootScript;
    [SerializeField] GunManager gunManager;
    [SerializeField] AirstrikeScript airstrike;
    [SerializeField] QuitScript quit;
    [SerializeField] InteractScript interact;

    PlayerController controls;
    PlayerController.GroundMovementActions groundMovement;
    Vector2 horizontalInput;

    Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerController();
        groundMovement = controls.GroundMovement;

        groundMovement.HorizontalMovement.performed += context => horizontalInput = context.ReadValue<Vector2>(); // Movement
        groundMovement.Jump.performed += _ => movement.OnJumpPressed(); // Jumping
        groundMovement.MouseX.performed += context => mouseInput.x = context.ReadValue<float>(); // MouseX
        groundMovement.MouseY.performed += context => mouseInput.y = context.ReadValue<float>(); // MouseY
        groundMovement.Shoot.performed += _ => gunManager.Shoot(); // Shooting
        groundMovement.Reload.performed += _ => gunManager.Reload(); // Reloading
        groundMovement.Airstrike.performed += _ => airstrike.OnQPressed(); // Airstrike
        groundMovement.Escape.performed += _ => quit.OnEscapePressed(); // Quit game
        groundMovement.Interact.performed += _ => interact.OnInteractPressed(); // Interact button
        
    }

    private void Update()
    {
        movement.InputReceived(horizontalInput); // Inputs for movement
        mouseLook.InputRecieved(mouseInput); // Inputs for mouse looking
    }

    private void OnEnable()
    {
        controls.Enable(); // Enable controls
    }

    private void OnDisable()
    {
        controls.Disable(); // Disable controls
    }
}
