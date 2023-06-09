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
    public float timer3 = Mathf.Infinity;
    public float timer4 = 0;
    private int brick = 0;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private AudioSource jumpSFX;
    //[SerializeField] private AudioSource moveSFX;

    private enum MovementState { idle, walking, jumping, falling, attack }

    public static bool gameIsPaused;
    private bool dirRight = true;
    private bool dashing = false;
    private float dashX = 0f;
    private bool hit = false;

    public Transform FirePoint;
    public GameObject Laser;
    public GameObject Spinach;
    public GameObject Brick;
    private bool BrickGround;
    private int item;
    private int attack;
    [SerializeField] private AudioSource shootSFX;

    private GameObject[] BrickList;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        BrickList = new GameObject[5];
        attack = anim.GetInteger("power");
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        if(timer3 > 0.3f) {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
        BrickGround = Brick.GetComponent<Block>().IsGrounded();
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSFX.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        //if(rb.position.x > 225){
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //    }

        UpdateAnimationState();
        //Various timers for the attacks
        if (timer > 10) //This will reset the player's animation to idle if they haven't attack
        {
            anim.SetInteger("power", 0);
        }
        if (attack == 3) //This will run the cooldown for the spinach
        {
            timer2 += Time.deltaTime;
        }
        if (timer2 > 1 && dashing) //This will prevent the player from dashing repeatly
        {
            dashing = false;
            timer2 = 0;
        }
        if (brick >= 1) //This will run a cooldown time if there's more than one brick have launched
        {
            timer4 += Time.deltaTime;
        }
        timer += Time.deltaTime;
        timer3 += Time.deltaTime;
    }

    private void ShootLaser()
    {
        Instantiate(Laser, FirePoint.position, FirePoint.rotation);
    }
    private void SpinachPunch()
    {
        timer2 = 0;
        dashX = dirX;
        dashing = true;
        anim.SetInteger("power", 3);
        //rb.velocity = new Vector2(dirX * dashSpeed, 0);
        if (timer2 < 0.3 && dashing == true)
        {
            rb.velocity = new Vector2(dashX * dashSpeed, 0);
            Instantiate(Spinach, FirePoint.position, FirePoint.rotation);
        }

    }
    private void ShootBrick()
    {
        GameObject tempBrick = Instantiate(Brick, FirePoint.position, FirePoint.rotation);
        Destroy(BrickList[brick%5]);
        BrickList[brick%5] = tempBrick;
    }
    private void Attack()
    {
        timer = 0;
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetInteger("power", 1);
            shootSFX.Play();
            ShootLaser();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            anim.SetInteger("power", 2);
            if (!BrickGround&&brick<1)
            {
                ShootBrick();
                brick++;
            }
                if(timer4>1)
                {
                    brick = 0;
                    timer4 = 0;
                }
        }
        else if(Input.GetKeyDown(KeyCode.V) && IsGrounded() && timer2 > 1.2)
        {
            SpinachPunch();
        }
        attack = anim.GetInteger("power");
    }

    //pauses game
    public void PauseGame()
    {
        if (Time.timeScale == 1)
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
            if (dirRight == false)
            {
                transform.Rotate(0f, 180f, 0f);
                dirRight = true;
            }


        }
        else if (dirX < 0f)
        {
            //moveSFX.Play();
            state = MovementState.walking;
            //sprite.flipX = true;
            if (dirRight == true)
            {
                transform.Rotate(0f, 180f, 0f);
                dirRight = false;
            }

        }
        else
        {
            state = MovementState.idle;
        }
        if (canAttack())
        {
            state = MovementState.attack;
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //gameIsPaused = !gameIsPaused;
            PauseGame();
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
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
        return Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.B) ||Input.GetKeyDown(KeyCode.V);
    }

}