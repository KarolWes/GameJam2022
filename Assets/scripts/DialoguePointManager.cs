using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePointManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _active = true;
    private BoxCollider2D _boxCollider;
    [SerializeField] private int sentence;
    [SerializeField] private GameObject player;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(transform.parent && transform.parent.gameObject != other.gameObject)
        {
            Debug.Log("1");
            if (other.CompareTag("NPC"))
            {
                Debug.Log("2");
                if (_active)
                {
                    Debug.Log("3");
                    _active = false;
                    DialogueManager.Instance.InvokeDialogue(player, sentence);
                }
            }
        }
    }
}
