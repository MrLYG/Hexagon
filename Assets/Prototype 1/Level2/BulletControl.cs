using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float distance;
    public float speed = 0.5f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.left * Time.fixedDeltaTime * speed);
        transform.localPosition += Vector3.right * Time.deltaTime * speed;
        DestroyGameObject();
    }

    void DestroyGameObject()
    {
        if (gameObject.transform.localPosition.x > distance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
