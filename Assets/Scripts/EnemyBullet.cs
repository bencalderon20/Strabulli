using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private Collider2D coll;
    public bool stay = false;
    public bool rotate = false;
    public int action = 0;
    public float force;
    public float wait;
    private float timer=0;
    private Vector2 oldsped;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        oldsped = rb.velocity;
        if (rotate == true)
        {
            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(stay&&timer>=wait)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerLife>().TakeDamage(20, coll.transform);
            Destroy(gameObject);
        }
        else if (!stay && other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            switch (action)
            {
                case 0:
                    break;
                case 1:
                    Destroy(gameObject);
                    break;

            }
        }
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if(stay && other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Vector3 Direction = Vector3.Reflect(oldsped.normalized, collision.contacts[0].normal);
            rb.velocity = Direction * Mathf.Max(oldsped.magnitude, 0);
            Debug.Log()
        }
    }*/
    void OnBecomeInvisible()
    {
        if(!stay)
            Destroy(gameObject);
    }
}
