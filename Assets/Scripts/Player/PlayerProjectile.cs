using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerProjectile : MonoBehaviour
{
    public float speed;
    public float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        //if (speed <=0)
        //{
        //    speed = 2.0f;
        //}
        
        if (lifetime <= 0)
        {
            lifetime = 2.0f;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);  // setting velocity just one time when we start the object
        Destroy(gameObject, lifetime); //lifetime is 2 seconds and will destroy itself after which
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
           EnemyTurret turretScript = collision.gameObject.GetComponent<EnemyTurret>();

           Destroy(gameObject); //destroys projectile
        }

    }
}
