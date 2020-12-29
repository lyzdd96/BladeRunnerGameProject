using UnityEngine;
using System.Collections;


// Abstract class for the motion controller (animation) of characters
public abstract class MotionController : MonoBehaviour
{
    public Animator animator { get; set; }  // animation attached to the gameobject

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// OnLanding event to be implemented by child class
    /// </summary>
    public abstract void OnLanding();


    /// <summary>
    /// Function to play destroying animation and destroy this character
    /// </summary>
    protected virtual void destroy()
    {
        animator.SetTrigger("IsDying");
        // layer 1 is the explosion layer for monster
        Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(1).length);
        
    }

}
