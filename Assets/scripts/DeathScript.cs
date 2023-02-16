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
    private AudioSource audio;

    [FormerlySerializedAs("player")] [SerializeField] private GameObject self;
    [SerializeField] private GameObject possessionManager;
    [SerializeField] private bool isPlayer = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _stats = self.GetComponent<Stats>();
        _stats.StartPos = self.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Hurt();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if(transform.parent && transform.parent.gameObject != other.gameObject)
        {
            if (other.CompareTag("Fire"))
            {
                _isTouching = true;
                audio = other.gameObject.GetComponent<AudioSource> ();
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
                Kill();
            }
        }
    }

    public void Kill()
    {
        print (self.name);
        DialogueManager.Instance.InvokeDialogue(self, 0);
        if (isPlayer)
        {
            self.GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            self.GetComponent<PossessedMovement>().enabled = false;
            _possess = FindObjectOfType<Possess> ();
        }
        gameObject.GetComponent<Collider2D>().enabled = false;
        if (self.name != "Player")
        {
            _possess.Release();
        }

        //death animation?
        StartCoroutine(UntilPlays(audio));
    }

    IEnumerator UntilPlays(AudioSource audio){
        audio.Play ();
        yield return new WaitWhile (()=> audio.isPlaying);
        if (transform.parent.gameObject.name == "Player")
        {
            _stats.KillCount += 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameObject.GetComponent<Collider2D>().enabled = true;
            self.GetComponent<PlayerMovement>().enabled = true;
            _stats.SaveFile();
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
