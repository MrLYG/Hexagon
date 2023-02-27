using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILight : MonoBehaviour
{
    public float ApperanceTime;
    public List<GameObject> AffectedObjects = new List<GameObject>();

    public virtual void Start()
    {
        if (ApperanceTime > 0)
            Invoke("RemoveLight", ApperanceTime);
    }
    
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    public virtual void RemoveLight()
    {
        Destroy(gameObject);
    }
}
