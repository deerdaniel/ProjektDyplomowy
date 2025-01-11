using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    InputSystemPlayer inputSystem;
    CharacterController characterController;
    Animator animator;
    Vector2 currentMovementInput;
    Vector3 currentWalkMovement;
    Vector3 currentRunMovement;
    bool isPressedMovement;
    bool isPressedRun;
    int isWalkingAnimator;
    int isRunningAnimator;
    public float SpeedRotation = 20.0f;
    public float RunSpeed = 5.0f;
    public float WalkSpeed = 1.0f;
    private void Awake()
    {
        inputSystem = new InputSystemPlayer();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        isWalkingAnimator = Animator.StringToHash("IsWalking");
        isRunningAnimator = Animator.StringToHash("IsRunning");
        inputSystem.PlayerControls.Move.started += callMovement;
        inputSystem.PlayerControls.Move.canceled += callMovement;
        inputSystem.PlayerControls.Move.performed += callMovement;

        inputSystem.PlayerControls.Run.started += callRun;
        inputSystem.PlayerControls.Run.canceled += callRun;
    }
    void callRun(InputAction.CallbackContext ctx)
    {
        isPressedRun = ctx.ReadValueAsButton();
    }
    void callMovement(InputAction.CallbackContext ctx)
    {
        currentMovementInput = ctx.ReadValue<Vector2>();
        currentWalkMovement.x = currentMovementInput.x * WalkSpeed ;
        currentWalkMovement.z = currentMovementInput.y * WalkSpeed;
        currentRunMovement.x = currentMovementInput.x * RunSpeed;
        currentRunMovement.z = currentMovementInput.y * RunSpeed;
        isPressedMovement = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }
    void Update()
    {
        handleAnimation();
        handleRotation();
        handleGravity();
        if (isPressedRun) 
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentWalkMovement * Time.deltaTime);
        }     
    }
    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundGravity = -0.05f;
            currentWalkMovement.y = groundGravity;
            currentRunMovement.y = groundGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentWalkMovement.y = gravity;
            currentRunMovement.y = gravity;
        }
    }
    void handleAnimation()
    {
        bool isRunning = animator.GetBool(isRunningAnimator);
        bool isWalking = animator.GetBool(isWalkingAnimator);
        
        if (isPressedMovement && !isWalking)
        {
            animator.SetBool(isWalkingAnimator, true);
        }
        else if (!isPressedMovement && isWalking)
        {
            animator.SetBool(isWalkingAnimator, false);
        }

        if((isPressedRun && isPressedMovement) && !isRunning)
        {
            animator.SetBool(isRunningAnimator, true);
        }
        else if ((!isPressedRun || !isPressedMovement) && isRunning)
        {
            animator.SetBool(isRunningAnimator, false);
        }
    }

    void handleRotation()
    {
        Vector3 posLookAt;        
        Quaternion targetRotation;
        Quaternion rotation = transform.rotation;
        posLookAt.x = currentWalkMovement.x;
        posLookAt.y = 0f;
        posLookAt.z = currentWalkMovement.z;
       
        if (isPressedMovement)
        {
            targetRotation = Quaternion.LookRotation(posLookAt);
            transform.rotation = Quaternion.Slerp(rotation, targetRotation, SpeedRotation * Time.deltaTime );
        }
        
    }

    private void OnEnable()
    {
        inputSystem.PlayerControls.Enable();
    }
    private void OnDisable()
    {
        inputSystem.PlayerControls.Disable();
    }
}
