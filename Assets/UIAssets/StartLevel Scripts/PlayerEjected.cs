using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEjected : MonoBehaviour
{
    public float movSpeed = 250.0f;

    private Rigidbody rb;
    private Vector3 eulerAnglVel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = new Vector3(movSpeed, 0.0f, 0.0f);
        eulerAnglVel = new Vector3(0, 0, 50);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion deltaRotation = Quaternion.Euler(eulerAnglVel * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        if (transform.position.x > 1400)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
