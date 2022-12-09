using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    protected const float MinMoveDist = 0.001f;
    protected const float ShellRad = 0.01f;
    
    protected Vector2 Velocity;
    protected Rigidbody2D RBody;
    protected ContactFilter2D ContactFilter;
    protected RaycastHit2D[] HitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> HitBufferList = new List<RaycastHit2D>(16);
    protected bool Grounded = false;
    protected Vector2 GroundNorm;
     
    
    public float gravityModifier = 0.5f;
    public float minGroundNormY = 0.65f;


    private void OnEnable()
    {
        RBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ContactFilter.useTriggers = false;
        ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        ContactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        Velocity += Physics2D.gravity * (gravityModifier * Time.deltaTime);
        Grounded = false;
        Vector2 deltaPos = Velocity * Time.deltaTime;
        Vector2 move = Vector2.up*deltaPos.y;
        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMove )
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
                    if (yMove)
                    {
                        GroundNorm = currNorm;
                        currNorm.x = 0; 
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
