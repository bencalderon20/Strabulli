using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float rightX;
    [SerializeField] private float leftX;
    [SerializeField] private float topY;
    [SerializeField] private float bottomY;

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(player.position.x, leftX, rightX), 
                                            Mathf.Clamp(player.position.y, bottomY, topY), transform.position.z);
    }
}
