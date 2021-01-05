using UnityEngine;
using System.Collections;

public class Monster : Character
{
    [Header("Battle values")]
    public float monsterHP;

    // Use this for initialization
    protected override void Start()
    {
        this.isFacingRight = false;
        this.healthPoint = monsterHP;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        checkDie();
    }


    /// <summary>
    /// Function to move this monster to the given destination
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="speed"></param>
    public override void Move(Vector3 destination, float speed)
    {
        Vector3 direction = (destination - this.transform.position).normalized;

        // change the facing direction if necessary
        if(direction.x <= 0 && isFacingRight)
        {
            Flip();
        }
        else if(direction.x > 0 && !isFacingRight)
        {
            Flip();
        }

        // move this monster to the destination
        this.thisRB.MovePosition(this.transform.position + direction * speed * Time.deltaTime);  
    }


    public override void Move(float move, bool crouch, bool jump)
    {
        // leave blank
    }

    /// <summary>
    /// This function will be called automatically when this monster is colliding with any collider
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // if collided with monster or monster bullet, the player is considered getting attacked
        if (collision.gameObject.tag == "PlayerBullet")
        {
            this.getAttacked(1);
        }
    }
     
}
