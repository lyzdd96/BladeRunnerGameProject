using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill {
    public Attack attack;
    public float cooldown { get; set; } = 1;

    public List<GameObject> targets = new List<GameObject>();
   
    public Skill(Attack attack, float cooldown) {
       this.attack = attack;
       this.cooldown = cooldown;
    }

    public virtual void createSkill(Transform transform) {}
}
