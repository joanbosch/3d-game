using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDoorScript : MonoBehaviour
{
    private SnakeMecanisim sm;
    private AudioManager AudioManager;
    // Start is called before the first frame update

    private bool traslate = false;
    private bool firstIt = false;
    private float elapsedTime;
    public float transitionTime = 0.2f; // Time in seconds
    private Vector3 iniDoor, endDoor;

    void Start()
    {
        elapsedTime = 0f;
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
        sm = GameObject.Find("Player").GetComponent<SnakeMecanisim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (traslate)
        {
            moveDoorTo(iniDoor, endDoor);
        }
        else
        {
            Collider c = GetComponent<Collider>();
            c.isTrigger = sm.SnakeMode;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //SnakeMecanisim sm = GameObject.Find("Player").GetComponent<SnakeMecanisim>();
        if (other.gameObject.name == "Player")
        {
            AudioManager.Play("Door");
            sm.resetSnakeMode();
            openDoor();
        }
    }

    private void openDoor()
    {
        traslate = true;
        iniDoor = gameObject.transform.position;
        endDoor = new Vector3(iniDoor.x, iniDoor.y, iniDoor.z + 0.4f);
    }

    private void moveDoorTo(Vector3 from, Vector3 to)
    {
        if (!firstIt) elapsedTime += Time.deltaTime;
        else firstIt = false;

        float t = elapsedTime / transitionTime;
        if (t >= 1f)
        {
            t = 1f;
            traslate = false;
            elapsedTime = 0f;
            firstIt = false;
        }

        gameObject.transform.position = Vector3.Lerp(from, to, t);
    }
}
