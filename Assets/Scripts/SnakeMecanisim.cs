using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMecanisim : MonoBehaviour
{
    // The state of the SnakeMode
    public bool SnakeMode = false;
    public float traceSize = 0.1f;
    private bool first = true;
    private bool second = true;

    // Aux Variables
    private Vector3 lastVelocity;
    private Collider collider;

    // Prefabs Created
    List<GameObject> trails;

    // SnakeTail Prefab
    public GameObject trailPrefab;
    private Rigidbody rb;

    // Last Trail End
    Vector3 lastTrailEnd;

    // Current Tail
    Collider lastTrail;
    Collider trail;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        lastVelocity = new Vector3(1.0f, 1.0f, 0.0f);
        trails = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!first) fitColliderBetween(trail, lastTrailEnd, transform.position);
        if (SnakeMode) {
            if (lastVelocity != rb.velocity) {
                newTrail();
            }
        }
        lastVelocity = rb.velocity;
    }

    void newTrail() {
        if (!first)
        {
            if (!second)
            {
                Physics.IgnoreCollision(lastTrail, collider, false);
            }
            else second = false;
        }
        else first = false;

        lastTrail = trail;
        lastTrailEnd = transform.position;

        // Obtain the position of the next trail
        Quaternion q = new Quaternion();
        q.SetLookRotation(rb.velocity);

        // Add a new trail
        GameObject go = Instantiate(trailPrefab, transform.position, q);
        trails.Add(go);
        trail = go.GetComponent<Collider>();
        Physics.IgnoreCollision(trail, collider);
    }

    void fitColliderBetween(Collider c, Vector3 a, Vector3 b) {
        c.transform.position = (new Vector3((a.x + b.x)/2.0f, (a.y +b.y)/2.0f, 0.0f));
        float dist = Vector3.Distance(a, b);
        c.transform.localScale = new Vector3(traceSize, traceSize, dist + traceSize);
    }

    void enableSnakeMode() {
        SnakeMode = true;
        
    }

    public void resetSnakeMode() {
        SnakeMode = false;
        for (int i = 0; i < trails.Count; i++)
        {
            Destroy(trails[i]);
        }
        first = true;
        second = true;
        trails = new List<GameObject>();
    }
}
