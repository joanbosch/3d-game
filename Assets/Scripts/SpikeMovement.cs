using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 3.0f;
    public bool directionLeft = false; // True if spike starts moving to left

    private GameObject player;
    private Rigidbody rbPlayer;
    private float playerDirection;
    private float previousPlayerDirection;
    //private bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        player = GameObject.Find("Player");
        rbPlayer = player.GetComponent<Rigidbody>();
        playerDirection = rbPlayer.velocity.x;
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = setNormalizedVelocity(rb.velocity.x);

        previousPlayerDirection = playerDirection;
        playerDirection = rbPlayer.velocity.x;

        if((playerDirection > 0.0f && previousPlayerDirection < 0.0f) || (playerDirection < 0.0f && previousPlayerDirection > 0.0f))
        {
            //Debug.Log("CHANGE DIRECTION!");
            if (directionLeft)
            {
                rb.velocity = new Vector3(-speed, 0.0f, 0.0f);
                directionLeft = false; // Next time spike will move to right
            }
            else
            {
                rb.velocity = new Vector3(speed, 0.0f, 0.0f);
                directionLeft = true; // Next time spike will move to left
            }
        }
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            //Debug.Log("STOP!");
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else if (collision.gameObject.tag == "Trace")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    //Vector3 setNormalizedVelocity(float x)
    //{
    //    if (x > 0) return new Vector3(speed, 0.0f, 0.0f);
    //    else if (x < 0) return new Vector3(-speed, 0.0f, 0.0f);
    //    else return new Vector3(0.0f, 0.0f, 0.0f);
    //}
}
