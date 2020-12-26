using UnityEngine;
using System.Collections;


// An abstract class for bullets
// will be inherited by real bullet class
public abstract class Attack : MonoBehaviour
{
    public int damage;
    public int speed;

}
