using UnityEngine;
using System.Collections;


// Class to control the monster motion and animation
public class FlyingMonsterMovementController : MotionController
{
	public Animator thisAnimator;
	public float movingSpeed = 10f;

	private GameObject player;
	private Monster monster;
	private Vector3 currentPosition;

	private bool isDestroyed = false; 



    private void Start()
    {
		base.animator = thisAnimator;
		monster = GetComponent<Monster>();

		currentPosition = this.transform.position;

		// get the player gameObject from the game flow manager
		player = GameObject.Find("GameManager").GetComponent<GameFlowManager>().getPlayer();
	}



    // Update is called once per frame
    void Update()
	{

		/*
		// detect whether player has pressed Fire and update the animator parameter
		if (Input.GetButtonDown("Fire"))
		{
			animator.SetBool("IsShooting", true);
		}
		else if(Input.GetButtonUp("Fire"))
		{
			animator.SetBool("IsShooting", false);
		}*/

		// check for destroying condition
		if(monster.checkHP() && !isDestroyed)
        {
			isDestroyed = true;
			// deactivate the monster moving sript
			this.gameObject.GetComponent<FlyingMonsterMovementController>().enabled = false;
			this.gameObject.GetComponent<FlyingMonsterAttackController>().enabled = false;
			animator.SetTrigger("IsDying");
			// destroy() this monster will be called when the animation finished (using animation event setting)
        }



	}

	// used for physical updates
	void FixedUpdate()
	{
		// Move our character
		//monster.Move(player.transform.position, movingSpeed);

		// calculate the current velocity using the positions
		float currentSpeed = (this.monster.transform.position - currentPosition).magnitude / Time.deltaTime;
		currentPosition = this.monster.transform.position;

		// use the monster speed to update the animator parameter
		animator.SetFloat("Speed", Mathf.Abs(currentSpeed));

	}


	/// <summary>
    /// Function to perform wandering action for this monster
    /// </summary>
	public void wander()
    {

    }

	/// <summary>
    /// Function to perform path finding to the target
    /// </summary>
    /// <param name="target"></param>
	public void pathFinding(Vector3 target)
    {
		// start SmoothMovement co-routine passing in the Vector2 end as destination
		//StartCoroutine(ConstantMovement(destination));
		monster.Move(target, movingSpeed);
	}

	/// <summary>
	/// Event function that resets the parameter in animator
	/// Will be invoked in CharacterController class
	/// </summary>
	public override void OnLanding()
	{
		//animator.SetBool("IsJumping", false);

	}

	public void OnCrouching(bool isCrouching)
	{
		//animator.SetBool("IsCrouching", isCrouching);
	}


}
