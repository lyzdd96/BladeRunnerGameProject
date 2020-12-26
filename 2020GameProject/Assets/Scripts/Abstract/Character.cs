using UnityEngine;
using System.Collections;


// An abstract class for characters
// will be inherited by the player and monsters
public abstract class Character : MonoBehaviour
{
    protected float healthPoint { get; set; }  // HP of this character

    protected CapsuleCollider2D capsCollider;      //The collider component attached to this object(the bullet game object which has a script that derives this abstract class).
    public Rigidbody2D thisRB { get; set; }                //The Rigidbody2D component attached to this object(the bullet game object which has a script that derives this abstract class).
    public bool isFacingRight { get; set; } = true;  // For determining which way the player is currently facing.
    public bool isGrounded { get; set; } = true;          // Whether or not the player is grounded.

    // Use this for initialization
    //Protected, virtual functions can be overridden by inheriting classes.
    protected virtual void Start()
    {
        //Get a component reference to this object's CapsuleCollider
        capsCollider = GetComponent<CapsuleCollider2D>();

        //Get a component reference to this object's Rigidbody2D
        thisRB = GetComponent<Rigidbody2D>();

    }

    /// <summary>
    /// Function to decrease the HP of character
    /// </summary>
    /// <param name="damage"></param>
    protected void getAttacked(int damage)
    {
        this.healthPoint -= damage;
    }


    public abstract void Move(float move, bool crouch, bool jump);

    protected void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
