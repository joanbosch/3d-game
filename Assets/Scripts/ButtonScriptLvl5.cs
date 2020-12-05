using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScriptLvl5 : MonoBehaviour
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
        // Positions must be mesured using Spikes.
        positions.Add(new Vector3(-3.6f, 1.8f, 0.0f));
        positions.Add(new Vector3(-3.6f, 0.608f, 0.0f));
        positions.Add(new Vector3(-3.6f, 1.212f, 0.0f));

        positions.Add(new Vector3(2.67f, 4.128f, 0.0f));
        positions.Add(new Vector3(3.129f, 4.128f, 0.0f));

        positions.Add(new Vector3(2.2922f, 0.291f, 0.0f));
        positions.Add(new Vector3(2.2922f, 0.816f, 0.0f));
        positions.Add(new Vector3(2.2922f, -0.232f, 0.0f));

        positions.Add(new Vector3(9.39f, 0.536f, 0.0f));
        positions.Add(new Vector3(9.36f, 0.03f, 0.0f));

        positions.Add(new Vector3(3.406f, -0.96f, 0.0f));
        positions.Add(new Vector3(3.905f, -0.96f, 0.0f));
        positions.Add(new Vector3(4.486f, -0.96f, 0.0f));

        positions.Add(new Vector3(3.763f, 1.209f, 0.0f));
        positions.Add(new Vector3(4.375f, 1.209f, 0.0f));


        skipeEnabled = new List<bool>();

        skipeEnabled.Add(true);
        skipeEnabled.Add(true);
        skipeEnabled.Add(true);

        skipeEnabled.Add(true);
        skipeEnabled.Add(true);

        skipeEnabled.Add(false);
        skipeEnabled.Add(false);
        skipeEnabled.Add(false);

        skipeEnabled.Add(true);
        skipeEnabled.Add(true);

        skipeEnabled.Add(false);
        skipeEnabled.Add(false);
        skipeEnabled.Add(false);

        skipeEnabled.Add(true);
        skipeEnabled.Add(true);

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

            //Adding Placeholders
            go = Instantiate(placeholder, new Vector3(positions[i].x-0.2f, positions[i].y, positions[i].z), transform.rotation);
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
