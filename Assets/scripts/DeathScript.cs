using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class DeathScript : MonoBehaviour
{
    private bool _isTouching;
    private Stats _stats;
    private float _nextHit = 0.15f;
    private float _hitDelay = 1.0f;
    private Possess _possess;

    [FormerlySerializedAs("player")] [SerializeField] private GameObject self;
    [SerializeField] private GameObject possessionManager;
    [SerializeField] private bool isPlayer = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (isPlayer)
        {
            _possess = possessionManager.GetComponent<Possess>();
        }
        _stats = self.GetComponent<Stats>();
        _stats.StartPos = self.transform.position;
        
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
            Debug.Log(transform.parent.gameObject + " hit " + _stats.Hp);
            _nextHit = Time.time + _hitDelay;
            if (_stats.Hp <= 0)
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
                Debug.Log(GetComponent<AudioSource>());
                GetComponent<AudioSource>().Play();
                Debug.Log(_stats.StartPos);
                _possess.Release();
                //death animation?
                StartCoroutine(UntilPlays());
                
            }
        }
    }

    IEnumerator UntilPlays(){
        var audio = gameObject.GetComponent<AudioSource>();
        audio.Play ();
        yield return new WaitWhile (()=> audio.isPlaying);
        Debug.Log(gameObject.name + " Finished");
        if (transform.parent.gameObject.name == "Player")
        {
            _stats.KillCount += 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _isTouching = false;
    }
}
