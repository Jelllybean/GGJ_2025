using System.Collections;
using UnityEngine;

public class MainCharacterAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to the character
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator not found! Make sure this GameObject has an Animator component.");
        }
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Walking animation (WASD keys)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Sword attack animation (Left Mouse Button)
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isAttacking");
        }

        // Reloading animation (R key)
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("isReloading");
        }
    }
}
