using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [Header("Aiming")]
    [SerializeField] private float sensitivity;
    private Camera camera;
    
    [Header("Movement")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float acceleration, friction, gravityAccel;
    private Vector2 inputDir;
    private Vector3 velocity;

    [Header("Visuals")] 
    [SerializeField] private Animator animator;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        velocity += camera.transform.forward * (inputDir.y * acceleration * Time.deltaTime);
        velocity += camera.transform.right * (inputDir.x * acceleration * Time.deltaTime);
        if(!controller.isGrounded) velocity += Vector3.down * (gravityAccel * Time.deltaTime);

        controller.Move(velocity);

        velocity -= new Vector3(velocity.x * friction * Time.deltaTime, velocity.y,
            velocity.z * friction * Time.deltaTime);
    }

    public void Input_Move(CallbackContext input)
    {
        inputDir = input.ReadValue<Vector2>();
    }
    public void Input_Aim(CallbackContext input)
    {
        
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
