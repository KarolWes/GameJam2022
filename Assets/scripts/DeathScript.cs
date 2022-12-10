using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    private bool _isTouching;
    private Stats _stats;
    private float _nextHit = 0.15f;
    private float _hitDelay = 1.0f;

    [SerializeField] private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        player.GetComponent<PlayerMovement>();
        _stats = player.GetComponent<Stats>();
        _stats.StartPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Hurt();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(transform.parent && transform.parent.gameObject != other.gameObject)
        {
            if (other.CompareTag("Fire"))
            {
                _isTouching = true;
            }
        }
    }

    private void Hurt()
    {
        if (_isTouching && Time.time > _nextHit)
        {
            
            _stats.Hp -= 1;
            Debug.Log("hit" + _stats.Hp);
            _nextHit = Time.time + _hitDelay;
            if (_stats.Hp <= 0)
            {
                //death animation?
                player.transform.position = _stats.StartPos;
                _stats.KillCount += 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isTouching = false;
    }
}
