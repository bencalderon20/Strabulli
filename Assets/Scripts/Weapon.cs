/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject Laser;
    public GameObject Spinach;

    [SerializeField] private AudioSource shootSFX;
    private void Awake()
    {
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {

            Attack();
        }
        if (Input.GetKeyDown(KeyCode.V)) {

            SpinachPunch();
        }
    }

    private void ShootLaser() {
        Instantiate(Laser, FirePoint.position, FirePoint.rotation);
    }
    private void SpinachPunch() {
        Instantiate(Spinach, FirePoint.position, FirePoint.rotation);
    }
    private void Attack()
    {
        shootSFX.Play();
        ShootLaser();
    }
}*/
