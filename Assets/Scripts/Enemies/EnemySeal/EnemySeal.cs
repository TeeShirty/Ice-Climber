using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class EnemySeal : MonoBehaviour
{

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public int health;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //Garbage checks
        if (speed <= 0)
        {
            speed = 2.0f;
        }

        if (health <= 0)
        {
            health = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Death"))
        {

            if (sr.flipX)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier" || collision.gameObject.tag == "Enemy")
        {
            sr.flipX = !sr.flipX;
        }

        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Debug.Log("seal hit");
            Destroy(collision.gameObject);
            isDead();
        }
    }

    public void isDead()
    {
        health--;
        if (health <= 0)
        {
            anim.SetBool("Death", true);
            rb.velocity = Vector2.zero; //so the dead enemy stops moving
            finishedDeath();

        }
    }

    public void finishedDeath()
    {
        Destroy(gameObject, 0.5f);
    }
}
