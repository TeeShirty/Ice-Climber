using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class PlayerFire : MonoBehaviour
{
    SpriteRenderer iceClimber;

    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public float projectileSpeed;
    public Projectiles projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        iceClimber = GetComponent<SpriteRenderer>();
        
        if (projectileSpeed <= 0)
        {
            projectileSpeed = 7.0f;
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            Debug.Log("Unity inspector value to be set");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireProjectile();
        }
    }

    void fireProjectile()
    {
        if (iceClimber.flipX)
        {
            Projectiles projectileInstance = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            projectileInstance.speed = projectileSpeed * -1;
        }
        else
        {
            Projectiles projectileInstance = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            projectileInstance.speed = projectileSpeed;
        }
    }
}
