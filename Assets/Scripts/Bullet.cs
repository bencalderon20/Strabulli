using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 30;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D (Collider2D HitInfo) {
        //Debug.Log(HitInfo.name);
        enemyPatrol enemy = HitInfo.GetComponent<enemyPatrol>();
        ClownMech mech= HitInfo.GetComponent<ClownMech>();
        Clown_Legs legs = HitInfo.GetComponent<Clown_Legs>();
        if (enemy != null) {
            enemy.TakeDamage(damage);
        }
        if(mech != null)
        {
            mech.TakeDamage(damage);
        }
        if (legs != null)
        {
            legs.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
