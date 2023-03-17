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

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, walking, jumping, falling }

    public static bool gameIsPaused;
    private bool dirRight = true;

    // Start is called before the first frame update
    private void Start()
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
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if(rb.position.x > 225){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        UpdateAnimationState();
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
            state = MovementState.walking;
            sprite.flipX = false;
            if (dirRight == false) {
                transform.Rotate(0f, 180f, 0f);
                dirRight = true;
            }

            
        }
        else if (dirX < 0f)
        {
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
}
