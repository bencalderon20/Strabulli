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

    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;
    private int attack;
    private float timer=0; //This timer will allow the mech to walk for a bit before attack
    private float timer2=0; //This timer will allow the selection animation to play before the mech attacks

    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private int health = 10000;
    [SerializeField] private GameObject deathEffect;

    //[SerializeField] private AudioSource deathSFX;

    [SerializeField] private GameObject rightEdge;
    [SerializeField] private GameObject leftEdge;

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
        System.Random random = new System.Random(); //This will allow the mech to "randomly" select which attack it will use
    }

    // Update is called once per frame
    void Update()
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
        if(timer>=10)
        {
            /*anim.SetBool("Selecting", true);
            timer2 += Time.deltaTime;
            if(timer2>20)
            {
                attack = random.Next(7);
                switch (attack)
                {
                    case 0:

                }
            }*/
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //Debug.Log("Ded");
            StartCoroutine(Die());
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
       // anim.SetTrigger("death");
        coll.enabled = !coll.enabled;
        yield return new WaitForSeconds(0.5f);
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
