using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private LockedDoorScript script;
    // Start is called before the first frame update
    void Start()
    {
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
            script.addMeteor();
            Destroy(gameObject);
        }
        
    }
}
