using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketScript : MonoBehaviour
{
    private float elapsedTime;
    private bool startCounter;
    public float timeToLeave = 3.0f;

    // particles
    private ParticleSystem smokePart;
    private ParticleSystem firePart;

    private AudioManager AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
        elapsedTime = 0f;
        startCounter = false;

        smokePart = GetSystem("SmokeParticles");
        firePart = GetSystem("FireParticles");
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.Play("Rocket");
            elapsedTime = 0f;
            startCounter = true;
            firePart.Play();
            smokePart.Play();
            collision.collider.gameObject.SetActive(false);
        }
    }

    // to get specific particle system
    private ParticleSystem GetSystem(string systemName)
    {
        Component[] children = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem childParticleSystem in children)
        {
            if (childParticleSystem.name == systemName)
            {
                return childParticleSystem;
            }
        }
        return null;
    }
}
