using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject Laser;
    private float dirX = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.C)) {
         Instantiate(Laser);
         ShootLaser();
      }
    }
    void ShootLaser() {
        Laser.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
    }
}
