using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to transform the user's controls to player motions and animations
public class PlayerMovementController : MotionController
{
	public Player player;
	public Animator thisAnimator;
	public float runSpeed = 20f;
	public float backJumpCooldown = 5;

	private float horizontalMove = 0f;
	private bool isJumping = false;
	private bool isCrouching = false;
    private bool isBackJumping = false;
	private float backJumpCooldownTimer = 0;
	private Skill backJumpSkill;



    private void Start()
    {
		base.animator = thisAnimator;
		this.backJumpSkill = new QuickJump(null, backJumpCooldown, player);
    }



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

		
		if (Input.GetButtonDown("BackJump"))
		{
			animator.SetTrigger("IsBackJumping");
			this.backJumpSkill.runSkill();
		}



	}

	// used for physical updates
	void FixedUpdate()
	{
		// Move our character
		player.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
		isJumping = false;
		isBackJumping = false;
	}


	/// <summary>
	/// Event function that resets the parameter in animator
	/// Will be invoked in CharacterController class
	/// </summary>
	public override void OnLanding()
	{
		animator.SetBool("IsJumping", false);
		isJumping = false;
	}

	public void OnCrouching(bool isCrouching)
	{
		// animator.SetBool("IsCrouching", isCrouching);
	}



}
