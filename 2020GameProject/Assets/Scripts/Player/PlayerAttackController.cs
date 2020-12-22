using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to transform the user's controls to player attacks
public class PlayerAttackController : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public GameObject bulletPrefab;

    [Header("Shooting values")]
    public Transform muzzlePoint;  // the muzzle point of weapon
    public float shootingCoolDown;  // the cooldown time between each shoot

    private float coolDownTimer = 0;  // timer for the shooting cooldown
    private float spawnRange = 0.1f;  // the vertical spawan range for bullets (to add some randomness to the bullets spawning position)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer += Time.deltaTime;  // update cooldown

        // when the button is pressed or held
        if (Input.GetButtonDown("Fire") || Input.GetButton("Fire"))
        {
            animator.SetBool("IsShooting", true);

            // if cooldown is terminated, player can shoot
            if (coolDownTimer > shootingCoolDown)
            {
                // add some randomness to the bullets spawning y-position
                Vector3 spawnPos = new Vector3(this.muzzlePoint.position.x, Random.Range(this.muzzlePoint.position.y - spawnRange, this.muzzlePoint.position.y + spawnRange), this.muzzlePoint.transform.position.z);
                GameObject bullet = Instantiate(bulletPrefab, spawnPos, this.transform.rotation);  // generate a bullet

                // set the shooting direction of this bullet depending on the player facing direction
                bullet.GetComponent<Player_Bullet1>().setDirection(this.controller.m_FacingRight ? Vector3.right : Vector3.left);  
                coolDownTimer = 0;
            }
        }
        else if(Input.GetButtonUp("Fire"))
        {
            animator.SetBool("IsShooting", false);
        }


    }
}
