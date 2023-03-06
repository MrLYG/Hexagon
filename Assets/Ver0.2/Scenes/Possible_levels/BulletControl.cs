using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : IEnemy
{
    public float distance;

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.left * Time.fixedDeltaTime * speed);
        transform.localPosition += Vector3.right * Time.deltaTime * curSpeed;
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
            collision.gameObject.GetComponent<PlayerHP>().getHurt(Damage, InvincibleTime, collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
