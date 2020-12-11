using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class CamerasScript : MonoBehaviour
{
    public float duration = 0.2f;
    public float magnitude = 0.03f;
    public abstract void moveCameraToOrigin();

    public void returnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator shakeCamera()
    {
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
