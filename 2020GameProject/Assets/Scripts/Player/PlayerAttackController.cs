using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to transform the user's controls to player attacks
public class PlayerAttackController : MonoBehaviour
{
    public Player player;
    public Animator animator;
    public GameObject bulletPrefab;

    [Header("Shooting values")]
    public Transform muzzlePoint;  // the muzzle point of weapon
    public float shootingCoolDown;  // the cooldown time between each shoot
    public float skill1CoolDown;  // the cooldown time of skill1

    private float fireCoolDownTimer = 0;  // timer for the shooting cooldown
    private float spawnRange = 0.1f;  // the vertical spawan range for bullets (to add some randomness to the bullets spawning position)

    // skill1
    private float skill1CoolDownTimer = 0;  // timer for the skill1 cooldown
    private int numBullets_skill1 = 12;  // number of bullets shooting by skill1


    // Start is called before the first frame update
    void Start()
    {
        fireCoolDownTimer = shootingCoolDown;
        skill1CoolDownTimer = skill1CoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        fire();  // handle the fire action
        skill1();  // handle the skill1 action
    }


    /// <summary>
    /// Function to handle the normal fire action of player
    /// </summary>
    private void fire()
    {
        fireCoolDownTimer += Time.deltaTime;  // update cooldown

        // when the button is pressed or held
        if (Input.GetButtonDown("Fire") || Input.GetButton("Fire"))
        {
            animator.SetBool("IsShooting", true);

            // if cooldown is terminated, player can shoot
            if (fireCoolDownTimer > shootingCoolDown)
            {
                // add some randomness to the bullets spawning y-position
                Vector3 spawnPos = new Vector3(this.muzzlePoint.position.x, Random.Range(this.muzzlePoint.position.y - spawnRange, this.muzzlePoint.position.y + spawnRange), this.muzzlePoint.transform.position.z);
                GameObject bullet = Instantiate(bulletPrefab, spawnPos, this.transform.rotation);  // generate a bullet

                // set the shooting direction of this bullet depending on the player facing direction
                bullet.GetComponent<Player_Bullet1>().setDirection(this.player.isFacingRight ? Vector3.right : Vector3.left);
                fireCoolDownTimer = 0;
            }
        }
        else if (Input.GetButtonUp("Fire"))
        {
            animator.SetBool("IsShooting", false);
        }
    }


    /// <summary>
    /// Function to handle the skill1 of player
    /// </summary>
    private void skill1()
    {
        skill1CoolDownTimer += Time.deltaTime;  // update cooldown

        // when the skill1 button is pressed and the player is not on the ground
        if (Input.GetButtonDown("Skill1") && !player.isGrounded)
        {
            // if cooldown is terminated, player can shoot
            if (skill1CoolDownTimer > skill1CoolDown)
            {
                float deltaAngle = 360 / numBullets_skill1;
                // generate 12 bullets flying from the player (one bullet for each 30 degree around the player)
                for (int i = 0; i < numBullets_skill1; i++)
                {
                    GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, deltaAngle * i));  // generate a bullet

                    // set the shooting direction of this bullet
                    bullet.GetComponent<Player_Bullet1>().setDirection(new Vector2(Mathf.Cos(Mathf.Deg2Rad*deltaAngle * i), Mathf.Sin(Mathf.Deg2Rad * deltaAngle * i)));
                }


                skill1CoolDownTimer = 0;
            }
        }
    }
}
