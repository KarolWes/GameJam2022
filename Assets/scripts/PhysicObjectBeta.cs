using System.Collections.Generic;
using UnityEngine;

public class PhysicObjectBeta : MonoBehaviour
{

    [SerializeField] protected int weight = 5; // modifies speed of falling
    [SerializeField] protected int speed = 5; // speed of horizontal movement
    [SerializeField] protected float jumpHeight = 1; // scaled to blocks
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
    }

    // Start is called before the first frame update
    void Start()
    {
        ContactFilter.useTriggers = false;
        ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(LayerMask.GetMask("Default")));
        ContactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Velocity += Physics2D.gravity * (gravityModifier * Time.deltaTime);
        Vector2 deltaPos = Velocity * Time.deltaTime;
        Vector2 move = Vector2.up * deltaPos.y;
        Movement(move);
    }

    void Movement(Vector2 move)
    {
        RBody.position += move;
    }
}
