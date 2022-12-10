using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float yPoint;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, yPoint, -10);
    }
}
