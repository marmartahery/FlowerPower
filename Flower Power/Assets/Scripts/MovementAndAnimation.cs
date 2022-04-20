using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndAnimation : MonoBehaviour
{
    //what drives the player
    public CharacterController Controller;
    //reference to the camera
    public Transform cam;
    //speed of character
    public float speed=6f;

    //for gravity
    Vector3 velocity;
    bool isGrounded;
    public float gravity = -9.8f;

    public Transform groundcheck;
    public float GroundDistance = 0.4f;
    public LayerMask groundMask;

    Animator animator;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Interaction component
    PlayerInteraction playerinteraction;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //gets the component from interaction
        playerinteraction = GetComponentInChildren<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement for the character
        movement();
        //check if the player is on an object with Ground tag
        CheckOnGround();
        //Runs the function that handles interactions
        Interact();
    }

    public void Interact()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //interact
            playerinteraction.Interact();
        }
        //TODO set up item interaction
    }

    public void CheckOnGround()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, GroundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    public void movement()
    {
        //get inputs for WASD
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //move on the x and z axis
        //normalize is to not increase the speed when 2 keys are pressed
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //for gravityy
        velocity.y += gravity * Time.deltaTime;
        Controller.Move(velocity * Time.deltaTime);

        //check if we move in any direction
        if (direction.magnitude > 0.1f)
        {
            //have the player model turn at the direction with the inputs
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //move in the direction of the camera
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //player moves with input
            Controller.Move(moveDir.normalized * speed * Time.deltaTime);

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
