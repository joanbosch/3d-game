using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDoorScript : MonoBehaviour
{
    private SnakeMecanisim sm;
    private AudioManager AudioManager;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
        sm = GameObject.Find("Player").GetComponent<SnakeMecanisim>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider c = GetComponent<Collider>();
        c.isTrigger = sm.SnakeMode;
    }

    private void OnTriggerEnter(Collider other)
    {
        //SnakeMecanisim sm = GameObject.Find("Player").GetComponent<SnakeMecanisim>();
        if (other.gameObject.name == "Player")
        {
            AudioManager.Play("Door");
            sm.resetSnakeMode();
            gameObject.SetActive(false);
        }
    }
}
