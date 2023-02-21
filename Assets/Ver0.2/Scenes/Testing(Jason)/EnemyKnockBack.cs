using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackForce;

    public void KnockBack(GameObject byObject)
    {
        if (byObject.transform.position.x - transform.position.x < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(knockBackForce, knockBackForce, 0));
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(-knockBackForce, knockBackForce, 0));
        }
    }
}
