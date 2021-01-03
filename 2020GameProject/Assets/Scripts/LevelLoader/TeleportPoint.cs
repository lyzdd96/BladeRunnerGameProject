using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    private Player player;  // the player script instance
    private GameFlowManager gameFlowManager;

    // Start is called before the first frame update
    void Start()
    {
        // get the player gameObject from the game flow manager
        player = GameObject.Find("GameManager").GetComponent<GameFlowManager>().getPlayer().GetComponent<Player>();
        gameFlowManager = GameObject.Find("GameManager").GetComponent<GameFlowManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Detect whether the player is reaching this teleport point
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.isReachingTPpoint = true;
            gameFlowManager.enableTeleportText(true);
        }
        else
        {
            player.isReachingTPpoint = false;
            gameFlowManager.enableTeleportText(false);
        }
    }


}
