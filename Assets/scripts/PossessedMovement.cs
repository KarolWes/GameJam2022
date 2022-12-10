using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessedMovement : PhysicsObject
{
    // Start is called before the first frame update
    protected Vector2 Pos;
    private Stats _stats;
    public bool _active = false;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;

    // Start is called before the first frame update
    void Start()
    {
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
            Vector2 move = Vector2.zero;
            move.x = Input.GetAxis("Horizontal"); // arrows or a-d
            if (Input.GetButtonDown("Jump") && Grounded) // space or w
            {
                Velocity.y = jumpSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (Velocity.y > 0)
                {
                    Velocity.y = Velocity.y * .5f;
                }
            }
    
            TargetVelocity = move * speed;
        }
    }
}
