using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToReappear : MonoBehaviour
{
    public float timeToReappear = 4.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToReappear -= Time.deltaTime;
        if (timeToReappear <= 0.0f)
        {
            rb.MovePosition(new Vector3(0.0f, 0.0f, 0.0f)); // TODO: change to savepoint
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            rb.rotation = Quaternion.identity;
            // 1 second to start moving
            if (timeToReappear <= -1.0f)
            {
                rb.velocity = new Vector3(1.0f, 1.0f, 0.0f);
                timeToReappear = 4.0f;
                this.enabled = false;
            }
        }
    }
}
