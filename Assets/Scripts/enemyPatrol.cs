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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        currentPoint = rightEdge.transform;
        //anim.SetBool("moving", true);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {    
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
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == rightEdge.transform)
        {
            flip();
            currentPoint = leftEdge.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == leftEdge.transform)
        {
            flip();
            currentPoint = rightEdge.transform;
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
