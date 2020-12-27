using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickJump : Skill {
    Character target;
    public QuickJump(Attack attack, float cooldown, Character target): base(attack, cooldown) {
        this.target = target;
        base.targets.Add(target);
    }

    public override void runSkill() {
        float deltaAngle = 0f;
        float backJumpForce = 4000f;
        Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad*deltaAngle) * backJumpForce, Mathf.Sin(Mathf.Deg2Rad*deltaAngle) * backJumpForce);
        if (this.target.isFacingRight)
            direction.x = direction.x * -1;
        target.thisRB.AddForce(direction);
    }
}
