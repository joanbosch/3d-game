using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    // Rope orentation:
    //      false -> horitzontal rope
    //      true  -> vertical rope
    public bool vertical = false;

    // PlayerMovement Script
    private PlayerMovement pm;
    private Rigidbody rb;
    private Transform t;

    private AudioManager AudioManager;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
        pm =  GameObject.Find("Player").GetComponent<PlayerMovement>();
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        t = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Play("Pipe_In");
        if (!vertical)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            t.position = new Vector3(t.position.x, gameObject.transform.position.y, 0.0f);
            pm.sethRope();
        }
        else {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            t.position = new Vector3(gameObject.transform.position.x, t.position.y, 0.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AudioManager.Play("Pipe_Out");
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        pm.resethRope();
    }
}

