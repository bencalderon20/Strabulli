using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;
    private float timer;
    private Animator anim;

    /*[SerializeField] private int health = 100;
    [SerializeField] private GameObject deathEffect;*/
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (Math.Abs(distance) < 10)
        {
            anim.SetBool("Attack", true);
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                shoot();
                anim.SetBool("Attack", false);
            }
        }
        else
        {
            timer = 0;
            anim.SetBool("Attack", false);
        }
    }
    /*public void TakeDamage(int damage)
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
        anim.SetTrigger("death");
        coll.enabled = !coll.enabled;
        yield return new WaitForSeconds(0.5f);
        //Instantiate(deathEffect, transform.position, Quaternion.identity);

    }*/
       void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}