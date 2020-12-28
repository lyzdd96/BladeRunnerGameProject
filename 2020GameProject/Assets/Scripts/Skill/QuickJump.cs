using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickJump : Skill {
    // which Character object to apply the skill
    Character target;
    public QuickJump(Attack attack, float cooldown, Character target): base(attack, cooldown) {
        this.target = target;
        base.targets.Add(target);
    }

    public override void runSkill() {
        float verticalMove = Input.GetAxisRaw("Vertical") * 5;
        float deltaAngle = verticalMove;
        float backJumpForce = 4000f;
        bool isJumping = ((Player)target).isJumping;
        
        Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad*deltaAngle) * backJumpForce, Mathf.Sin(Mathf.Deg2Rad*deltaAngle) * backJumpForce);
        if (!this.target.isFacingRight)
            direction.x = direction.x * -1;
        
        target.thisRB.AddForce(direction);
    }
}
