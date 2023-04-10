using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 20.0f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed* Time.deltaTime);
    }
}
