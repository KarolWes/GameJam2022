using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Possess : MonoBehaviour
{
    [SerializeField] private float size = 1.0f;
    [SerializeField] private float possessionDelay = 0.5f;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip possSound;
    
    private bool _inRange = false;
    private bool _possessing = false;
    private bool _possessionCommand = false;
    private float _nextPossession = 0.15f;

    private GameObject _candidate;
    private GameObject _npcPossessed = null;
    private DeathScript _death;
    private PossessedMovement _npcController;

    private SpriteRenderer _rend;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private Stats _stats;
    private Stats _npcStats;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _rend = player.GetComponent<SpriteRenderer>();
        _rb = player.GetComponent<Rigidbody2D>();
        _col = player.GetComponent<CapsuleCollider2D>();
        _stats = player.GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && Time.time > _nextPossession)
        {
            PossessFunc();
            _nextPossession = Time.time + possessionDelay;
        }

        if (Input.GetKey(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _stats.LoadFile();
        }
        // if (Input.GetKey(KeyCode.V))
        // {
        //     SceneManager.LoadScene("level1withcolliders");
        // }
        //
        // if (Input.GetKey(KeyCode.B))
        // {
        //     Debug.Log(_stats.PossessionCount);
        // }

        if (_possessing)
        {
            player.transform.position = _npcPossessed.transform.position;
        }
    }

    private void PossessFunc()
    {
        if (_inRange)
        {
            if (_possessing)
            {
                Release();
            }

            _stats.PossessionCount += 1;
            Debug.Log("Possessed " + _candidate.name);
            _npcPossessed = _candidate;
            _npcController = _npcPossessed.GetComponent<PossessedMovement>();
            _npcStats = _npcPossessed.GetComponent<Stats>();
            _npcController.Activate();
            DialogueManager.Instance.InvokeDialogue(_npcPossessed, 2);
            _death = _npcPossessed.GetComponentInChildren<DeathScript>();
            player.transform.position = _npcPossessed.transform.position;
            _rend.enabled = false;
            _inRange = false;
            _possessing = true;
            _candidate = null;
            _rb.bodyType = RigidbodyType2D.Static;
            _col.enabled = false;
            _stats.Hp = _npcStats.Hp;
            _stats.Type = _npcStats.Type;
            player.GetComponentInChildren<ActivityManager>().enabled = false;
            
        }
        else
        {
            Release();
        }
        _stats.SaveFile();
    }

    public void Release()
    {
        Debug.Log("Released");
        if (_npcController != null)
        {
            _possessing = false;
            var audio = _npcPossessed.GetComponent<AudioSource>();
            _col.enabled = true;
            player.transform.position += new Vector3(.3f, .3f);
            _rend.enabled = true;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _npcController.Activate();
            _npcController = null;
            _npcPossessed = null;
            player.GetComponentInChildren<ActivityManager>().enabled = true;
            transform.position = new Vector3(0, 0, 0);
            _death.Kill(audio);
            _death = null;

        }
        
        _stats.Hp = 1;
        _stats.Type = "ghost";
    }

    public bool IsPossessing()
    {
        return _npcPossessed != null;
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
                    DialogueManager.Instance.InvokeDialogue(_candidate, 1);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            _inRange = false;
        }
    }

    public GameObject NpcPossessed
    {
        get => _npcPossessed;
    }
}
