using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#include <stdlib.h>

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;
    private float timer;
    private Animator anim;
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
        if (abs(distance) < 5)
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
    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}