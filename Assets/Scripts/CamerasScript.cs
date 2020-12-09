using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class CamerasScript : MonoBehaviour
{
    public float duration = 1.2f;
    public float magnitude = 10f;
    public abstract void moveCameraToOrigin();

    public void returnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void shakeCamera()
    {
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + x, gameObject.transform.position.x + y, gameObject.transform.position.z);
            elapsedTime += Time.deltaTime;
        }
        transform.position = originalPosition;
    }
}
