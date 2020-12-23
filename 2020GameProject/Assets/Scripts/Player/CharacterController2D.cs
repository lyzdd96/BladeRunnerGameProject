using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[Header("Motion values")]
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	public LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	public Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	public Transform m_CeilingCheck;                            // A position marking where to check for ceilings
	public Collider2D m_CrouchDisableCollider;              // A collider that will be disabled when crouching
	public ContactFilter2D groundContactFilter;  // for ground detection

	//const float k_GroundedRadius = 0.04f; // Radius of the overlap circle to determine if grounded
	
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	public BoolEvent OnCrouchEvent;


	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight { get; set; } = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	private bool m_wasCrouching = false;
	private bool m_Grounded;            // Whether or not the player is grounded.
	private float groundCheckTimer = 0;  // a timer for ground check, to avoid ground detection when the player just starts jumping

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		//bool wasGrounded = m_Grounded;
		//m_Grounded = false;

		/*
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This will be done using layers ("ground" layer for the terrain collider object)
		// Note: the k_GroundedRadius has to be precise such that the ray circle will exactly touch the ground.
		//       otherwise Jump action may not be performed properly
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		
		for (int i = 0; i < colliders.Length; i++)
		{
			// if the collider game object is not the player itself
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
                {
					OnLandEvent.Invoke();
					Debug.Log(colliders[i].tag);
				}
			}
		}
		*//*
		if(m_Rigidbody2D.IsTouching(groundContactFilter))
        {
			m_Grounded = true;
			if (!wasGrounded)
			{
				OnLandEvent.Invoke();
				Debug.Log("G");
			}
		} */

		
		m_Grounded = m_Rigidbody2D.IsTouching(groundContactFilter);

		groundCheckTimer += Time.deltaTime;
		// only call OnLandEvent after 0.1s of the jumping action (reset timer everytime the player jumps)
		if (groundCheckTimer >= 0.1f)
        {
			if (m_Grounded)
			{
				OnLandEvent.Invoke();
			}
		}



		//Debug.DrawRay(m_GroundCheck.position, new Vector3(0, -k_GroundedRadius, m_GroundCheck.position.z), Color.green);



		//Debug.Log(m_Grounded);
	}


	public void Move(float move, bool crouch, bool jump, bool backJump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character (pass m_Velocity by reference, and SmoothDamp will change it gradually by applying smoothing)
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

			groundCheckTimer = 0;
			groundCheckTimer += Time.deltaTime;  // update timer
		}
		/*
		// If the player is backJumping
		if (m_Grounded && backJump)
		{
            this.backJump(1250, 200);
		}*/
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void backJump(float backForce, float verticalForce)
    {
		Vector2 backJumpForce = new Vector2();
		backJumpForce.x = this.m_FacingRight ? -backForce : backForce;  // define the horizontal force sign with the player facing direction
		backJumpForce.y = verticalForce;
		// Add a force to the player to back jump
		m_Grounded = false;
		m_Rigidbody2D.AddForce(backJumpForce);

	}
}
