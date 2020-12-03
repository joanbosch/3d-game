using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Cameras : MonoBehaviour
{
    public GameObject InitialCamera;
    public GameObject Camera2;
    public GameObject Camera3;
    public GameObject Camera4;
    public GameObject Camera5;
    public GameObject Camera6;
    public GameObject Camera7;
    public GameObject Camera8;
    public GameObject Camera9;
    public GameObject Camera10;

    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        InitialCamera.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x > 0)
        {
            InitialCamera.SetActive(false);
            Camera2.SetActive(true);
        }
    }
}
