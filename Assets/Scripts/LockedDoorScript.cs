using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorScript : MonoBehaviour
{

    public GameObject meteor;
    // Prefabs Created
    List<GameObject> meteors;

    int num_meteors;

    // Start is called before the first frame update
    void Start()
    {
        num_meteors = 0;
        meteors = new List<GameObject>();
        for (int i = 0; i < 3; i++) {
            GameObject go = Instantiate(meteor, new Vector3((transform.position.x - 0.02f), (float)(transform.position.y + 0.05 + 0.355f*i)), transform.rotation);
            go.active = false;
            StaticFragments(ref go, true);
            meteors.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StaticFragments(ref GameObject go, bool b) {
        for(int i = 0; i< 6; ++i) { 
            Rigidbody rb = go.transform.GetChild(i).gameObject.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(GameObject.FindWithTag("Player").GetComponent<Collider>(), go.transform.GetChild(i).gameObject.GetComponent<MeshCollider>());
            if (b) rb.constraints = RigidbodyConstraints.FreezePosition;
            else rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void addMeteor() {
        meteors[num_meteors].active = true;
        if (num_meteors == 2) {
            Collider c = GetComponent<Collider>();
            c.isTrigger = true;
        }
        if (num_meteors < 3) ++num_meteors;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger!");
        if (other.gameObject.name == "Player")
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject go = meteors[i];
                StaticFragments(ref go, false);
            }
            Destroy(gameObject);
        }
    }
}
