using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill {
    public Attack attack;
    public float cooldown { get; set; } = 1;

    public List<Character> targets = new List<Character>();
   
    public Skill(Attack attack, float cooldown) {
       this.attack = attack;
       this.cooldown = cooldown;
    }

    public virtual void createSkill(Transform transform) {}

    // Skill that doesn't need to spawn an Attack
    public virtual void runSkill() {}
}
