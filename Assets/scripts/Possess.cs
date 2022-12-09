using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possess : MonoBehaviour
{
    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (inRange)
            {
                Debug.Log("Possessed");
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(transform.parent && transform.parent.gameObject != other.gameObject)
        {
            if (other.CompareTag("NPC"))
            {
                inRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
    }
}
