using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [Header("Aiming")]
    [SerializeField] private Transform eyes;
    [SerializeField] private float sensitivityX, sensitivityY;
    [SerializeField] private float minCameraHeight, maxCameraHeight;
    private Vector2 lookInput;
    private float lookX, lookY;
    private Camera cam;
    
    [Header("Movement")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float acceleration, friction, gravityAccel;
    private Vector2 moveInput;
    private Vector3 velocity;

    [Header("Visuals")] 
    [SerializeField] private Animator animator;
    [SerializeField] private Transform model;
    [SerializeField] private float modelRotationSpeed;

    private void Awake()
    {
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // aiming
        lookY = Mathf.Clamp(lookY + sensitivityY * -lookInput.y, minCameraHeight, maxCameraHeight);
        lookX = lookX + sensitivityX * lookInput.x;
        eyes.rotation = Quaternion.Euler(lookY, lookX, 0);
        if (model != null)
        {
            model.rotation = Quaternion.Lerp(model.rotation, Quaternion.Euler(0, lookX, 0),
                Time.deltaTime * modelRotationSpeed);
        }

    // movement
        velocity += cam.transform.forward * (moveInput.y * acceleration);
        velocity += cam.transform.right * (moveInput.x * acceleration);
        if(!controller.isGrounded) velocity += Vector3.down * (gravityAccel);

        controller.Move(velocity * Time.deltaTime);

        velocity -= new Vector3(velocity.x * friction * Time.deltaTime, velocity.y,
            velocity.z * friction * Time.deltaTime);
    }

    public void Input_Move(CallbackContext input)
    {
        moveInput = input.ReadValue<Vector2>();
    }
    public void Input_Aim(CallbackContext input)
    {
        lookInput = input.ReadValue<Vector2>();
    }
    public void Input_Bubble(CallbackContext input)
    {
        
    }
    public void Input_Dart(CallbackContext input)
    {
        
    }
    public void Input_Pause(CallbackContext input)
    {
        
    }
}
