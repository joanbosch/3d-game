using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 initialDirection = new Vector3(1.0f, 1.0f, 0.0f); // This represents the initial direction of the Player
    private Vector3 lastDirection;
    private Rigidbody rb;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.velocity = initialDirection;
    }

    // Update is called once per frame
    void Update()
    {
        lastDirection = rb.velocity;

        if (Input.GetKeyDown("space"))
        {
            if (rb.velocity.y > 0) Bounce(new Vector3(0.0f, -1.0f, 0.0f));
            else Bounce(new Vector3(0.0f, 1.0f, 0.0f));
        }
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector3 normal)
    {
        float vel = lastDirection.magnitude;
        Vector3 newDirection = Vector3.Reflect(lastDirection.normalized, normal);
        rb.velocity = newDirection.normalized * speed;
    }
}
