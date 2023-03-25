using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    [SerializeField] private GameObject rightEdge;
    [SerializeField] private GameObject leftEdge;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private BoxCollider2D coll;

    [SerializeField] private float speed;

    [SerializeField] private  int health = 100;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float jump = 7f;
    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        currentPoint = rightEdge.transform;
        anim.SetBool("moving", true);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //Debug.Log("Ded");
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {    
        //Debug.Log("Super Ded");
        anim.SetTrigger("death");
        coll.enabled = !coll.enabled;
        yield return new WaitForSeconds(0.5f);
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;

        if(currentPoint == rightEdge.transform)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        if(Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.5f && currentPoint == rightEdge.transform)
        {
            flip();
            currentPoint = leftEdge.transform;
        }
        if (Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.5f && currentPoint == leftEdge.transform)
        {
            flip();
            currentPoint = rightEdge.transform;
        }

        if (IsGrounded() && jump > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
