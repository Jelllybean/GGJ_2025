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
    private float lookY;
    private Camera cam;
    
    [Header("Movement")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float acceleration, friction, gravityAccel;
    private Vector2 moveInput;
    private Vector3 velocity;

    [Header("Visuals")] 
    [SerializeField] private Animator animator;

    private void Awake()
    {
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // aiming
        lookY = Mathf.Clamp(lookY + sensitivityY * Time.deltaTime * -lookInput.y, minCameraHeight, maxCameraHeight);
        eyes.rotation = Quaternion.Euler(lookY, 0, 0);
        
        // movement
        velocity += cam.transform.forward * (moveInput.y * acceleration * Time.deltaTime);
        velocity += cam.transform.right * (moveInput.x * acceleration * Time.deltaTime);
        if(!controller.isGrounded) velocity += Vector3.down * (gravityAccel * Time.deltaTime);

        controller.Move(velocity);

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
