using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<GameObject> items;
    [SerializeField] private float itemFollowingSpeed = 10f;
    [SerializeField] private float smoothTime = 0.1f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        items = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if(!HasItem(collision.gameObject))
                items.Add(collision.gameObject);
            //UpdateItemList();
        }
    }

    public bool HasItem(GameObject targetItem)
    {
        foreach (GameObject item in items)
        {
            if(item == targetItem)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(GameObject targetItem)
    {
        foreach (GameObject item in items)
        {
            if (item == targetItem)
            {
                items.Remove(item);
                Destroy(item);
                return;
            }
        }
    }

    private void ItemFollowing()
    {
        int x = 0;
        foreach (GameObject item in items)
        {
            x++;
            Vector3 targetLoc = transform.TransformPoint(new Vector3(x, -0.3f, 0));

            item.transform.position = Vector3.SmoothDamp(item.transform.position, targetLoc, ref velocity, smoothTime);
        }
    }

    private void FixedUpdate()
    {
        ItemFollowing();
    }
}
