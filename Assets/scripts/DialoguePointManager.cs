using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class DialoguePointManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _active = true;
    private BoxCollider2D _boxCollider;
    [SerializeField] private int sentence;
    [SerializeField] private GameObject player;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject() == player || (other.gameObject.CompareTag ("NPC") && other.gameObject.GetComponent<PossessedMovement> ().active))
        {
            if (_active)
            {
                _active = false;
                DelayedShow();
                
            }
        }
    }

    async void DelayedShow()
    {
        await Task.Delay(1000);
        DialogueManager.Instance.InvokeDialogue(player, sentence);
    }
}
