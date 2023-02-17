using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet : MonoBehaviour
{
    [SerializeField] List<GameObject> lights;
    [SerializeField] int type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("ReverseLight"))
        {
            Instantiate(lights[type], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
