using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

public class PlayerCollision : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerMovement pm;

    public float bounceForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();

        if (bounceForce <= 0)
        {
            bounceForce = 100.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
   

        if (collision.gameObject.tag == "TurretProjectile")
        {
            GameManager.instance.lives--;
            //if lives are > 0 then respawn player nad level
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.lives--;
            Destroy(collision.gameObject);
            //if lives are > 0 then respawn player nad level
        }
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
