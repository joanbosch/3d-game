using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Cameras : CamerasScript
{
    private int cameraState = 1; // 
    List<Vector3> cameras;
    private GameObject Player;

    // Smooth moving camera variables
    private bool firstIt;
    private bool movingCamera;
    private float elapsedTime;
    public float transitionTime = 1f; // Time in seconds
    private int lastCameraState = 1;
    private Vector3 lastCameraPos;


    // Smooth transition of the z component inside pipes!
    private bool firstItPipes;
    private bool movingCameraPipes;
    private float elapsedTimePipes;
    public float transitionTimePipes = 0.6f; // Time in seconds

    // Smooth transition to move the camera to the origin of the game.
    private bool cameraToOrigin;

    // ReEnable the Snake Mode
    private EnableSnakeMode sm;

    // Sounds!
    private AudioManager AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
        AudioManager.Play("Storage");
        setUpMovingCamera();
        setUpMovingPipesCamera();
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

        // Move camera to origin variables
        cameraToOrigin = false;

        // If the camera is reset, reset the snake mode.
        sm = GameObject.Find("Snake5").GetComponent<EnableSnakeMode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            this.returnMainMenu();
        }
        if (movingCamera)
        {
            moveCameraTo(lastCameraPos, cameras[cameraState - 1]);
        } else if (cameraToOrigin)
        {
            // The origin game must be in the first position of the cameras array!

            moveCameraTo(lastCameraPos, cameras[0]);
        }
        else
        {
            int nextCameraState = nextState(Player.transform.position, cameraState);
            if (cameraState == 11 || cameraState == 12 || cameraState == 8) followPlayer(lastCameraPos, new Vector3(Player.transform.position.x, Player.transform.position.y, -4f));
            if (cameraState != nextCameraState)
            {
                if ((nextCameraState == 11) || (nextCameraState == 12) || (nextCameraState == 8)) { setUpMovingPipesCamera(); }
                else movingCamera = true;
                lastCameraState = cameraState;
                lastCameraPos = gameObject.transform.position;
                cameraState = nextCameraState;

            }
        }
        
    }

    private void setUpMovingPipesCamera()
    {
        firstItPipes = true;
        movingCameraPipes = false;
        elapsedTimePipes = 0f;
    }

    private void followPlayer(Vector3 from, Vector3 to)
    {
        if (!firstItPipes) elapsedTimePipes += Time.deltaTime;
        else firstItPipes = false;

        float t = elapsedTimePipes / transitionTimePipes;
        if (t > 1.0f) t = 1.0f;
            
        gameObject.transform.position = Vector3.Lerp(from, to, t);

    }

    private void setUpMovingCamera()
    {
        movingCamera = false;
        cameraToOrigin = false;
        firstIt = true;
        elapsedTime = 0f;
    }

    private void moveCameraTo(Vector3 from, Vector3 to)
    {
        if (!firstIt) elapsedTime += Time.deltaTime;
        else firstIt = false;

        float t = elapsedTime / transitionTime;
        if (t >= 1f)
        {
            t = 1f;
            setUpMovingCamera();
        }

        gameObject.transform.position = Vector3.Lerp(from, to, t);
    }

    public override void moveCameraToOrigin() {
        cameraToOrigin = true;
        cameraState = 1;
        sm.reActive();
    }

    private int nextState(Vector3 playerPos, int cameraState)
    {
        float x = playerPos.x;
        float y = playerPos.y;
        if (cameraState == 1)
        {
            if (x >= 0f) return 2;
            else return 1;
        }

        else if (cameraState == 2)
        {
            if (y <= -1.68f) return 3;
            else if (y >= 4.5f) return 8;
            else if (x <= 0f) return 1;
            else if (x >= 5.1f) return 11;
            else return 2;
        }

        else if (cameraState == 3)
        {
            if (y >= -1.68f) return 2;
            else if (x >= 5.85f) return 4;
            else return 3;
        }

        else if (cameraState == 4)
        {
            if (x <= 5.85f) return 3;
            else if (x >= 13.2f) return 5;
            else return 4;
        }

        else if (cameraState == 5)
        {
            if (x <= 13.2f) return 4;
            else if (y >= -2.05) return 6;
            else return 5;
        }

        else if (cameraState == 6)
        {
            if (y <= -2.05) return 5;
            else if (x >= 19.8f) return 7;
            else if (x <= 13.3f) return 11;
            else if (y >= 4.25f) return 12;
            else return 6;
        }

        else if (cameraState == 7)
        {
            if (x <= 19.8f) return 6;
            else return 7;
        }

        else if (cameraState == 8)
        {
            if (y <= 4.5f) return 2;
            else if (y >= 10.6f) return 9;
            else return 8;
        }
        else if (cameraState == 9)
        {
            if ((x <= 3.7) && (y <= 10.6f)) return 8;
            else if (x >= 10.4f) return 10;
            else return 9;
        }
        else if (cameraState == 10)
        {
            if (x <= 10.4f) return 9;
            else if (y <= 9.5f) return 12;
            return 10;
        }

        else if (cameraState == 11)
        {
            if (x <= 4.9f) return 2;
            else if (x >= 13.9f) return 6;
            else return 11;
        }

        else if (cameraState == 12)
        {
            if (y >= 9) return 10;
            else if (y <= 4.25) return 6;
            else return 12;
        }

        return 0;
    }
}
