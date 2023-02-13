using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveRight : MonoBehaviour
{
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.fixedDeltaTime * speed);
        DestroyGameObject();
    }

    void DestroyGameObject()
    {
        if (gameObject.transform.position.x > 39)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
