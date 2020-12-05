using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    // to track position of player
    public GameObject player;
    public Text UImessage;

    private bool writing;
    private int charInd;
    private float timePerChar = 0.1f;
    private string textToWrite;
    private float timer;

    //sound
    private AudioSource audioManager;

    // Start is called before the first frame update
    void Start()
    {
        writing = false;
        charInd = 0;
        audioManager = GetComponent<AudioSource>();
        textToWrite = "pink was ejected.";
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("player pos " + player.transform.position);
        if (player.transform.position.x > 470 && charInd == 0) writing = true;

        if (writing)
        {
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                //display next character
                audioManager.Play();
                timer += timePerChar;
                charInd++;
                string text = textToWrite.Substring(0, charInd);
                text += "<color=#00000000>" + textToWrite.Substring(charInd) + "</color>";  // to keep all letters on its final pos
                UImessage.text = text;

                if (charInd >= textToWrite.Length) writing = false;
            }
        }
    }
}
