using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Cameras : CamerasScript
{
    private int cameraState = 1;
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
    //private EnableSnakeMode sm;

    // Sounds!
    private AudioManager AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
        AudioManager.Play("Electric");
        setUpMovingCamera();
        setUpMovingPipesCamera();
        cameras = new List<Vector3>();
        cameras.Add(new Vector3(-5.08f, 0.3f, -5.5f));
        cameras.Add(new Vector3(-0.05f, 0.3f, -5.5f));
        cameras.Add(new Vector3(-0.05f, 6.21f, -5.3f));
        cameras.Add(new Vector3(-0.08f, -5.51f, -5.4f));
        cameras.Add(new Vector3(5.61f, -5.5f, -5.4f));
        cameras.Add(new Vector3(11.35f, -4.1f, -5.4f));
        cameras.Add(new Vector3(11.35f, 6.06f, -5.3f));
        cameras.Add(new Vector3(17.25f, 4.17f, -6.3f));
        cameras.Add(new Vector3(18.48f, -1.14f, -4f));
        Player = GameObject.Find("Player");
        cameraState = 1;
        gameObject.transform.position = cameras[0];

        // Move camera to origin variables
        cameraToOrigin = false;

        // If the camera is reset, reset the snake mode.
        //sm = GameObject.Find("Snake5").GetComponent<EnableSnakeMode>();
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
        }
        else if (cameraToOrigin)
        {
            // The origin game must be in the first position of the cameras array!

            moveCameraTo(lastCameraPos, cameras[0]);
        }
        else
        {
            int nextCameraState = nextState(Player.transform.position, cameraState);
            if ((cameraState == 10) || (cameraState == 11)) followPlayer(lastCameraPos, new Vector3(Player.transform.position.x, Player.transform.position.y, -4f));
            if (cameraState != nextCameraState)
            {
                if ((nextCameraState == 10) || (nextCameraState == 11)) { setUpMovingPipesCamera(); }
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

    public override void moveCameraToOrigin()
    {
        cameraToOrigin = true;
        cameraState = 1;
        //sm.reActive();
    }

    private int nextState(Vector3 playerPos, int cameraState)
    {
        float x = playerPos.x;
        float y = playerPos.y;
        if (cameraState == 1)
        {
            if (x >= -2.68f) return 2;
            else return 1;
        }

        else if (cameraState == 2)
        {
            if (x <= -2.68f) return 1;
            else if (y >= 3.2f) return 3;
            else if (y <= -2.75f) return 4;
            else return 2;
        }

        else if (cameraState == 3)
        {
            if (y <= 3.2f) return 2;
            else if (x >= 2.51f) return 10;
            else return 3;
        }

        else if (cameraState == 4)
        {
            if (y >= -2.75f) return 2;
            else if (x >= 2.6f) return 5;
            else return 4;
        }

        else if (cameraState == 5)
        {
            if (x <= 2.6f) return 4;
            else if (x >= 8.5f) return 6;
            else return 5;
        }

        else if (cameraState == 6)
        {
            if (x <= 8.5f) return 5;
            else if (y >= -1.56f) return 11;
            else return 6;
        }

        else if (cameraState == 7)
        {
            if (x >= 14.46f) return 8;
            else if (x <= 8.68f) return 10;
            else if (y <= 3.19f) return 11;
            else return 7;
        }

        else if (cameraState == 8)
        {
            if (x <= 14.46f) return 7;
            else if (y <= 0.62f) return 9;
            else return 8;
        }

        else if (cameraState == 9)
        {
            if (y >= 0.62f) return 8;
            else return 9;
        }

        else if (cameraState == 10)
        {
            if (x <= 2.51f) return 3;
            else if (x >= 8.68f) return 7;
            else return 10;
        }

        else if (cameraState == 11)
        {
            if (y <= -1.56f) return 6;
            else if (y >= 3.19f) return 7;
            else return 11;
        }

        return 0;
    }
}
