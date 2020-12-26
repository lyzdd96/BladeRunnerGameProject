using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet1 : Bullet
{
    public float maxDistance;  // max distance of this bullet can reach
    public float bulletSpeed;  // speed of this bullet


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
    /// This function will be called automatically when the bullet is colliding with any collider
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Ground")
        {
            animator.SetTrigger("IsHit");
            stop();  // stop the motion of bullet when hits a monster
        }
    }




}
