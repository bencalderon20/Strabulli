using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;
    private SpriteRenderer spr;
    private Material originalMaterial;
    private Coroutine flashRoutine;


    [SerializeField] private float duration;
    [SerializeField] private AudioSource deathSFX;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private  int health = 100;
    public int damage = 1;
    private float direction;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        originalMaterial = spr.material;
    }

    public void TakeDamage(int damage, Transform transform2)
    {
        direction = transform2.position.x - transform.position.x;
        Debug.Log(direction);
        rb.velocity = new Vector2(rb.velocity.x + Mathf.Sign(direction) * -100, 14);
        
        
        health -= damage;
        
        if (health <= 0)
        {
            //Debug.Log("Ded");
            Die();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Traps") || collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(damage, collision.transform);
        }
        if(collision.gameObject.CompareTag("Pit"))
        {
            TakeDamage(health, collision.transform);
        }
    }

    private void Die()
    {
        deathSFX.Play();
        coll.enabled = !coll.enabled;
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
