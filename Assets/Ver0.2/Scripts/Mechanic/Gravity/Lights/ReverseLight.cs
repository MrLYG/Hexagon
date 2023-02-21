using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseLight : MonoBehaviour
{
    [SerializeField] private float ApperanceTime;
    [SerializeField] private float AffectTime;
    [SerializeField] private List<GameObject> AffectedObjects = new List<GameObject>();
    private void Start()
    {
        Invoke("RemoveLight", ApperanceTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
           if(collision.gameObject.GetComponent<ObjectGravity>() != null)
            {
                collision.gameObject.GetComponent<ObjectGravity>().ReverseGravityDirection();
                AffectedObjects.Add(collision.gameObject);
                Invoke("ResetGravity", AffectTime);

                // Stop really fast falling (Optional)
                if(collision.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }
        }
    }

    private void ResetGravity()
    {
        if(AffectedObjects[0] != null)
        {
            AffectedObjects[0].GetComponent<ObjectGravity>().ReverseGravityDirection();
            AffectedObjects.RemoveAt(0);
        }
    }
    private void RemoveLight()
    {
        if(AffectedObjects.Count != 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<CircleCollider2D>().enabled = false;
            Invoke("RemoveLight", AffectTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
