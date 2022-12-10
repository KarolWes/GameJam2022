using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    [SerializeField] private float activityDelay = 0.5f;
    [SerializeField] private GameObject self;
    private float _nextActivity= 0.15f;
    private PossessedMovement _controller;
    private GameObject _candidate;

    private List<GameObject> _inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _controller = self.GetComponent<PossessedMovement>();
        _inventory = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller._active)
        {
            if (Input.GetKey(KeyCode.E) && Time.time > _nextActivity)
            {
                Debug.Log("doing something");
                _nextActivity = Time.time + activityDelay;
                if (_candidate.CompareTag("Takable"))
                {
                    _inventory.Add(_candidate);
                    _candidate.GetComponent<ItemManager>().Activate();
                }
            }

            if (Input.GetKey(KeyCode.I))
            {
                foreach (var entry in _inventory)
                {
                    Debug.Log(entry);
                }
                {
                    
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(transform.parent && transform.parent.gameObject != other.gameObject)
        {
            Debug.Log("See" + other.gameObject.name);
            _candidate = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
