using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to transform the user's controls to player motions and animations
public class PlayerMovementController : MotionController
{
	public Player player;
	public Animator thisAnimator;
	public float runSpeed = 20f;
	public float backJumpCooldown = 1;

	private float horizontalMove = 0f;
	private bool isFiring = false;
	private bool isJumping = false;
	private bool isCrouching = false;
    private bool isBackJumping = false;
	private bool isRunning = false;
	private float backJumpCooldownTimer = 0;
	private Skill backJumpSkill;



    private void Start()
    {
		base.animator = thisAnimator;
		this.backJumpSkill = new QuickJump(null, backJumpCooldown, player);
		backJumpCooldownTimer = backJumpCooldown;

    }

	private void UpdateCooldown() {
 	 	backJumpCooldownTimer += Time.deltaTime;
	}

    // Update is called once per frame
    void Update()
	{
		this.UpdateCooldown();
		// get the user's input of horizontal movement
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		isRunning = Mathf.Abs(horizontalMove) > 1;

		// detect the player speed and update the animator parameter
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		// detect whether player has pressed Jump and update the animator parameter
		if (Input.GetButtonDown("Jump"))
		{
			isJumping = true;
			animator.SetBool("IsJumping", this.isJumping);
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
			isFiring = true;
		}
		else if(Input.GetButtonUp("Fire"))
		{
			isFiring = false;
		}

		
		if (Input.GetButtonDown("BackJump") && backJumpCooldownTimer > backJumpCooldown && isRunning)
		{
			isBackJumping = true;
			this.backJumpSkill.runSkill();
			backJumpCooldownTimer = 0;
		}
		if (isBackJumping) {
			isFiring = false;
			if (!player.isJumping) {
				animator.SetTrigger("IsBackJumping");
			}
		}
		isBackJumping = false;
		animator.SetBool("IsShooting", isFiring);
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
		isJumping = false;
		animator.SetBool("IsJumping", isJumping);
	}

	public void OnCrouching(bool isCrouching)
	{
		// animator.SetBool("IsCrouching", isCrouching);
	}



}
