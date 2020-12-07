using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 3.0f;
    public bool directionLeft = false; // True if spike starts moving to left

    private bool visible = false;
    private AudioManager AudioManager;

    //private GameObject player;
    //private Rigidbody rbPlayer;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));

        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        //player = GameObject.Find("Player");
        //rbPlayer = player.GetComponent<Rigidbody>();
        //playerDirection = rbPlayer.velocity.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameInvisible()
    {
        visible = false;
    }
    void OnBecameVisible()
    {
        visible = true;
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            if(visible) AudioManager.Play("MovingSpike");
            //Debug.Log("STOP!");
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else if (collision.gameObject.tag == "Trace")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    public void changeDirection()
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

    //Vector3 setNormalizedVelocity(float x)
    //{
    //    if (x > 0) return new Vector3(speed, 0.0f, 0.0f);
    //    else if (x < 0) return new Vector3(-speed, 0.0f, 0.0f);
    //    else return new Vector3(0.0f, 0.0f, 0.0f);
    //}
}
