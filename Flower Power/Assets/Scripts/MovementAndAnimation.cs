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

    Animator animator;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //get inputs for WASD
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //move on the x and z axis
        //normalize is to not increase the speed when 2 keys are pressed
        Vector3 direction= new Vector3(horizontal, 0f, vertical).normalized;
        //check if we move in any direction
        if(direction.magnitude > 0.1f)
        {
            //have the player model turn at the direction with the inputs
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime );
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
        //animator.SetFloat("isWalking", (direction * speed * Time.deltaTime).magnitude);
    }
}
