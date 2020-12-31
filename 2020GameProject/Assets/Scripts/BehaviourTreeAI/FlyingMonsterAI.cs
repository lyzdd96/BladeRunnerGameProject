﻿using UnityEngine;
using System.Collections;
using CleverCrow.Fluid.BTs.Trees;
using CleverCrow.Fluid.BTs.Tasks;


// Class for the monster AI using behaviour tree
public class FlyingMonsterAI : MonoBehaviour
{
    
    public FlyingMonsterMovementController movementController;
    public FlyingMonsterAttackController attackController;

    public float attackRange = 20f;  // the shooting range for this monster
    public float alertRange = 12f;  // the alerting range for this monster

    private GameObject player;
    

    [SerializeField]
    private BehaviorTree _tree;

    private void Start()
    {
        // get the player gameObject from the game flow manager
        player = GameObject.Find("GameManager").GetComponent<GameFlowManager>().getPlayer();

        // build the behaviour tree
        _tree = new BehaviorTreeBuilder(gameObject)
            .Selector()
                .Sequence()
                    .Condition("isPlayerInAlertRange", () => {
                        // check the distance between this monster and the player
                        return Vector3.Distance(this.transform.position, player.transform.position) <= alertRange;
                    })
                    .Selector()
                        .Condition("isPlayerInAttackRange", () => {
                            // check the distance between this monster and the player
                            return Vector3.Distance(this.transform.position, player.transform.position) <= attackRange;
                        })
                        .Do("PathFinding to Player", () => {
                            // perform path finding to the player
                            movementController.pathFinding(player.transform.position);
                            return TaskStatus.Failure;  // return failure to keep path finding if needed
                        })
                    .End()
                    .WaitTime(1f)  // wait 3 seconds before attack
                    .Do("Attack", () => {
                        attackController.attack(player, 0.25f, 3);
                        return TaskStatus.Success;
                    })
                .End()
                .Do("Wandering", () => {
                    movementController.wander();  // perform wander action
                    return TaskStatus.Success;
                })
            .End()
            .Build();

        
    }

    // Update is called once per frame
    void Update()
    {
        // Update our tree every frame
        _tree.Tick();
    }
}
