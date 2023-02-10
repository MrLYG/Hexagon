using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveLeft : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.fixedDeltaTime * speed);
        DestroyGameObject();
    }

    void DestroyGameObject()
    {
        if (gameObject.transform.position.x < 51f)
        {
            Destroy(gameObject);
        }
    }
}
