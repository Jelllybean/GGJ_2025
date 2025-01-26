using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [Header("Aiming")]
    [SerializeField] private CinemachineThirdPersonFollow cineCamTP;
    [SerializeField] private float sensitivityX, sensitivityY;
    [SerializeField] private float minCameraHeight, maxCameraHeight;
    private Vector2 lookInput;
    
    [Header("Movement")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float acceleration, friction, gravityAccel;
    private Vector2 moveInput;
    private Vector3 velocity;

    [Header("Visuals")] 
    [SerializeField] private Animator animator;

    private void Awake()
    {
    }

    private void Update()
    {
        // aiming
        cineCamTP.VerticalArmLength -= lookInput.y * Time.deltaTime * sensitivityY;
        cineCamTP.VerticalArmLength = Mathf.Clamp(cineCamTP.VerticalArmLength, minCameraHeight, maxCameraHeight);
        transform.Rotate(0, lookInput.x * Time.deltaTime * sensitivityX, 0);
        
        // movement
        velocity += cineCamTP.transform.forward * (moveInput.y * acceleration * Time.deltaTime);
        velocity += cineCamTP.transform.right * (moveInput.x * acceleration * Time.deltaTime);
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
