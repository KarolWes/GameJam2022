using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ActivityManager : MonoBehaviour
{
    [SerializeField] private float activityDelay = 0.5f;
    [SerializeField] private GameObject self;
    private float _nextActivity= 0.15f;
    private PossessedMovement _controller;
    private GameObject _candidate;
    private Stats _stats;
    [SerializeField] private bool isPlayer = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _controller = self.GetComponent<PossessedMovement>();
        _stats = self.GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if ( isPlayer || _controller._active)
        {
            
            if (Input.GetKey(KeyCode.E) && Time.time > _nextActivity)
            {
                
                Debug.Log(_candidate);
                if(_candidate != null)
                {
                    if (!isPlayer)
                    {
                        if (_candidate.CompareTag("Takable"))
                        {
                            Debug.Log("takeing");
                            _stats.Inventory.Add(_candidate);
                            _candidate.GetComponent<ItemManager>().Activate();
                        }
                    }
                    Debug.Log(_candidate.gameObject.name);
                    if (_candidate.CompareTag("Door"))
                    {
                        if (_candidate.GetComponent<DoorController>().Open)
                        {
                            Debug.Log("You finished the level");
                            SceneManager.LoadScene("New Scene");
                        }
                        else
                        {
                            if (!isPlayer)
                            {
                                Debug.Log("opening");
                                Debug.Log(_candidate.GetComponent<ActivateByItem>().Activate(_stats.Inventory));
                            }
                        }
                    }
                }
                _nextActivity = Time.time + activityDelay;
            }

            if (Input.GetKey(KeyCode.I))
            {
                foreach (var entry in _stats.Inventory)
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
                Debug.Log(_candidate);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("World"))
        {
            _candidate = null;
        }
    }
}
