using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 initialDirection = new Vector3(1.0f, 1.0f, 0.0f); // This represents the initial direction of the Player
    private Vector3 lastDirection;
    private Rigidbody rb;
    public float speed = 1.0f;

    //animation
    Animator anim;
    bool die = false;
    int bounceHash = Animator.StringToHash("bounce");
    int deadHash = Animator.StringToHash("playerDead");

    //dead script
    private TimeToReappear deadScript;

    // Snake Script:
    private SnakeMecanisim snakeScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialDirection = initialDirection.normalized * speed;
        resetVelocity();
        anim = GetComponent<Animator>();

        deadScript = GetComponent<TimeToReappear>();
        deadScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Mathf.Abs(rb.velocity.x) != initialDirection.x) || (Mathf.Abs(rb.velocity.y) != initialDirection.y) && !die) 
        {
            float x_mult = 1;
            float y_mult = 1;
            if (rb.velocity.x < 0f) { x_mult = -1.0f; }
            if (rb.velocity.y < 0f) { y_mult = -1.0f; }
            rb.velocity = new Vector3(initialDirection.x * x_mult, initialDirection.y * y_mult, 0.0f);
        }
        if (die) { rb.velocity = new Vector3(0.0f, 0.0f, -2.0f); }

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
        if (collision.gameObject.tag == "Spikes" || collision.gameObject.tag == "Trace")
        {
            die = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            anim.SetTrigger(deadHash);
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            deadScript.enabled = true;
            SnakeMecanisim sm = GameObject.Find("Player").GetComponent<SnakeMecanisim>();
            sm.resetSnakeMode();
        }
        else {
            Bounce(collision.contacts[0].normal);
        }
    }

    private void Bounce(Vector3 normal)
    {
        anim.SetTrigger(bounceHash);
        float vel = lastDirection.magnitude;
        Vector3 newDirection = Vector3.Reflect(lastDirection.normalized, normal);
        rb.velocity = newDirection * speed;
    }

    public void resetVelocity() { rb.velocity = initialDirection; }

    public void resetDie() { die = false; }
}
