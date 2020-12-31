using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to transform the user's controls to player motions and animations
public class PlayerMovementController : MotionController
{
	public Player player;
	public Animator thisAnimator;
	public float runSpeed = 20f;
	public float quickMoveCooldown = 1;
	public GameObject dashEffect;
	public CameraShake CameraParent;

	private float horizontalMove = 0f;
	//private bool isFiring = false;
	private bool isJumping = false;
	private bool isCrouching = false;
    //private bool isQuickMoving = false;
	private bool isRunning = false;
	private float quickMoveCooldownTimer = 0;
	private Skill quickMoveSkill;



    private void Start()
    {
		base.animator = thisAnimator;
		this.quickMoveSkill = new QuickMove(null, quickMoveCooldown, player);
		quickMoveCooldownTimer = quickMoveCooldown;

    }

	private void UpdateCooldown() {
		quickMoveCooldownTimer += Time.deltaTime;
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
			//isFiring = true;
		}
		else if(Input.GetButtonUp("Fire"))
		{
			//isFiring = false;
		}

		
		if (Input.GetButtonDown("QuickMove") && quickMoveCooldownTimer > quickMoveCooldown && isRunning)
		{
			QuickJump();
		}

		// check if we need to trigger the dying animation
		if(player.isDead)
        {
			destroy();
		}

	}

	void QuickJump() {
		//isQuickMoving = true;
		this.quickMoveSkill.runSkill();
		quickMoveCooldownTimer = 0;
		//isFiring = false;
		if (!player.isJumping) {
			animator.SetTrigger("Crouch");
		} else {
			animator.Play("Jump", 0, 0f);
		}
		Destroy(Instantiate(dashEffect, transform.position, Quaternion.identity), 1);
		CameraParent.ShakeCamera(0.5f, 0.005f);
	}

	// used for physical updates
	void FixedUpdate()
	{
		// Move our character
		player.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
		isJumping = false;
		//isQuickMoving = false;
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

	/// <summary>
	/// Function to play destroying animation
	/// </summary>
	protected override void destroy()
	{
		// we won't destroy the player game object

		animator.SetTrigger("IsDying");  // trigger the dying animation
		// deactivate the player animator
		//this.player.gameObject.GetComponent<Animator>().enabled = false;

		// deactivate the player moving sript
		this.player.gameObject.GetComponent<PlayerMovementController>().enabled = false;
	}



}
