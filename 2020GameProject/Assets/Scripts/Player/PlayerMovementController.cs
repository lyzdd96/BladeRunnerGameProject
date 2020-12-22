using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to transform the user's controls to player motions and animations
public class PlayerMovementController : MonoBehaviour
{
	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 20f;

	private float horizontalMove = 0f;
	private bool isJumping = false;
	private bool isCrouching = false;

	// Update is called once per frame
	void Update()
	{
		// get the user's input of horizontal movement
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		// detect the player speed and update the animator parameter
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		// detect whether player has pressed Jump and update the animator parameter
		if (Input.GetButtonDown("Jump"))
		{
			isJumping = true;
			animator.SetBool("IsJumping", true);
		}

		/*
		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}*/

		
		// detect whether player has pressed Fire and update the animator parameter
		if (Input.GetButtonDown("Fire"))
		{
			animator.SetBool("IsShooting", true);
		}
		else if(Input.GetButtonUp("Fire"))
		{
			animator.SetBool("IsShooting", false);
		}

		if (Input.GetButtonDown("Fire2"))
		{
			animator.SetTrigger("IsHurt");
		}



	}

	// used for physical updates
	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
		isJumping = false;
	}


	/// <summary>
	/// Event function that resets the parameter in animator
	/// Will be invoked in CharacterController class
	/// </summary>
	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
		isJumping = false;
	}

	public void OnCrouching(bool isCrouching)
	{
		//animator.SetBool("IsCrouching", isCrouching);
	}



}
