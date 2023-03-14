using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [Header("Movement Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    public void Awake()
    {
        initScale = enemy.localScale;
    }

    private void MoveInDirection(int direction)
    {
        enemy.localScale = new Vector3 (Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        if (movingLeft) 
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        movingLeft = !movingLeft;
    }
}
