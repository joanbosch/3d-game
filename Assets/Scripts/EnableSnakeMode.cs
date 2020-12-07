using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSnakeMode : MonoBehaviour
{

    private AudioManager AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Play("PickElement1");
        SnakeMecanisim sm = GameObject.Find("Player").GetComponent<SnakeMecanisim>();
        sm.enableSnakeMode();
        gameObject.SetActive(false);
    }

    public void reActive()
    {
        gameObject.SetActive(true);
    }


}
