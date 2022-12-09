using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Possess : MonoBehaviour
{
    [SerializeField] private float size = 1.0f;
    [SerializeField] private float possessionDelay = 0.5f;
    private bool _inRange = false;
    private bool _possessing = false;
    private bool _possessionCommand = false;
    private float _nextPossession = 0.15f;

    private GameObject _candidate;
    private GameObject _npcPossessed = null;

    private SpriteRenderer _rend;

    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _rend = player.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && Time.time > _nextPossession)
        {
            PossessFunc();
            _nextPossession = Time.time + possessionDelay;
        }
    }

    private void PossessFunc()
    {
        if(!_possessing)
        {
            if (_inRange)
            {
                Debug.Log("Possessed " + _candidate.name);
                _npcPossessed = _candidate;
                player.transform.position = _npcPossessed.transform.position;
                _rend.enabled = false;
                _inRange = false;
                _possessing = true;
                _candidate = null;
            }
        }
        else
        {
            Debug.Log("Released");
            player.transform.position += new Vector3(2 * size, 0);
            _rend.enabled = true;
            _possessing = false;
            _npcPossessed = null;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(transform.parent && transform.parent.gameObject != other.gameObject)
        {
            if (other.CompareTag("NPC"))
            {
                if(_npcPossessed == null || other.gameObject.name != _npcPossessed.name)
                {
                    _inRange = true;
                    _candidate = other.gameObject;
                    Debug.Log("see");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _inRange = false;
    }
}
