using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool _open = false;

    public bool Open
    {
        get => _open;
        set => _open = value;
    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
