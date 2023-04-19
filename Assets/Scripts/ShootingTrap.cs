using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject spikeBall;
    [SerializeField] private float startTimeBetween;
    //[SerializeField] private AudioSource shootSFX;
    private float timebetween;

    // Start is called before the first frame update
    void Start()
    {
        timebetween = startTimeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        if (timebetween <= 0)
        {
            //shootSFX.Play();
            Instantiate(spikeBall, firePoint.position, firePoint.rotation);
            timebetween = startTimeBetween;
        }
        else
        {
            timebetween -= Time.deltaTime;
        }
    }
}
