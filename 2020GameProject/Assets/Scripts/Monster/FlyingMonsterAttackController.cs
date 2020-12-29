using UnityEngine;
using System.Collections;

public class FlyingMonsterAttackController : AttackController
{
    [Header("Shooting values")]
    public Transform muzzlePoint;  // the muzzle point of weapon

    public Character character;
    private GameObject player;
    private float spawnRange = 0.1f;  // the vertical spawan range for bullets (to add some randomness to the bullets spawning position)

    private IEnumerator coroutine; // the attack coroutine

    // Use this for initialization
    protected override void Start()
    {
        this.currentAttack = this.attacks[this.attackSelected];

        // get the player gameObject from the game flow manager
        player = GameObject.Find("GameManager").GetComponent<GameFlowManager>().getPlayer();
    }

    // Update is called once per frame
    protected override void Update()
    {
        fire(player);
    }

    /// <summary>
    /// Function to read input and parameters from the model to execute an attack from list of attacks
    /// </summary>
    protected override void spawnAttack()
    {

    }

    /*
    public void attack(GameObject target, float cooldown)
    {
        // start fire co-routine
        this.coroutine = Fire(target, cooldown);
        StartCoroutine(coroutine);
    }*/

    /// <summary>
    /// Function to handle the fire action of monster toward the target gameObject
    /// </summary>
    private void fire(GameObject target)
    {
        // add some randomness to the bullets spawning y-position
        Vector3 spawnPos = new Vector3(this.muzzlePoint.position.x, Random.Range(this.muzzlePoint.position.y - spawnRange, this.muzzlePoint.position.y + spawnRange), this.muzzlePoint.transform.position.z);
        Attack bullet = Instantiate(this.currentAttack, spawnPos, this.transform.rotation);  // generate a bullet

        // set the shooting direction of this bullet depending on the player position
        Vector3 direction = target.transform.position - this.transform.position;
        bullet.GetComponent<Attack>().setDirection(direction);

        
    }
}
