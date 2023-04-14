using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackForce;

    public void KnockBack(Vector3 hitPoint)
    {
        if (hitPoint.x - transform.position.x < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(knockBackForce, knockBackForce, 0));
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(-knockBackForce, knockBackForce, 0));
        }
    }
}
