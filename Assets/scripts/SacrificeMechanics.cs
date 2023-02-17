using UnityEngine;

public class SacrificeMechanics : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject possessionManager;
    [SerializeField] private AudioClip sacrificeAudio;
    private Possess _possess;
    private bool _ready = false;
    private AudioSource audioS;

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
        if (Input.GetKey(KeyCode.K))
        {
            if (_ready)
            {
                _ready = false;
                Sacrifice();
            }
        }
    }

    private void Sacrifice()
    {
        DialogueManager.Instance.InvokeDialogue(player, 6);
        var npc = _possess.NpcPossessed;
        _possess.Release();
        audioS = player.GetComponent<AudioSource> ();
        audioS.clip = sacrificeAudio;
        audioS.playOnAwake = false;
        npc.GetComponentInChildren<DeathScript>().Kill(audioS);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(transform.parent && transform.parent.gameObject != other.gameObject)
        {
            if (other.CompareTag("NPC"))
            {
                if (_possess.IsPossessing() && _possess.NpcPossessed.GetComponent<Stats>().Inventory.Count == 0)
                {
                    _ready = true;
                    Debug.Log("Sacrifice point available");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(transform.parent && transform.parent.gameObject != other.gameObject)
        {
            if (other.CompareTag("NPC"))
            {
                _ready = false;
            }
        }
    }
}