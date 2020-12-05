using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject spike, placeholder;
    List<GameObject> spikes;
    List<GameObject> placeholders;

    List<Vector3> positions;
    List<bool> skipeEnabled;

    //Button animations
    int press = Animator.StringToHash("press");
    int release = Animator.StringToHash("unpress");
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        initPositions();
        initScene();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<spikes.Count; ++i)
        {
            if (skipeEnabled[i]) spikes[i].active = true;
            else spikes[i].active = false;
        }
        for (int i = 0; i < placeholders.Count; ++i)
        {
            if (!skipeEnabled[i]) placeholders[i].active = true;
            else placeholders[i].active = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switchSpikes();
        }
    }

    void initPositions()
    {
        positions = new List<Vector3>();
        positions.Add(new Vector3(-1.6f, -0.1f, 0.0f));
        positions.Add(new Vector3(-1.1f, -0.1f, 0.0f));
        positions.Add(new Vector3(-0.5f, -0.1f, 0.0f));
        positions.Add(new Vector3(0.1f, 1.0f, 0.0f));
        positions.Add(new Vector3(0.1f, 0.3f, 0.0f));
        positions.Add(new Vector3(0.1f, -0.2f, 0.0f));

        skipeEnabled = new List<bool>();
        skipeEnabled.Add(true);
        skipeEnabled.Add(true);
        skipeEnabled.Add(true);
        skipeEnabled.Add(false);
        skipeEnabled.Add(false);
        skipeEnabled.Add(false);
    }

    void initScene()
    {
        spikes = new List<GameObject>();
        placeholders = new List<GameObject>();

        for (int i = 0; i < positions.Count; i++)
        {
            //Adding spikes
            GameObject go = Instantiate(spike, positions[i], transform.rotation);
            if (skipeEnabled[i]) go.active = false;
            spikes.Add(go);

            //Adding Meteors
            go = Instantiate(placeholder, positions[i], transform.rotation);
            if (!skipeEnabled[1]) go.active = false;
            placeholders.Add(go);
        }
    }

    public void switchSpikes()
    {
        anim.SetTrigger(press);
        for (int i = 0; i < skipeEnabled.Count; ++i) skipeEnabled[i] = !skipeEnabled[i];
    }
}
