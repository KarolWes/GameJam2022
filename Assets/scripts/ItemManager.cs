using System;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update
    private String _tag;
    private bool _active = false;
    private SpriteRenderer _rend;
    private Collider2D _col;
    
    [SerializeField] private AudioClip keySound;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _tag = gameObject.tag;
        _rend = gameObject.GetComponent<SpriteRenderer>();
        _col = gameObject.GetComponent<CapsuleCollider2D>();
    }

    public void Activate()
    {
        switch (tag)
        {
            case "Takable":
                _rend.enabled = false;
                _col.enabled = false;
                break;
            case "Door":
                
                break;
            default:
                Debug.Log("unrecognized type");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
