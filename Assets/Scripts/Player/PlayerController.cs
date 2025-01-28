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
    public CapsuleCollider capsuleCollider;
    //public CapsuleCollider collider;
    float cooldownTime = 1.5f;
    float currentCooldownTime;
    Vector2 currentMovementInput;
    Vector3 currentWalkMovement;
    Vector3 currentRunMovement;
    bool isPressedMovement;
    bool isPressedRun;
    bool isJumpingAnimator;
    int isWalkingAnimator;
    int isRunningAnimator;
    int isJumpingAnimatorInt;
    int isAttackingAnimatorInt;
    public float SpeedRotation = 20.0f;
    public float RunSpeed = 5.0f;
    public float WalkSpeed = 1.0f;
    float gravity = -9.8f;
    float groundGravity = -0.05f;
    //jump
    float jumpVelocity;
    private float maxHeightJump = 5.0f;
    private float maxTimeJump = 0.8f;
    float multiplyFall = 2.0f;
    bool isPressedJump = false;
    bool isJumping = false;
    bool isPressedAttack = false;
    bool isAttacking = false;

    private void Awake()
    {
        inputSystem = new InputSystemPlayer();
        //collider = GetComponentInChildren<CapsuleCollider>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        isWalkingAnimator = Animator.StringToHash("IsWalking");
        isRunningAnimator = Animator.StringToHash("IsRunning");
        isJumpingAnimatorInt = Animator.StringToHash("IsJumping");
        isAttackingAnimatorInt = Animator.StringToHash("IsSpinning");
        currentCooldownTime = cooldownTime;
        inputSystem.PlayerControls.Move.started += callMovement;
        inputSystem.PlayerControls.Move.canceled += callMovement;
        inputSystem.PlayerControls.Move.performed += callMovement;

        inputSystem.PlayerControls.Run.started += callRun;
        inputSystem.PlayerControls.Run.canceled += callRun;

        inputSystem.PlayerControls.Jump.started += callJump;
        inputSystem.PlayerControls.Jump.canceled += callJump;

        inputSystem.PlayerControls.Attack.started += callAttack;
        inputSystem.PlayerControls.Attack.canceled += callAttack;
        updateJumpVariable();
    }
    void updateJumpVariable()
    {
        float timeToTop = maxTimeJump / 2;
        gravity = (-2 * maxHeightJump) / Mathf.Pow(timeToTop, 2);
        jumpVelocity = (2 * maxHeightJump) / timeToTop;
    }
    void callAttack(InputAction.CallbackContext ctx)
    {
        isPressedAttack = ctx.ReadValueAsButton();
    }
    void callJump(InputAction.CallbackContext ctx)
    {
        isPressedJump = ctx.ReadValueAsButton();
    }
    void callRun(InputAction.CallbackContext ctx)
    {
        isPressedRun = ctx.ReadValueAsButton();
    }
    void callMovement(InputAction.CallbackContext ctx)
    {
        currentMovementInput = ctx.ReadValue<Vector2>();
        currentWalkMovement.x = currentMovementInput.x * WalkSpeed;
        currentWalkMovement.z = currentMovementInput.y * WalkSpeed ;
        currentRunMovement.x = currentMovementInput.x * RunSpeed;
        currentRunMovement.z = currentMovementInput.y * RunSpeed;
        isPressedMovement = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }
    void LateUpdate()
    {
        handleRotation();
        handleAnimation();
        
        if (isPressedRun) 
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentWalkMovement * Time.deltaTime);
        }
        handleGravity();
        handleJump();
        handleAttack();
    }
    void handleAttack()
    {
        if (isPressedAttack && currentCooldownTime <= 0.0f)
        {
            FindAnyObjectByType<AudioManager>().Play("Spin");
            animator.SetTrigger("IsSpinTrig");
            currentCooldownTime = cooldownTime;
        }
        else
        {
            currentCooldownTime -= Time.deltaTime;
        }
    }
    void handleJump()
    {
        if (!isJumping && characterController.isGrounded && isPressedJump)
        {
            FindAnyObjectByType<AudioManager>().Play("Jump");
            animator.SetBool(isJumpingAnimatorInt, true);
            isJumpingAnimator = true;
            isJumping = true;
            currentWalkMovement.y = jumpVelocity * 0.5f;
            currentRunMovement.y = jumpVelocity * 0.5f;
        }
        else if (!isPressedJump && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }
    void handleGravity()
    {
        bool isFalling = currentWalkMovement.y <= 0.0f || !isPressedJump;
        float velcocityY;
        float newVelocityY;
        float nextVelocityY;
        if (characterController.isGrounded)
        {
            if (isJumpingAnimator)
            {
                animator.SetBool(isJumpingAnimatorInt, false);
                isJumpingAnimator = false;
            }
            animator.SetBool("IsJumping", false);
            currentWalkMovement.y = groundGravity;
            currentRunMovement.y = groundGravity;
        }
        else if (isFalling)
        {
            velcocityY = currentWalkMovement.y;
            newVelocityY = currentWalkMovement.y + (gravity * multiplyFall * Time.deltaTime);
            nextVelocityY = (velcocityY + newVelocityY) * 0.5f;
            currentWalkMovement.y = nextVelocityY;
            currentRunMovement.y = nextVelocityY;
        }
        else
        {
            velcocityY = currentWalkMovement.y;
            newVelocityY = currentWalkMovement.y + (gravity * Time.deltaTime);
            nextVelocityY = (velcocityY + newVelocityY) * 0.5f;
            currentWalkMovement.y = nextVelocityY;
            currentRunMovement.y = nextVelocityY;
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
    void enableAttack()
    {
        capsuleCollider.enabled = true;
    }
    void disableAttack()
    {
        capsuleCollider.enabled = false;
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
