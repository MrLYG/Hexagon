using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(5f, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 V = rb.velocity;
        
        if(V.magnitude > maxSpeed)
        {
            rb.velocity = V.normalized * maxSpeed;
        }
    }
}
