using System.Collections;
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
            _nextHit = Time.time + _hitDelay;
            if (_stats.Hp <= 0)
            {
                Kill(gameObject.GetComponent<AudioSource>());
            }
        }
    }

    public void Kill(AudioSource audio)
    {
        if (isPlayer)
        {
            self.GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            self.GetComponent<PossessedMovement>().enabled = false;
        }
        gameObject.GetComponent<Collider2D>().enabled = false;
        if (self.name == "Player")
        {
            _possess.Release();
        }

        //death animation?
        StartCoroutine(UntilPlays(audio));
    }

    IEnumerator UntilPlays(AudioSource audio){
        audio.Play ();
        yield return new WaitWhile (()=> audio.isPlaying);
        Debug.Log(gameObject.name + " Finished");
        if (transform.parent.gameObject.name == "Player")
        {
            _stats.KillCount += 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameObject.GetComponent<Collider2D>().enabled = true;
            self.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            Destroy(self);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _isTouching = false;
    }
}
