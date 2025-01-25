using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [Header("Aiming")]
    [SerializeField] private Camera camera;
    [SerializeField] private float sensitivity;
    
    [Header("Movement")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed;

    [Header("Visuals")] 
    [SerializeField] private Animator animator;
    
    
    public void Input_Move(CallbackContext input)
    {
        
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
