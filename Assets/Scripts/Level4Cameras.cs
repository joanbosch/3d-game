using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Cameras : CamerasScript
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
        AudioManager.Play("MedBayRoom");
        setUpMovingCamera();
        setUpMovingPipesCamera();
        cameras = new List<Vector3>();
        cameras.Add(new Vector3(0f, 0f, -6f)); // 1 FOV: 60
        cameras.Add(new Vector3(6.4f, 0f, -5.3f)); // 2 FOV: 60
        cameras.Add(new Vector3(11.423f, -2.656f, -7.088f)); // 3 FOV: 60
        cameras.Add(new Vector3(23.96f, -6.75f, -6.64f)); // 4 FOV: 60
        cameras.Add(new Vector3(29.38f, -12f, -7.11f)); // 5 FOV: 60
        cameras.Add(new Vector3(32.55f, -5.56f, -4.76f)); // 6 FOV: 60
        cameras.Add(new Vector3(27.37f, -20.17f, -8f)); // 7 FOV: 60
        cameras.Add(new Vector3(14.17f, -20.17f, -8f)); // 8 FOV: 60
        cameras.Add(new Vector3(33.4f, -17.347f, -4.635f)); // 9 FOV: 60
        cameras.Add(new Vector3(16.502f, -4.418f, -5.759f)); // 11 (pos 10)
        Player = GameObject.Find("Player");
        cameraState = 1;
        gameObject.transform.position = cameras[0];

        // Move camera to origin variables
        cameraToOrigin = false;

        // If the camera is reset, reset the snake mode.
        sm = GameObject.Find("Snake4").GetComponent<EnableSnakeMode>();
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
            if (cameraState == 11) followPlayer(lastCameraPos, new Vector3(Player.transform.position.x, -20.17f, -8.3f));
            if (cameraState != nextCameraState)
            {
                if (nextCameraState == 11) { setUpMovingPipesCamera(); }
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
        Debug.Log(t);
            
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
            if (x >= 2.6f) return 2;
            else return 1;
        }

        else if (cameraState == 2)
        {
            if (x >= 10f) return 3;
            else if (x <= 2.6f) return 1;
            else return 2;
        }

        else if (cameraState == 3)
        {
            if (x <= 10f) return 2;
            else if (x >= 13.3f) return 10;
            else return 3;
        }

        else if (cameraState == 4)
        {
            if (x <= 21.3f) return 10;
            else if (x >= 27.1f) return 5;
            else return 4;
        }

        else if (cameraState == 5)
        {
            if (x <= 27.1f) return 4;
            else if (y >= -8f) return 6;
            else if (y <= -15.7f) return 7;
            else return 5;
        }

        else if (cameraState == 6)
        {
            if (y <= -8f) return 5;
            else return 6;
        }

        else if (cameraState == 7)
        {
            if (y >= -15.5f) return 5;
            else if (x <= 25.5f) return 11;
            else if (x >= 30.4f) return 9;
            else return 7;
        }

        else if (cameraState == 8)
        {
            if (x >= 16f) return 11;
            else return 8;
        }
        else if (cameraState == 9)
        {
            if (x <= 30.4f) return 7;
            else return 9;
        }

        else if (cameraState == 10)
        {
            if (x <= 13.3f) return 3;
            else if (x >= 21.3f) return 4;
            return 10;
        }

        else if (cameraState == 11)
        {
            if (x <= 16f) return 8;
            else if (x >= 25.5f) return 7;
            return 11;
        }

        return 0;
    }

    public override bool getMovingCamera()
    {
        return movingCamera;
    }

    public override bool getMovingCameraPipes()
    {
        return movingCameraPipes;
    }
}
