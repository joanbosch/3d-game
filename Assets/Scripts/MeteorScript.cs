using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private AudioManager AudioManager;
    private LockedDoorScript script;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
        script = GameObject.Find("LockedDoor").GetComponent<LockedDoorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            AudioManager.Play("Meteor");
            script.addMeteor();
            Destroy(gameObject);
        }
        
    }
}
