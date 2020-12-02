using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSnakeMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        SnakeMecanisim sm = GameObject.Find("Player").GetComponent<SnakeMecanisim>();
        sm.enableSnakeMode();
        gameObject.SetActive(false);
    }


}
