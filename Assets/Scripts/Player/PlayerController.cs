using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static bool IsGamePaused = false;

    public float SpeedRotation = 20.0f;
    public float RunSpeed = 5.0f;
    public float WalkSpeed = 1.0f;
    public float CooldownTime = 1.5f;

    public CapsuleCollider CapsuleCollider;
    public PauseMenu PauseMenu;

    private InputSystemPlayer inputSystem;
    private CharacterController characterController;
    private Animator animator;

    
    private float currentCooldownTime;
    private Vector2 currentMovementInput;
    private Vector3 currentWalkMovement;
    private Vector3 currentRunMovement;

    private bool isPressedMovement;
    private bool isPressedRun;
    private bool isJumpingAnimator;

    private int isWalkingAnimator;
    private int isRunningAnimator;
    private int isJumpingAnimatorInt;

    private float gravity = -9.8f;
    private float groundGravity = -0.05f;
    //jump
    private float jumpVelocity;
    private float maxHeightJump = 5.0f;
    private float maxTimeJump = 0.8f;
    private float multiplyFall = 2.0f;
    private bool isPressedJump = false;
    private bool isJumping = false;
    private bool isPressedAttack = false;

    private void Awake()
    {
        inputSystem = new InputSystemPlayer();

        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        isWalkingAnimator = Animator.StringToHash("IsWalking");
        isRunningAnimator = Animator.StringToHash("IsRunning");
        isJumpingAnimatorInt = Animator.StringToHash("IsJumping");

        currentCooldownTime = CooldownTime;

        inputSystem.PlayerControls.Move.started += callMovement;
        inputSystem.PlayerControls.Move.canceled += callMovement;
        inputSystem.PlayerControls.Move.performed += callMovement;

        inputSystem.PlayerControls.Run.started += callRun;
        inputSystem.PlayerControls.Run.canceled += callRun;

        inputSystem.PlayerControls.Jump.started += callJump;
        inputSystem.PlayerControls.Jump.canceled += callJump;

        inputSystem.PlayerControls.Attack.started += callAttack;
        inputSystem.PlayerControls.Attack.canceled += callAttack;

        inputSystem.PlayerControls.Pause.performed += callPause;
        updateJumpVariable();
    }

    //Pause menu
    void callPause(InputAction.CallbackContext ctx)
    {
        //IsGamePaused = !IsGamePaused;
        if (IsGamePaused)
        {
            PauseMenu.GameResume();
        }
        else
        {
            PauseMenu.GamePause();
        }
    }

    //Player actions jump, attack, jump, run, movement
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
            currentCooldownTime = CooldownTime;
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
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Finish")
        {
            PauseMenu.GamePauseOnFinish();
        }
        if (collider.gameObject.tag == "Obstacle")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void enableAttack()
    {
        CapsuleCollider.enabled = true;
    }
    void disableAttack()
    {
        CapsuleCollider.enabled = false;
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
