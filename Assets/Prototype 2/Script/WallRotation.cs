using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotation : MonoBehaviour
{

    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, speed);
    }
}
