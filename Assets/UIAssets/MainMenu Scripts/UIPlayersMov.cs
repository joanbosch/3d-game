using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayersMov : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 100.0f;
    Quaternion worldRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 randomVec = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        if (randomVec == new Vector3(0, 0, 0)) randomVec = new Vector3(1, 0, 0);
        rb.velocity = randomVec * speed;

        // set random rotation
        transform.rotation = Random.rotation;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        // Reflect direction vector
        Vector3 newDirection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
        newDirection.z = 0;
        rb.velocity = newDirection * speed;
        
    }
}
