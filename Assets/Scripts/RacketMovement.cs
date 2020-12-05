using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketMovement : MonoBehaviour
{
    private Vector3 initialDirection = new Vector3(0.0f, 1f, 0.0f); // This represents the initial direction of the Player
    private Vector3 direction;
    private Vector3 lastDirection;
    private Rigidbody rb;
    public float speed = 1.0f;
    public float searchSpeed = 1.5f;

    private GameObject player;
    public float racketControlDistance = 1f;
    private float yRcketNoise = 0.2f;
    private bool detected = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialDirection;
        direction = initialDirection;

        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = setNormalizedVelocity(rb.velocity.y);
        //rb.velocity = direction;

        float xPlayer = player.transform.position.x;
        float xRacket = GetComponent<Transform>().position.x;
        float xDistance = Mathf.Abs(xPlayer - xRacket);
        if (xDistance < racketControlDistance) // The racket must trace the Player
        {
            if (!detected)
            {
                lastDirection = rb.velocity;
                detected = true;
            }

            float yPlayer = player.transform.position.y;
            // Debug.Log("yPLAYER = " + yPlayer);
            float yRacket = GetComponent<Transform>().position.y;
            // Debug.Log("yRACKET = " + yRacket);
            if(yPlayer >= (yRacket - yRcketNoise) && (yPlayer <= (yRacket + yRcketNoise))) rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            else if (yPlayer < yRacket)
            {
                // Debug.Log("GO UP!");
                rb.velocity = new Vector3(0.0f, -searchSpeed, 0.0f);
            }
            else if(yPlayer > yRacket)
            {
                // Debug.Log("GO DOWN!");
                rb.velocity = new Vector3(0.0f, searchSpeed, 0.0f);
            }
        }
        else if (detected)
        {
            detected = false;
            rb.velocity = lastDirection;
        }
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (!detected) rb.velocity = setNormalizedVelocity(collision.contacts[0].normal.y);
            else lastDirection = setNormalizedVelocity(collision.contacts[0].normal.y);
        }
        else if (collision.gameObject.tag == "Player") {
            rb.velocity = setNormalizedVelocity(rb.velocity.y);
        } else if (collision.gameObject.tag == "Trace")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        //rb.velocity = direction;
    }

    Vector3 setNormalizedVelocity(float y) 
    {
        if (y > 0) return new Vector3(0.0f, 1.0f, 0.0f);
        else return new Vector3(0.0f, -1.0f, 0.0f);
    }
}
