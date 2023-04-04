using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    [SerializeField] private List<GameObject> Doors;
    private float numObj;
    private Color LockedColor;
    private Color OpenedColor;

    void Start()
    {
        // Set color to be same as button
        LockedColor = GetComponent<SpriteRenderer>().color;
        OpenedColor = LockedColor;
        OpenedColor.a = 0f;
        foreach (GameObject door in Doors)
        {
            door.GetComponent<SpriteRenderer>().color = LockedColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Object"))
        {
            numObj++;
            foreach (GameObject door in Doors)
            {
                door.GetComponent<BoxCollider2D>().isTrigger = true;
                door.GetComponent<SpriteRenderer>().color = OpenedColor;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Object"))
        {
            numObj--;
            if (numObj <= 0)
            {
                foreach (GameObject door in Doors)
                {
                    door.GetComponent<BoxCollider2D>().isTrigger = false;
                    door.GetComponent<SpriteRenderer>().color = LockedColor;
                }
            }
        }
    }
}
