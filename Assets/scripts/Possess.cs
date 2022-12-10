using UnityEngine;

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
    private PossessedMovement _npcController;

    private SpriteRenderer _rend;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private Stats _stats;
    private Stats _npcStats;

    [SerializeField] private GameObject player;
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

        if (_possessing)
        {
            player.transform.position = _npcPossessed.transform.position;
        }
    }

    private void PossessFunc()
    {
        if(!_possessing)
        {
            if (_inRange)
            {
                _stats.PossessionCount += 1;
                Debug.Log("Possessed " + _candidate.name);
                _npcPossessed = _candidate;
                _npcController = _npcPossessed.GetComponent<PossessedMovement>();
                _npcStats = _npcPossessed.GetComponent<Stats>();
                _npcController.Activate();
                player.transform.position = _npcPossessed.transform.position;
                _rend.enabled = false;
                _inRange = false;
                _possessing = true;
                _candidate = null;
                _rb.bodyType = RigidbodyType2D.Static;
                _col.enabled = false;
                _stats.Hp = _npcStats.Hp;
                _stats.Type = _npcStats.Type;
            }
        }
        else
        {
            Release();
        }
    }

    public void Release()
    {
        Debug.Log("Released");
        if (_npcController != null)
        {
            _npcController.Activate();
            _npcController = null;
            _possessing = false;
            _npcPossessed = null;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _col.enabled = true;
            player.transform.position += new Vector3(0, .1f);
            _rend.enabled = true;
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
                    Debug.Log("see");
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
