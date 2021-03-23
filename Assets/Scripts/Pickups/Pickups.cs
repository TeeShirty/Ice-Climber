using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{

    //creating an enumerated value namespace
    public enum CollectibleType
    {
        //Creating fiff types of collectibles that is kept track of (Can potentially be part of the UI)
        POWERUP,
        COLLECTIBLE,
        LIVES,
        KEY
    }

    public CollectibleType currentCollectible;
    BoxCollider2D trigger;
    AudioSource pickupSound;
    public AudioClip pickupSFX;


    // Start is called before the first frame update
    void Start()
    {
        pickupSound = GetComponent<AudioSource>();
        trigger = GetComponent<BoxCollider2D>();
        if(pickupSound)
        {
            pickupSound.clip = pickupSFX;
            pickupSound.loop = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!pickupSound.isPlaying && !trigger.enabled)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {  
            Debug.Log("Triggered");
            switch (currentCollectible)
            {
                case CollectibleType.COLLECTIBLE:
                    Debug.Log("Collectible");
                    //collision.GetComponent<PlayerMovement>().score++;
                    pickupSound.Play();
                    trigger.enabled = false; //disabling the collision box so that the player passes through it only once.
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