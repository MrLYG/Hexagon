using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] private GameObject key;
    [SerializeField] private Sprite Unlocked;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerInventory>().HasItem(key)) {
                collision.gameObject.GetComponent<PlayerInventory>().RemoveItem(key);
                gameObject.GetComponent<SpriteRenderer>().sprite = Unlocked;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }
}
