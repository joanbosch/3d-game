using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Cameras : MonoBehaviour
{
    private int cameraState = 1; // cameraState = 0 --> Follow the player!
    List<Vector3> cameras;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        cameras = new List<Vector3>();
        cameras.Add(new Vector3(-1.97f, 0.96f, -5.5f)); // FOV: 60
        cameras.Add(new Vector3(2.61f, 1.52f, -6.26f)); // FOV: 60
        cameras.Add(new Vector3(3.71f, -5.3f, -7.67f)); // FOV: 60
        cameras.Add(new Vector3(9.52f, -6.7f, -6.67f)); // FOV: 60
        cameras.Add(new Vector3(14.62f, -5.29f, -7.18f)); //FOV: 60
        cameras.Add(new Vector3(15.07f, 1.48f, -5.95f)); // FOV: 60
        cameras.Add(new Vector3(21.65f, 1.78f, -3.29f)); // FOV: 60
        cameras.Add(new Vector3(2.59f, 4.91f, -2.65f)); // FOV: 60 OK!
        cameras.Add(new Vector3(6.28f, 11.99f, -6.17f)); // FOV: 16
        cameras.Add(new Vector3(13.17f, 12.08f, -5.79f)); // FOV: 16
        Player = GameObject.Find("Player");
        cameraState = 1;
        gameObject.transform.position = cameras[0];
    }

    // Update is called once per frame
    void Update()
    {
        
        int nextCameraState = nextState(Player.transform.position, cameraState);
        if (cameraState != nextCameraState) {
            cameraState = nextCameraState;
            Debug.Log("         Next Camera Pos: " + cameras[(nextCameraState - 1)]);
            gameObject.transform.position = cameras[(nextCameraState-1)];
        }
        //if (Player.transform.position.x > 0)
        //{
        //    InitialCamera.SetActive(false);
        //    Camera2.SetActive(true);
        //}
    }

    private int nextState(Vector3 playerPos, int cameraState)
    {
        float x = playerPos.x;
        float y = playerPos.y;
        if(cameraState == 1)
        {
            if (x >= 0f) return 2;
            else return 1;
        }

        else if(cameraState == 2)
        {
            if (y <= -1.68f) return 3;
            else if (y >= 4.5f) return 8;
            else if (x <= 0f) return 1;
            else if (x >= 5.1f) return 6; // INSIDE PIPE!
            else return 2;
        }

        else if(cameraState == 3)
        {
            if (y >= -1.68f) return 2;
            else if (x >= 5.85f) return 4;
            else return 3;
        }

        else if(cameraState == 4)
        {
            if (x <= 5.85f) return 3;
            else if (x >= 10.2f) return 5;
            else return 4;
        }

        else if(cameraState == 5)
        {
            if (x <= 10.2f) return 4;
            else if (y >= -2.05) return 6;
            else return 5;
        }

        else if(cameraState == 6)
        {
            if (y <= -2.05) return 5;
            else if (x >= 19.8f) return 7;
            else if (x <= 13.3f) return 2;// TODO: CAMERA FOLLOW THE PLAYER TRHOW THE PIPE!
            else if (y >= 4.25f) return 10; // TODO: PIPES!!
            else return 6;
        }

        else if(cameraState == 7)
        {
            if (x <= 19.8f) return 6;
            else return 7;
        }

        else if(cameraState == 8)
        {
            if (y <= 4.5f) return 2;
            else if (y >= 10.6f) return 9;
            else return 8;
        }
        else if( cameraState == 9)
        {
            if ((x <= 3.7) && (y <= 10.6f)) return 8;
            else if (x >= 14.1f) return 10;
            else return 9;
        }
        else if(cameraState == 10)
        {
            if (x >= 14.1f) return 9;
            else if (y >= 9f) return 6; // TODO: PIPES!!
        }

        return 0;
    }
}
