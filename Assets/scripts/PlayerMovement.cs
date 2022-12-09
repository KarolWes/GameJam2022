using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PhysicsObject
{
    

    private Vector2 _pos;

    private Vector2 _goal = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        _pos = transform.position;
        _goal = _pos;
    }

    // Update is called once per frame
    
    protected override void ComputeVelocity()
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
