using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer iceClimber;
    Animator anim;

    public float speed;
    public int jumpforce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    private bool isAttacking;

    int _score = 0; // protects the actual score value that is in the player
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            Debug.Log("Current score is " + _score);
        }
    }

    public int maxLives = 3;
    int _lives = 3;

    public int lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;                          //check both lives gained and lost together
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }
            else if (_lives < 0)
            {
                //game over code
            }

            Debug.Log("Current lives are " + _lives);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        iceClimber = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if(speed <= 0)
        {
            speed = 5.0f;
        }

        if(jumpforce <= 0)
        {
            jumpforce = 450;
        }

        if (groundCheckRadius <=0)
        {
            groundCheckRadius = 0.0f;
        }

        if(!groundCheck)
        {
            Debug.Log("No groundcheck");
        }
    }


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        // to add jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpforce);
        }

        // to turn the player sprite
        if (!iceClimber.flipX && horizontalInput > 0 || iceClimber.flipX && horizontalInput < 0)
        {
            iceClimber.flipX = !iceClimber.flipX;
        }

        // to walk
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isAttacking", isAttacking);

        // to add attack
        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            isAttacking = false;
        }
    }
    public void StartJumpforceChange()
    {
        StartCoroutine(JumpforceChange());
    }

    IEnumerator JumpforceChange()
    {
        int jumpForce = 800;
        yield return new WaitForSeconds(2.0f);
        jumpForce = 300;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Powerup")
        {
            Pickups curPickup = collision.GetComponent<Pickups>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (curPickup.currentCollectible)
                {
                    case Pickups.CollectibleType.KEY:
                        Destroy(collision.gameObject);
                        break;
                }
            }
        }
    }
}
