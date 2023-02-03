using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : PhysicObjectBeta
{
    private Stats _stats;

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        Angle *= Dir;

        if (Angle <= 1 || !Grounded)
        {
            move.x = Input.GetAxis("Horizontal");

        }
        if (Input.GetButtonDown("Jump") && Grounded)
        {
            Velocity.y = jumpHeight;
            Jumped = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (Velocity.y > 0)
            {
                Velocity.y *= 0.5f;
            }
        }
        TargetVelocity = move * speed;
    }
    
    
}
