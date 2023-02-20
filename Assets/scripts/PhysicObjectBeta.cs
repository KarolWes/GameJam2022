using System;
using System.Collections.Generic;
using UnityEngine;



public class PhysicObjectBeta : MonoBehaviour
{
    public float gravityModifier = 0.5f;
    public float minGroundNormY = 0.65f;
    [SerializeField] protected int weight = 5; // modifies speed of falling
    [SerializeField] protected int speed = 5; // speed of horizontal movement
    [SerializeField] protected float jumpHeight = 1; // scaled to blocks
    [SerializeField] protected float jumpDelay = 0.5f;
    
    protected const float MinMoveDist = 0.001f;
    protected const float ShellRad = 0.01f;
    
    protected SpriteRenderer Rend;
    protected Rigidbody2D RBody;
    protected ContactFilter2D ContactFilter;
    protected RaycastHit2D[] HitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> HitBufferList = new List<RaycastHit2D>(16);
    protected Vector2 Velocity;
    protected Vector2 TargetVelocity;
    protected Vector2 GroundNorm;
    protected bool Grounded = false;
    protected bool Jumped = false;
    [SerializeField] protected int Dir = 1;
    protected float NextJump= 0.15f;
    protected float Angle;
    private Vector2 _movement = new Vector2(1,1);
    private Vector2 _speedVec;

    protected void OnEnable()
    {
        RBody = GetComponent<Rigidbody2D>();
        Rend = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ContactFilter.useTriggers = false;
        ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        ContactFilter.useLayerMask = true;
        
    }

    private void Update()
    {
        UpdateFunction();
    }

    protected virtual void UpdateFunction()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Dir = -1;
            Rend.flipX = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Dir = 1;
            Rend.flipX = false;
        }
        TargetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity() {}

    // Update is called once per frame
    void FixedUpdate()
    {
        Velocity += Physics2D.gravity * (gravityModifier * Time.deltaTime);
        Velocity.x = TargetVelocity.x;
        Grounded = false;
        Vector2 deltaPos = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(GroundNorm.y, -GroundNorm.x);
        Vector2 move = new Vector2();
        Angle = moveAlongGround.y / moveAlongGround.x;
        move = moveAlongGround * deltaPos.x;
        Movement(move, false, deltaPos);
        move = Vector2.up * deltaPos.y;
        Movement(move, true, deltaPos);
    }

    void Movement(Vector2 move, bool yMovement, Vector2 clearMove)
    {
        var distance = move.magnitude;
        if (distance > MinMoveDist)
        {
            var count = RBody.Cast(move, ContactFilter, HitBuffer, ShellRad);
            HitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                HitBufferList.Add(HitBuffer[i]);
            }

            for (int i = 0; i < HitBufferList.Count; i++)
            {
                Vector2 currNorm = HitBufferList[i].normal;
                if (currNorm.y > minGroundNormY)
                {
                    Grounded = true;
                    Jumped = false;
                    if (yMovement)
                    {
                        GroundNorm = currNorm;
                        currNorm.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currNorm);
                if (projection < 0)
                {
                    Velocity -= projection * currNorm;
                }

                float modifiedDist = HitBufferList[i].distance - ShellRad;
                distance = modifiedDist < distance ? modifiedDist : distance;
            }
        }

        if (yMovement ||!Grounded|| Angle <= 1)
        {
            if (Jumped)
            {
                RBody.position += clearMove.normalized*distance;
            }
            else
            {
                RBody.position += move.normalized*distance;
            }
            
        }
        
    }
}
