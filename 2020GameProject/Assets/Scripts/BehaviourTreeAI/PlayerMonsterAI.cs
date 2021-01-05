using UnityEngine;
using System.Collections;
using CleverCrow.Fluid.BTs.Trees;
using CleverCrow.Fluid.BTs.Tasks;


// Class for the monster AI using behaviour tree
public class PlayerMonsterAI : MonoBehaviour
{
    
    public PlayerMonsterMovementController movementController;
    public PlayerMonsterAttackController attackController;

    public float attackRange = 5f;  // the shooting range for this monster
    public float alertRange = 12f;  // the alerting range for this monster

    float jumpCooldown = 2f;
    float jumpCooldownTimer = 0f;

    float shootingCooldown = 1f;
    float shootingCooldownTimer = 0f;

    private Player player;
    

    [SerializeField]
    private BehaviorTree _tree;

    private void Start()
    {
        // get the player gameObject from the game flow manager
        player = GameObject.Find("GameManager").GetComponent<GameFlowManager>().getPlayer();
        jumpCooldownTimer = jumpCooldown;
        // build the behaviour tree
        _tree = new BehaviorTreeBuilder(gameObject)
            .Selector()
                .Sequence()
                    .Condition("isBulletIncoming", () => {
                        bool cooldownOK = jumpCooldownTimer > jumpCooldown;
                        // no need to raycast if cooldown
                        if (!cooldownOK) return false;
                        RaycastHit2D raycasthit = Physics2D.CircleCast(this.transform.position, 4f, Vector2.left);
                        // player attack or player itself enters circle cast
                        
                        return raycasthit.collider.gameObject.tag.Contains("Player");
                    })
                    .Do("Jump", () => {
                        Debug.Log("AI jump");
                        jumpCooldownTimer = 0f;
                        movementController.jump();
                        return TaskStatus.Success;
                    })
                .End()
                .Sequence()
                    .Condition("isPlayerInAttackRange", () => {
                        bool cooldownOK = shootingCooldownTimer > shootingCooldown;
                        // check the distance between this monster and the player
                        return Vector3.Distance(this.transform.position, player.transform.position) <= attackRange && cooldownOK;
                    })
                    // .WaitTime(1f)  // wait 1 second before each attack action
                    .Do("Attack", () => {
                        shootingCooldownTimer = 0f;
                        movementController.pathFinding(player.transform.position - this.transform.position);
                        attackController.attack(player, 2f, 1);
                        return TaskStatus.Success;
                    })
                .End()
                .Sequence()
                    .Condition("isPlayerInAlertRange", () => {
                        // check the distance between this monster and the player
                        return Vector3.Distance(this.transform.position, player.transform.position) <= alertRange;
                    })
                    .Do("PathFinding to Player", () => {
                        // perform path finding to the player
                        movementController.pathFinding(player.transform.position - this.transform.position);
                        return TaskStatus.Success;  // return failure to keep path finding if needed
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
        //Debug.Log(Vector3.Distance(player.transform.position, this.transform.position));
        jumpCooldownTimer += Time.deltaTime;
        shootingCooldownTimer += Time.deltaTime;
    }
}
