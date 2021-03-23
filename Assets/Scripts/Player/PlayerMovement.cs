using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer IceClimberSprite;

    //Defining Variables
    public float speed; //will be multiplied by movement vector
    public int jumpForce;
    public bool isGrounded;
    public bool isFiring;

    //Checks
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;

    //Sound
    AudioSource jumpAudioSource;
    public AudioClip jumpSFX;

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
        rb = GetComponent<Rigidbody2D>(); //default function that retreaves the rigidbody's status
        anim = GetComponent<Animator>();
        IceClimberSprite = GetComponent<SpriteRenderer>();

        //Establishing some checks
        if (speed <= 0)
        {
            speed = 5.0f; //all float values need to end with 'f' as all decimal values are doubles
        }

        if (jumpForce <= 0)
        {
            jumpForce = 450;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.0f;
        }

        if (!groundCheck) //if groundCheck does not exist
        {
            Debug.Log("Groundcheck does not exist, set a transform value");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal"); //diff b/w GetAxis and GetAxisRaw is that Raw changes the gravity of the button press to an integer change rather than an float change
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

            if (Input.GetButtonDown("Jump") && isGrounded) //when we press the jump button and wplayer is on ground
            {
                //Debug.Log("Space Pressed");

                rb.velocity = Vector2.zero; //setting the value of velocity of zero. Running and jumping will be different to idle jump
                rb.AddForce(Vector2.up * jumpForce);

                if (!jumpAudioSource)
                {
                    jumpAudioSource = gameObject.AddComponent<AudioSource>();
                    jumpAudioSource.clip = jumpSFX;
                    jumpAudioSource.loop = false;
                    jumpAudioSource.Play();
                }
                else
                {
                    jumpAudioSource.Play();
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                //Debug.Log("Cntl Pressed");
                isFiring = true;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                //Debug.Log("Cntl Released");
                isFiring = false;
            }

            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y); //unit vector +1 and -1. on the y, velocity will stay the same
            anim.SetFloat("Speed", Mathf.Abs(horizontalInput)); //absolute value of to negate negative values
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isFiring", isFiring); //if isFiring is true we attack


            if (!IceClimberSprite.flipX && horizontalInput > 0 || IceClimberSprite.flipX && horizontalInput < 0)
            {
                IceClimberSprite.flipX = !IceClimberSprite.flipX;
            }
        }
    }

    public void StartJumpforceChange()
    {
        StartCoroutine(JumpforceChange());
    }

    IEnumerator JumpforceChange()
    {
        jumpForce = 850;
        yield return new WaitForSeconds(2.0f);
        jumpForce = 450;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Powerup")
        {
            Pickups curPickup = collision.GetComponent<Pickups>();
            if (Input.GetKey(KeyCode.E))
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
