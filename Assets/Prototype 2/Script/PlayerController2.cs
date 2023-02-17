using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    // Start is called before the first frame update
    private float moveSpeed = 10.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D boxRigidBody;


    void Start()
    {
        boxRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * moveSpeed);
        transform.Translate(Vector3.up * Time.deltaTime * verticalInput * moveSpeed);

        boxRigidBody.AddForce(new Vector3(5, 5, 5));
    }
}
