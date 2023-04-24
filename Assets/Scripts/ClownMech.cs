using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownMech : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private Collider2D coll;
    private SpriteRenderer spr;
    private Material originalMaterial;
    private Coroutine flashRoutine;

    [SerializeField] private GameObject purple;
    [SerializeField] private GameObject white;
    [SerializeField] private GameObject orange;
    [SerializeField] private GameObject stripe;
    [SerializeField] private GameObject sauce;
    private GameObject clown;
    public Transform bulletPos;
    public Transform saucePos;
    private GameObject player;
    private float timer=0; //This timer will allow the mech to walk for a bit before attack
    private float timer2=0; //This timer will allow the selection animation to play before the mech attacks
    private int attack = -1;
    private bool dash = false;
    System.Random random = new System.Random(); //This will allow the mech to "randomly" select which attack it will use

    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private int health = 10;
    //[SerializeField] private GameObject deathEffect;

    //[SerializeField] private AudioSource deathSFX;

    [SerializeField] private GameObject rightEdge;
    [SerializeField] private GameObject leftEdge;
    [SerializeField] private GameObject dashEdge;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        spr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentPoint = rightEdge.transform;
        originalMaterial = spr.material;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dash)
        {

            if (attack < 0)
            {
                if (currentPoint == rightEdge.transform)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                if (Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.5f && currentPoint == rightEdge.transform)
                {
                    currentPoint = leftEdge.transform;
                }
                if (Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.5f && currentPoint == leftEdge.transform)
                {
                    currentPoint = rightEdge.transform;
                }
                timer += Time.deltaTime;
                Debug.Log(timer);
                if (timer >= 5)
                {
                    anim.SetBool("Selecting", true);
                    timer2 += Time.deltaTime;
                    if (timer2 > .005)
                    {
                        anim.SetBool("Selecting", false);
                        attack = random.Next(6);
                        //Debug.Log(attack);
                        timer2 = 0;
                    }
                    timer = 0;
                }
            }
            else
            {
                anim.SetInteger("Attack", attack + 1);
                //Debug.Log("Hello" + timer2);
                timer2 += Time.deltaTime;
                if (timer2 > 3)
                {
                    anim.SetInteger("Attack", 0);
                    switch (attack)
                    {
                        case 0:
                            //anim.SetBool("Shoot", true);
                            if (timer2 > 5)
                            {
                                Instantiate(white, bulletPos.position, Quaternion.identity);
                                //anim.SetBool("Shoot", false);
                                attack = -1;
                                timer2 = 0;
                            }
                            break;
                        case 1:
                            //anim.SetBool("Shoot", true);
                            if (timer2 > 5)
                            {
                                Instantiate(purple, bulletPos.position, Quaternion.identity);
                                //anim.SetBool("Shoot", false);
                                attack = -1;
                                timer2 = 0;
                            }
                            break;
                        case 2:
                            //anim.SetBool("Shoot", true);
                            if (timer2 > 5)
                            {
                                Instantiate(orange, bulletPos.position, Quaternion.identity);
                                //anim.SetBool("Shoot", false);
                                attack = -1;
                                timer2 = 0;
                            }
                            break;
                        case 3:
                            //anim.SetBool("Shoot", true);
                            if (timer2 > 5)
                            {
                                Instantiate(stripe, bulletPos.position, Quaternion.identity);
                                //anim.SetBool("Shoot", false);
                                attack = -1;
                                timer2 = 0;
                            }
                            break;
                        case 4:
                            Instantiate(sauce, saucePos.position, Quaternion.identity);
                            attack = -1;
                            timer2 = 0;
                            break;
                        case 5:
                            dash = true;
                            currentPoint = dashEdge.transform;
                            attack = -1;
                            timer2 = 0;
                            break;
                    }
                }
            }
        }
        else
        {
            if (currentPoint == dashEdge.transform)
            {
                if (timer2 > 5)
                {
                    rb.velocity = new Vector2(-speed * 4, rb.velocity.y);
                }
                else
                    rb.velocity = new Vector2(0, rb.velocity.y);
                timer2 += Time.deltaTime;
            }
            else
            {
                rb.velocity = new Vector2(speed * 1.5f, rb.velocity.y);
            }
            if (Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.6f && currentPoint == dashEdge.transform)
            {
                timer2 = 0;
                currentPoint = leftEdge.transform;
            }
            if (Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.5f && currentPoint == leftEdge.transform)
            {
                dash = false;
                currentPoint = rightEdge.transform;
            }
        }        
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            Debug.Log("Ded");
            StartCoroutine(Die());
            //Die();
        }
        Resources.Load<Material>("FlashMaterial");
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());


    }

    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial.
        spr.material = flashMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        spr.material = originalMaterial;

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }
    IEnumerator Die()
    {
        Debug.Log("Super Ded");
        //deathSFX.Play();
        anim.SetTrigger("death");
        coll.enabled = !coll.enabled;
        yield return new WaitForSeconds(2);
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
