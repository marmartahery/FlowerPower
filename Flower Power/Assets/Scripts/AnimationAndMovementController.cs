using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    // Start is called before the first frame update

    PlayerControls playerInput;
    CharacterController characterController;
    Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = .1f; // turning player object
    float WalkingSpeed = 5.0f;
    float RunningSpeed = 10.0f;

    //--------------------------------------

    //--------------------------------------
    public Transform cam;

    private void Awake()
    {
        playerInput = new PlayerControls();
        // grabs the CharacterController that was declared
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // component is called PlayerControls (see script name)
        // CharacterMovement := ActionMap
        // Move := Actions
        // playerInput.CharacterMovement.Move.started += context => { Debug.Log(context.ReadValue<Vector2>()); }; --- CHECK PLAYER INPUT
        playerInput.CharacterMovement.Move.started += onMovementInput;
        playerInput.CharacterMovement.Move.canceled += onMovementInput;
        playerInput.CharacterMovement.Move.performed += onMovementInput;
        
        // run action
        playerInput.CharacterMovement.Run.started += onRun;
        playerInput.CharacterMovement.Run.canceled += onRun;
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
            //-----------------------------------------
            //float targetAngle = Mathf.Atan2(currentMovement.x, currentMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationFactorPerFrame, rotationFactorPerFrame);
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //CharacterController.Move(moveDir.normalized * 5f * Time.deltaTime);
        }




    }
    void onMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();  // storing player input
        currentMovement.x = currentMovementInput.x * WalkingSpeed;
        currentMovement.z = currentMovementInput.y * WalkingSpeed;

        currentRunMovement.x = currentMovementInput.x * RunningSpeed;
        currentRunMovement.z = currentMovementInput.y * RunningSpeed;

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        //isMovementPressed = true;
    }

    void handleAnimation()
    {
        // Assigning animation parameter into this script
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning"); // not working yet
        
        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }


        if(isMovementPressed && isRunPressed && isWalking)
        {
            animator.SetBool("isRunning", true);
        }

        else if (!isMovementPressed && isWalking )
        {
            animator.SetBool("isWalking", false);
        }

        if (!isRunPressed)
        {
            animator.SetBool("isRunning", false);
        }
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // turn player obj
        handleRotation();

        // connecting animation <-> movement
        handleAnimation();

        //inputs gravity to the player
        handleGravity();

        /* Time.deltaTime = time since last frame */
        
        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }

        


    }

    private void OnEnable()
    {
        // Enables the CharacterMovement Action Map
        playerInput.CharacterMovement.Enable();
    }

    private void OnDisable()
    {
        // Disables the CharacterMovement Action Map
        playerInput.CharacterMovement.Disable();
    }
}
