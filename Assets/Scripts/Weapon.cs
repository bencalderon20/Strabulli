using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject Laser;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {

         ShootLaser();
      }
    }

    void ShootLaser() {
        Instantiate(Laser, FirePoint.position, FirePoint.rotation);
    }

}
