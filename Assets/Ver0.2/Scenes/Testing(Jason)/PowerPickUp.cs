using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickUp : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerSpecialBullet>().getPower(bullet);
            Destroy(gameObject);
        }
    }
}
