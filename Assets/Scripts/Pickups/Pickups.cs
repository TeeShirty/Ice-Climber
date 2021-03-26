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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pickupSound)
        {
            pickupSound.clip = pickupSFX;
            pickupSound.loop = false;
            //pickupSound.Play();
            //trigger.enabled = false;
        }
        if (!pickupSound.isPlaying && !trigger.enabled)
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
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5000);
                    pickupSound.Play();
                    trigger.enabled = false; //disabling the collision box so that the player passes through it only once.
                    //Destroy(gameObject);
                    break;

                case CollectibleType.POWERUP:
                    Debug.Log("Powerup");
                    collision.GetComponent<PlayerMovement>().StartJumpforceChange();
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5000);
                    pickupSound.Play();
                    trigger.enabled = false; //disabling the collision box so that the player passes through it only once.
                    //Destroy(gameObject);
                    break;

                case CollectibleType.LIVES:
                    Debug.Log("Lives");
                    collision.GetComponent<PlayerMovement>().lives++;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5000);
                    pickupSound.Play();
                    trigger.enabled = false; //disabling the collision box so that the player passes through it only once.
                    //Destroy(gameObject);
                    break;
            }
        }

    }
}
