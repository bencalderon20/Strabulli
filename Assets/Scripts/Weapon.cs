using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject Laser;
    public GameObject Spinach;

    [SerializeField] private AudioSource shootSFX;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {

            shootSFX.Play();
            ShootLaser();
        }
        if (Input.GetKeyDown(KeyCode.V)) {

            SpinachPunch();
        }
    }

    void ShootLaser() {
        Instantiate(Laser, FirePoint.position, FirePoint.rotation);
    }
    void SpinachPunch() {
        Instantiate(Spinach, FirePoint.position, FirePoint.rotation);
    }
}
