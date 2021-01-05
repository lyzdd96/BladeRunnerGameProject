using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMove : Skill {
    // which Character object to apply the skill
    Character target;
    MotionController movementcontroller;

    public QuickMove(Attack attack, float cooldown, Character target): base(attack, cooldown) {
        this.target = target;
        base.targets.Add(target);
        movementcontroller = target.GetComponent<PlayerMovementController>();
    }

    public override void runSkill() { 
        float verticalMove = Input.GetAxisRaw("Vertical") * 5;
        bool isJumping = false;
        bool isCrouching = false;
        if (verticalMove > 0) {
            movementcontroller.animator.SetBool("IsJumping", true);
            isJumping = true;
        }
        float deltaAngle = verticalMove;
        float backJumpForce = 4000f;

        
        Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad*deltaAngle) * backJumpForce, Mathf.Sin(Mathf.Deg2Rad*deltaAngle) * backJumpForce);

        if (!this.target.isFacingRight)
            direction.x = direction.x * -1;
        
        // target.thisRB.AddForce(direction);

        // this is better
        target.Move(direction.x * Time.fixedDeltaTime, isCrouching, isJumping);

    }
}
