using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum CollectibleType
    {
        //Creating fiff types of collectibles that is kept track of (Can potentially be part of the UI)
        POWERUP,
        COLLECTIBLE,
        LIVES,
        KEY
    }

    public CollectibleType currentCollectible;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (currentCollectible)
            {
                case CollectibleType.COLLECTIBLE:
                    Debug.Log("Collectible");
                    collision.GetComponent<PlayerMovement>().score++;
                    Destroy(gameObject);
                    break;

                case CollectibleType.POWERUP:
                    Debug.Log("Powerup");
                    collision.GetComponent<PlayerMovement>().StartJumpforceChange();
                    Destroy(gameObject);
                    break;

                case CollectibleType.LIVES:
                    Debug.Log("Lives");
                    collision.GetComponent<PlayerMovement>().lives++;
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
