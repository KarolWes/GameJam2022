using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int spaceDelta = 5;

    private Vector2 _pos;

    private Vector2 _goal = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        _pos = transform.position;
        _goal = _pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _goal.x += spaceDelta;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _goal.x -= spaceDelta;
        }

        _pos = Vector2.MoveTowards(_pos, _goal, Time.deltaTime);
        transform.position = _pos;
        _goal = _pos;
    }
}
