using UnityEngine;

public class PlayerMovement : PhysicsObject
{
    private Stats _stats;
    private bool dir = true;

    // Start is called before the first frame update
    void Start()
    {
        ContactFilter.useTriggers = false;
        ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(LayerMask.GetMask("Default")));
        ContactFilter.useLayerMask = true;
        
    }

    // Update is called once per frame
    
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal"); // arrows or a-d
        if (move.x < 0 && dir)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            dir = false;
        }

        if (move.x > 0 && !dir)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            dir = true;
        }
        if (move.x != 0)
        {
            
        }
        if (Input.GetButtonDown("Jump") && Grounded && Time.time > _nextjump) // space or w
        {
            Velocity.y = jumpSpeed;
            _nextjump = Time.time + jumpDelay;
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
