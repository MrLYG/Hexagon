using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float speed = 5f;
    
    private Rigidbody2D rigidbody2D;
    private Vector3 velocity = Vector3.zero;
    [Range(0, .2f)] [SerializeField] private float smoothMovementTime = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

    }

    private void FixedUpdate()
    {
        //transform.Translate(Vector3.right * horizontal * Time.fixedDeltaTime * speed);
        //transform.Translate(Vector3.up * vertical * Time.fixedDeltaTime * speed);
        Vector2 targetVelocity = targetVelocity = new Vector2(horizontal * speed * Time.deltaTime * 10f, vertical * speed * Time.deltaTime * 10f);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, smoothMovementTime);
    }
}

