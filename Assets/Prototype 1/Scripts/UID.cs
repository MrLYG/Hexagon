using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UID : MonoBehaviour
{
    public static string UUID;
    // Start is called before the first frame update
    void Start()
    {
        UUID = System.Guid.NewGuid().ToString("N");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
