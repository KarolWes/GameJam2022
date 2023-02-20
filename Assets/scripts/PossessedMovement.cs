using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PossessedMovement : PlayerMovement
{
    // Start is called before the first frame update
    [FormerlySerializedAs ("_active")] public bool active = false;

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
        active ^= true;
    }
    
    protected override void ComputeVelocity()
    {
        if (active)
        {
            base.ComputeVelocity();
        }
    }

    protected override void UpdateFunction()
    {
        if (active)
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
