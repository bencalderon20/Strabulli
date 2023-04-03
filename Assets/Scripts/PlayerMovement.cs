using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private float timer = Mathf.Infinity;
    private float timer2 = Mathf.Infinity;


    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private AudioSource jumpSFX;
    //[SerializeField] private AudioSource moveSFX;

    private enum MovementState { idle, walking, jumping, falling }

    public static bool gameIsPaused;
    private bool dirRight = true;

    public Transform FirePoint;
    public GameObject Laser;
    public GameObject Spinach;
    [SerializeField] private AudioSource shootSFX;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSFX.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        //if(rb.position.x > 225){
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //    }

        UpdateAnimationState();
        if (Input.GetKeyDown(KeyCode.C) && canAttack())
        {
            anim.SetBool("attack", true);
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {

            SpinachPunch();
        }
        if(timer>10) {
            anim.SetBool("attack", false);
        }
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
    }

    private void ShootLaser()
    {
        Instantiate(Laser, FirePoint.position, FirePoint.rotation);
    }
    private void SpinachPunch()
    {
        timer2 = 0;
        rb.velocity = new Vector2(dirX * dashSpeed, 0);
        Instantiate(Spinach, FirePoint.position, FirePoint.rotation);
    }
    private void Attack()
    {
        timer = 0;
        shootSFX.Play();
        ShootLaser();
    }

    //pauses game
    public void PauseGame ()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
        else 
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        // Movement Animation Changing
        if (dirX > 0f)
        {
            //moveSFX.Play();
            state = MovementState.walking;
            sprite.flipX = false;
            if (dirRight == false) {
                transform.Rotate(0f, 180f, 0f);
                dirRight = true;
            }

            
        }
        else if (dirX < 0f)
        {
            //moveSFX.Play();
            state = MovementState.walking;
            //sprite.flipX = true;
            if (dirRight == true) {
                transform.Rotate(0f, 180f, 0f);
                dirRight = false;
            }

        }
        else
        {
            state = MovementState.idle;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //gameIsPaused = !gameIsPaused;
            PauseGame();
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        } else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    public bool canAttack()
    {
        return Input.GetKeyDown(KeyCode.C);
    }
    
}
