using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 30;
    public BoxCollider2D coll;
    public Rigidbody2D rb;
    private float timer = 0;

    //This field is needed to send a message back to the playerin order to allow the brick to drop a second time
    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    /*void OnTriggerEnter2D (Collider2D HitInfo) {
        //Debug.Log(HitInfo.name);
        enemyPatrol enemy = HitInfo.GetComponent<enemyPatrol>();
        if (enemy != null) {
            enemy.TakeDamage(damage);
            //Destroy(gameObject);
        }
    }*/

    IEnumerator OnBecameInvisible()
    {
        yield return new WaitForSeconds(5);
        //Destroy(gameObject);
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
