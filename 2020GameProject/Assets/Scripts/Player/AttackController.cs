using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to transform input parameters to attacks
public abstract class AttackController : MonoBehaviour
{
    public CharacterController2D character;
    public Animator animator;
    public List<GameObject> attacks = new List<GameObject>();

    public int attackSelected = 0;
    protected GameObject currentAttack;
    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}

    /// <summary>
    /// Function to read input and parameters from the model to execute an attack from list of attacks
    /// </summary>
    private void spawnAttack() {}

    private void changeAttack(int newAttackIndex) {
        if (newAttackIndex >= 0 && newAttackIndex < attacks.Count) {
            this.attackSelected = newAttackIndex;
            this.currentAttack = this.attacks[this.attackSelected];
        }
    }
}
