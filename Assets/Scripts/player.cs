using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float walkingSpeed = 6f;
    [SerializeField] float flyingSpeed = 12f;
    [SerializeField] float climbingSpeed = 2f;
    [SerializeField] float mass = 1f;
    [SerializeField] float jumpPower = 7f;
    
    public Transform cameraTransform;

    CharacterController controller;
    Vector3 velocity;
    Vector2 look;

    public State state;

    public enum State
    {
	Walking,
	Spectating,
	Climbing
    }

    void Start()
    {
        
    }

    void Update()
    {
	switch (state)
	{
	    case State.Walking:
		UpdateMovement();
		UpdateLook();
		UpdateGravity();
		break;
	    case State.Climbing:
		UpdateMovementClimbing();
		UpdateLook();
		break;
	    case State.Spectating:
		UpdateMovementFlying();
		UpdateLook();
		break;
	}

    }

    void Awake()
    {
	controller = GetComponent<CharacterController>();
    }

    void UpdateMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

 	var input = new Vector3(); 
	input += transform.forward * y;      
	input += transform.right * x;
	input = Vector3.ClampMagnitude(input, 1f);
	
	controller.Move((input * walkingSpeed + velocity) * Time.deltaTime);
	
	//Jumping
	if (Input.GetButtonDown("Jump") && controller.isGrounded)
	{
	    velocity.y += jumpPower;
	}

    }

    void UpdateGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -1f; // small negative value to keep grounded
        }
        else
        {
            var gravity = Physics.gravity * mass * Time.deltaTime;
            velocity.y += gravity.y;
        }
    }

    void UpdateMovementFlying()
    {
 	//var input = GetMovementInput(flyingSpeed, false); 
     
	//var factor = acceleration * Time.deltaTime;
	//velocity = Vector3.Lerp(velocity, input, factor);
	
	//controller.Move(velocity * Time.deltaTime);
    }

    void UpdateMovementClimbing()
    {
 	//var input = GetMovementInput(flyingSpeed, false); 
     
	//var factor = acceleration * Time.deltaTime;
	//velocity = Vector3.Lerp(velocity, input, factor);
	
	//controller.Move(velocity * Time.deltaTime);
    }

    void UpdateLook()
    {
        look.x += Input.GetAxis("Mouse X") * mouseSensitivity;
	look.y += Input.GetAxis("Mouse Y") * mouseSensitivity;

	look.y = Mathf.Clamp(look.y, -89f, 89f);
	
	cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
	transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }
}
