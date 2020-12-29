using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to transform input parameters to attacks
public abstract class AttackController : MonoBehaviour
{
    public Animator animator;
    public List<Attack> attacks = new List<Attack>();
    protected List<Skill> skills = new List<Skill>();

    public int attackSelected = 0;
    protected Attack currentAttack;

    // Start is called before the first frame update
    protected abstract void Start();

    // Update is called once per frame
    protected abstract void Update();

    /// <summary>
    /// Function to read input and parameters from the model to execute an attack from list of attacks
    /// </summary>
    protected abstract void spawnAttack();

    protected virtual void changeAttack(int newAttackIndex) {
        if (newAttackIndex >= 0 && newAttackIndex < attacks.Count) {
            this.attackSelected = newAttackIndex;
            this.currentAttack = this.attacks[this.attackSelected];
        }
    }
}
