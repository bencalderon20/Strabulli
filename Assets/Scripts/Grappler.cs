using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    public LineRenderer lineRen;
    public DistanceJoint2D distjoin;
    public Rigidbody2D rb;
    public float force;
    private Vector3 MouseDir; //Get the mouse's direction
    public Transform LinePos; //Position of the line
    public bool connect; //Can connect
    // Start is called before the first frame update
    void Start()
    {
        connect = true;
        distjoin.autoConfigureDistance = true; //Sets position of the mouse
        distjoin.enabled = false;
        lineRen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 place = Input.mousePosition;
        Vector3 p = new Vector3(place.x, place.y, transform.position.y);*/
        //MouseDir = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));//Gets the mouse's position in the screen
        if (connect == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Vector3 mousePos = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
                //lineRen.SetPosition(0, mousePos);
                //distjoin.connectedAnchor = mousePos;
                //distjoin.enabled = true;
                //LinePos.position = mousePos;
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                lineRen.SetPosition(1, transform.position);
                lineRen.enabled = true;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                distjoin.enabled = false;
                lineRen.enabled = false;
            }
            if (distjoin.enabled)
            {
                lineRen.SetPosition(1, transform.position);
            }
            if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Mouse0))
            {

                Vector3 Direction = LinePos.position - transform.position;

                rb.velocity = new Vector2(Direction.x * force, Direction.y * force).normalized * force * Time.deltaTime;
                distjoin.enabled = false;
            }

            if (Input.GetKeyUp(KeyCode.E) && Input.GetKey(KeyCode.Mouse0))
            {
                distjoin.enabled = true;

            }
        }

    }
}
