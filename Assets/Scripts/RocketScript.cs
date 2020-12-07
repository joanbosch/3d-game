using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketScript : MonoBehaviour
{
    private float elapsedTime;
    private bool startCounter;
    public float timeToLeave = 3.0f;
    private AudioManager AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
        elapsedTime = 0f;
        startCounter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCounter)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timeToLeave)
            {
                startCounter = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Play("Rocket");
            elapsedTime = 0f;
            startCounter = true;
            // TODO: SHOW ROCKET PARTICLES!!
            other.gameObject.SetActive(false);
        }
    }
}
