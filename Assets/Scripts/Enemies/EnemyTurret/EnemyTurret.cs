using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyTurret : MonoBehaviour
{

    public GameObject target;


    public Transform projectileSpawnPointLeft;
    public Transform projectileSpawnPointRight;
    public PlayerProjectile projectilePrefab;

    public float projectileForce;
    public float aggroDistance;

    public float projectileFireRate;
    float timeSinceLastFire = 0.0f;
    public int health;

    Animator anim;
    SpriteRenderer turretSprite;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        turretSprite = GetComponent<SpriteRenderer>();



        //Garbage data
        if (projectileForce <= 0)
        {
            projectileForce = 7.0f;
        }

        if (health <= 0)
        {
            health = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(GameManager.instance.playerInstance.transform.position, turretSprite.transform.position) <= aggroDistance)
        {
            anim.SetBool("Fire", true);
            //turret follows direction of player
            if (target == true)
            {
                if (!turretSprite.flipX && GameManager.instance.playerInstance.transform.position.x <= transform.position.x || turretSprite.flipX && GameManager.instance.playerInstance.transform.position.x >= transform.position.x)
                {
                    turretSprite.flipX = !turretSprite.flipX;
                }
            }

            //Delay timer between turret attacks
            if (Time.time >= timeSinceLastFire + projectileFireRate)
            {
                anim.SetBool("Fire", true);
                timeSinceLastFire = Time.time;
            }
        }
        else
        {
            anim.SetBool("Fire", false);
        }
    }

    public void Fire()
    {
        //Projectile is fired from here
        if (turretSprite.flipX)
        {

            PlayerProjectile temp = Instantiate(projectilePrefab, projectileSpawnPointLeft.position, projectileSpawnPointLeft.rotation);
            temp.speed = projectileForce * -1;
        }
        else
        {
            PlayerProjectile temp = Instantiate(projectilePrefab, projectileSpawnPointRight.position, projectileSpawnPointRight.rotation);
            temp.speed = projectileForce;
        }
    }

    public void returnToIdle()
    {
        anim.SetBool("Fire", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health--;
            Debug.Log("Turret hit");
           
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
