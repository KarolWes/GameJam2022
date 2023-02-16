using System;
using System.Collections.Generic;
using UnityEngine;

public class PossessedMovement : PlayerMovement
{
    // Start is called before the first frame update
    public bool _active = false;

    // [SerializeField] private Dictionary<string, bool> abilities = new Dictionary<String,Boolean>();
    

    // Start is called before the first frame update
    void Start()
    {
        ContactFilter.useTriggers = false;
        ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(LayerMask.GetMask("Default")));
        ContactFilter.useLayerMask = true;
    }
    
    

    // Update is called once per frame

    public void Activate()
    {
        _active ^= true;
    }
    
    protected override void ComputeVelocity()
    {
        if (_active)
        {
            base.ComputeVelocity();
        }
    }

    protected override void UpdateFunction()
    {
        if (_active)
        {
            if (Input.GetKey(KeyCode.A))
            {
                Dir = -1;
                Rend.flipX = false;
            }

            if (Input.GetKey(KeyCode.D))
            {
                Dir = 1;
                Rend.flipX = true;
            }
            TargetVelocity = Vector2.zero;
            ComputeVelocity();
        }
    }
}
