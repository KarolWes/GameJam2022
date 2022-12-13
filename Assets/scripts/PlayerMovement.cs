using UnityEngine;

public class PlayerMovement : PhysicObjectBeta
{
    private Stats _stats;
    private bool dir = true;

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && Grounded)
        {
            Velocity.y = jumpHeight;
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
