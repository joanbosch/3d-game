using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class CamerasScript : MonoBehaviour
{
    public abstract void moveCameraToOrigin();

    public void returnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
