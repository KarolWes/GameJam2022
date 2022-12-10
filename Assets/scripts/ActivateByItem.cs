using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateByItem : MonoBehaviour
{

    private bool _active = false;

    [SerializeField] private GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Activate(List<GameObject> inventory)
    {
        foreach (var el in inventory)
        {
            if (el == item)
            {
                _active = true;
                return true;
            }
        }

        return false;
    }
}
