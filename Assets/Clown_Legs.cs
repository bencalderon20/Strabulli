using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown_Legs : MonoBehaviour
{
    [SerializeField] private ClownMech mech;
    private Rigidbody2D rb;
    private Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        //mech = objectToCheck.GetComponenetInParent<health>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if(mech!=null)
        {
            mech.TakeDamage(damage);
        }
    }
}
