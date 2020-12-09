using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager AudioManager;

    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
        AudioManager.Play("Pods");
    }
    public void StartGame()
    {
        
        // load next scene on Queue
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game!");
        Application.Quit();
    }

    public void LoadLevel(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void playSoundSelect()
    {
        AudioManager.Play("Select");
    }
}
