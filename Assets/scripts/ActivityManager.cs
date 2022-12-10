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
                
                _nextActivity = Time.time + activityDelay;
                if (_candidate.CompareTag("Takable"))
                {
                    Debug.Log("takeing");
                    _inventory.Add(_candidate);
                    _candidate.GetComponent<ItemManager>().Activate();
                }

                if (_candidate.CompareTag("Door"))
                {
                    Debug.Log("opening");
                    Debug.Log(_candidate.GetComponent<ActivateByItem>().Activate(_inventory));
                }
            }

            if (Input.GetKey(KeyCode.I))
            {
                foreach (var entry in _inventory)
                {
                    Debug.Log(entry);
                }
                Debug.Log("___");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(transform.parent && transform.parent.gameObject != other.gameObject)
        {
            if(!other.gameObject.CompareTag("World"))
            {
                _candidate = other.gameObject;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _candidate = null;
    }
}
