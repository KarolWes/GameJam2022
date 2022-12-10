using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField] protected int speed = 5;
    [SerializeField] protected int jumpSpeed = 7;
    [SerializeField] protected float jumpDelay = 0.5f;
    
    
    protected const float MinMoveDist = 0.001f;
    protected const float ShellRad = 0.01f;
    protected float _nextjump= 0.15f;
    
    protected Vector2 Velocity;
    protected Vector2 TargetVelocity;
    protected Rigidbody2D RBody;
    protected ContactFilter2D ContactFilter;
    protected RaycastHit2D[] HitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> HitBufferList = new List<RaycastHit2D>(16);
    protected bool Grounded = false;
    protected Vector2 GroundNorm;
    private Vector2 movement = new Vector2(1,1);
    private Vector2 speedVec;

    public float gravityModifier = 0.5f;
    public float minGroundNormY = 0.65f;


    
    protected void OnEnable()
    {
        RBody = GetComponent<Rigidbody2D>();
        speedVec = new Vector2(speed, jumpSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        ContactFilter.useTriggers = false;
        ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(LayerMask.GetMask("Default")));
        ContactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        TargetVelocity = Vector2.zero;
        ComputeVelocity();
        // float inputX = Input.GetAxis ("Horizontal");
        // float inputY = Input.GetAxis ("Vertical");
        //
        // movement = new Vector2(
        //     speedVec.x * inputX,
        //     speedVec.y * inputY);
        //
        // if (Input.GetKeyDown ("space")){
        //     transform.Translate(Vector3.up * 260 * Time.deltaTime, Space.World);
        //} 
    }

    protected virtual void ComputeVelocity()
    {
        
    }

    private void FixedUpdate()
    {
        // RBody.velocity = movement;
        // RBody.AddForce(movement, ForceMode2D.Impulse);
        Velocity += Physics2D.gravity * (gravityModifier * Time.deltaTime);
        Velocity.x = TargetVelocity.x;
        Grounded = false;
        Vector2 moveOnGroud = new Vector2(GroundNorm.y, -GroundNorm.x);
        Vector2 deltaPos = Velocity * Time.deltaTime;
        Vector2 move = new Vector2(1,0) * deltaPos.x;
        Movement(move, false);
        move = Vector2.up*deltaPos.y;
        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMove)
    {
        float distance = move.magnitude;
        if (distance > MinMoveDist)
        {
            int count = RBody.Cast(move, ContactFilter, HitBuffer, distance + ShellRad);
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
                    if (!yMove)
                    {
                        GroundNorm = currNorm;
                        currNorm.y = 0; 
                    }
                }

                float projection = Vector2.Dot(Velocity, currNorm);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * currNorm;
                }

                float modifiedDist = HitBufferList[i].distance - ShellRad;
                distance = modifiedDist < distance ? modifiedDist : distance;
                
            }
        }
        
        RBody.position = RBody.position + move.normalized*distance;
    }
}
