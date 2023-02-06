using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject target;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
        updateMovement();
    }

    public void setTarget(GameObject newTarget)
    {
        target = newTarget;
        updateMovement();
    }

    private void updateMovement()
    {
        if (target != null)
        {
            GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * speed;
            //GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControl>().getHurt();
            Destroy(this);
            Debug.Log("Game Over");
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this);
        }
    }
}
