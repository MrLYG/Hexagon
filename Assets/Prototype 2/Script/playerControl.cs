using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * horizontal * Time.fixedDeltaTime * speed);
        transform.Translate(Vector3.up * vertical * Time.fixedDeltaTime * speed);
    }
}

