using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    private AudioManager AudioManager;

    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
        AudioManager.Play("Win");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game!");
        Application.Quit();
    }

    public void playSoundSelect()
    {
        AudioManager.Play("Select");
    }
}
