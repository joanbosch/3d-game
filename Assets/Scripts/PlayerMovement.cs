using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 initialDirection = new Vector3(1.0f, 1.0f, 0.0f); // This represents the initial direction of the Player
    private Vector3 lastDirection;
    private Rigidbody rb;
    public float speed = 1.0f;

    private AudioManager AudioManager;

    // Is the player in horitzontal Rope?
    private bool hRope = false;

    //animation
    Animator anim;
    bool die = false;
    int bounceHash = Animator.StringToHash("bounce");
    int deadHash = Animator.StringToHash("playerDead");

    //dead script
    private TimeToReappear deadScript;

    // Snake Script:
    private SnakeMecanisim snakeScript;

    //particles
    private ParticleSystem jumpPart;
    private ParticleSystem bloodPart;

    //moving spikes
    private GameObject[] movingSpikes;

    //god mode
    private bool godMode = false;
    private GameObject godModeText;
    private List<Collider> disabledColliders;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
        rb = GetComponent<Rigidbody>();
        initialDirection = initialDirection.normalized * speed;
        resetVelocity();
        anim = GetComponent<Animator>();
        jumpPart = GetSystem("AirJumpParticles");
        bloodPart = GetSystem("BloodParticles");

        deadScript = GetComponent<TimeToReappear>();
        deadScript.enabled = false;

        movingSpikes = GameObject.FindGameObjectsWithTag("MovingSpikes");
        godModeText = GameObject.Find("GodModeText");
        godModeText.SetActive(godMode);
        disabledColliders = new List<Collider>();
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
            if (!hRope)
            {
                if (rb.velocity.y > 0)
                {
                    Bounce(new Vector3(0.0f, -1.0f, 0.0f));
                    var emitParams = new ParticleSystem.EmitParams();
                    emitParams.velocity = -rb.velocity;
                    jumpPart.Emit(emitParams, 3);
                }
                else
                {
                    Bounce(new Vector3(0.0f, 1.0f, 0.0f));
                    var emitParams = new ParticleSystem.EmitParams();
                    emitParams.velocity = -rb.velocity;
                    jumpPart.Emit(emitParams, 3);
                }
            } else
            {
                if (rb.velocity.y > 0)
                {
                    Bounce(new Vector3(-1.0f, 0.0f, 0.0f));
                    var emitParams = new ParticleSystem.EmitParams();
                    emitParams.velocity = -rb.velocity;
                    jumpPart.Emit(emitParams, 3);
                }
                else
                {
                    Bounce(new Vector3(1.0f, 0.0f, 0.0f));
                    var emitParams = new ParticleSystem.EmitParams();
                    emitParams.velocity = -rb.velocity;
                    jumpPart.Emit(emitParams, 3);
                }
            }
        }
        if (Input.GetKeyDown("g"))
        {
            godMode = !godMode;
            godModeText.SetActive(godMode);
            if (!godMode) //if disabled restore colliders
            {
                foreach (Collider c in disabledColliders)
                {
                    Physics.IgnoreCollision(c, GetComponent<Collider>(), false);
                }
                disabledColliders = new List<Collider>();
            }
        }
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spikes" || collision.gameObject.tag == "MovingSpikes" || collision.gameObject.tag == "Trace")
        {
            Debug.Log("Ha colisionsat amb " + collision.gameObject.name);
            if (godMode)
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
                disabledColliders.Add(collision.collider);
            }
            else
            {
                die = true;
                bloodPart.Emit(10);
                rb.constraints = RigidbodyConstraints.FreezeAll;
                anim.SetTrigger(deadHash);
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                deadScript.enabled = true;
                AudioManager.Play("Kill");
                SnakeMecanisim sm = GameObject.Find("Player").GetComponent<SnakeMecanisim>();
                sm.resetSnakeMode();
            }
        }
        else {
            Vector3 normal = collision.contacts[0].normal;
            Bounce(normal);
            AudioManager.Play("GlassStep");
            if (collision.gameObject.tag == "Wall" && ((normal.x < 0 && lastDirection.x > 0) || (normal.x > 0 && lastDirection.x < 0)))
            {
                foreach (GameObject ms in movingSpikes)
                {
                    SpikeMovement spm = ms.GetComponent<SpikeMovement>();
                    spm.changeDirection();
                }
            }

        }
    }

    private void Bounce(Vector3 normal)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("bouncingVer")) anim.SetTrigger(bounceHash);
        float vel = lastDirection.magnitude;
        Vector3 newDirection = Vector3.Reflect(lastDirection.normalized, normal);
        rb.velocity = newDirection * speed;
    }

    public void resetVelocity() { rb.velocity = initialDirection; }

    public void resetDie() { die = false; }

    public void sethRope() { hRope = true; }
    public void resethRope() { hRope = false; }

    // to get specific particle system
    private ParticleSystem GetSystem(string systemName)
    {
        Component[] children = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem childParticleSystem in children)
        {
            if (childParticleSystem.name == systemName)
            {
                return childParticleSystem;
            }
        }
        return null;
    }
}
