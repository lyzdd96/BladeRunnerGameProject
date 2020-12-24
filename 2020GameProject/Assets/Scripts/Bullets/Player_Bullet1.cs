using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet1 : Bullet
{
    public Animator animator;
    public float maxDistance;  // max distance of this bullet can reach
    public float bulletSpeed;  // speed of this bullet


    private Vector3 direction;  // the shooting direction of this bullet

    // Start is called before the first frame update
    protected override void Start()
    {


        base.flyingSpeed = bulletSpeed;
        base.Start();  // call the start() in the base class

        Move(this.transform.position, direction, maxDistance);  // start moving this bullet (coroutine)
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Function to set the shooting direction of this bullet
    /// </summary>
    /// <param name="direction"></param>
    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }


    /// <summary>
    /// This function will be called automatically when the bullet is colliding with any collider
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Ground")
        {
            animator.SetTrigger("IsHit");
            stop();  // stop the motion of bullet when hits a monster
        }
    }




}
