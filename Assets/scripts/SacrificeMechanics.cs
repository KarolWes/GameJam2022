using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeMechanics : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject possessionManager;
    private Possess _possess;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        _possess = possessionManager.GetComponent<Possess>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(transform.parent && transform.parent.gameObject != other.gameObject)
        {
            if (other.CompareTag("NPC"))
            {
                if (_possess.IsPossessing() && _possess.NpcPossessed.GetComponent<Stats>().Inventory.Count == 0)
                {
                    Debug.Log("Sacrifice point available");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
