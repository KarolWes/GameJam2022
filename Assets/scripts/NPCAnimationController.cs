using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject self;

    private PossessedMovement cntrl;
    // Start is called before the first frame update
    void Start()
    {
        
    }
	

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.W))
        {
            animator.SetBool("jump", true);
        }
        if (Input.GetKeyUp (KeyCode.W))
        {
            animator.SetBool("jump", false);
        }
        if (Input.GetKeyDown (KeyCode.A))
        {
            animator.SetTrigger("left");
        }
        if (Input.GetKeyDown (KeyCode.D))
        {
            animator.SetTrigger ("right");
        }
    }
}
