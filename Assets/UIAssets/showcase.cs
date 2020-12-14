using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showcase : MonoBehaviour
{
    public List<GameObject> prefabs;
    public Vector3 position;
    private float currentTime;
    private int currentInd;
    private List<GameObject> objs;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        currentInd = 0;
        initScene();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 1)
        {
            objs[currentInd%4].active = false;
            if (currentInd < 4) currentInd++;
            else currentInd = 0;
            objs[currentInd].active = true;
            currentTime = 0;
        }
    }

    void initScene()
    {
        objs = new List<GameObject>();
        for (int i = 0; i < prefabs.Count; i++)
        {
            GameObject go = Instantiate(prefabs[i], position, transform.rotation);
            go.transform.parent = gameObject.transform;
            go.transform.localScale = new Vector3(20, 20, 20);
            if (i == 0) go.active = true;
            else go.active = false;
            objs.Add(go);
        }
    }
}
