using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet : MonoBehaviour
{
    [SerializeField] List<GameObject> lights;
    [SerializeField] int type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ground") || 
            collision.gameObject.CompareTag("Object") || collision.gameObject.CompareTag("DeadZone"))
        {
            Instantiate(lights[type], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
