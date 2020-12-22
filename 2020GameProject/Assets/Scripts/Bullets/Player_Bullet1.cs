using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet1 : Bullet
{
    public float maxDistance;  // max distance of this bullet can reach
    public float bulletSpeed;  // speed of this bullet

    private Vector3 direction;  // the shooting direction of this bullet

    // Start is called before the first frame update
    protected override void Start()
    {
        base.flyingSpeed = bulletSpeed;
        base.Start();  // call the start() in the base class
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(this.transform.position, direction, maxDistance);
    }

    /// <summary>
    /// Function to set the shooting direction of this bullet
    /// </summary>
    /// <param name="direction"></param>
    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

}
