using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    [SerializeField] private GameObject Door;
    private float numObj;
    private Color LockedColor;
    private Color OpenedColor;

    void Start()
    {
        // Set color to be same as button
        LockedColor = GetComponent<SpriteRenderer>().color;
        OpenedColor = LockedColor;
        OpenedColor.a = 0.5f;
        Door.GetComponent<SpriteRenderer>().color = LockedColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Object"))
        {
            numObj++;
            Door.GetComponent<BoxCollider2D>().isTrigger = true;
            Door.GetComponent<SpriteRenderer>().color = OpenedColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Object"))
        {
            numObj--;
            if (numObj <= 0)
            {
                Door.GetComponent<BoxCollider2D>().isTrigger = false;
                Door.GetComponent<SpriteRenderer>().color = LockedColor;
            }
        }
    }
}
