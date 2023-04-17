using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : IObject
{
    public bool clockwise = false;
    // Update is called once per frame
    void Update()
    {
        if(clockwise)
        {
            transform.Rotate(-1 * Vector3.forward, curSpeed * Time.deltaTime);
        } else
        {
            transform.Rotate(Vector3.forward, curSpeed * Time.deltaTime);
        }
    }
}
