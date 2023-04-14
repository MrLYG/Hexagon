using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemyObstacle : MonoBehaviour
{
    public float Damage;
    public float InvincibleTime;
    public bool HarmOnTouch = true;

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (HarmOnTouch && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHP>().getHurt(Damage, InvincibleTime, gameObject);
        }
    }
    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (HarmOnTouch && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHP>().getHurt(Damage, InvincibleTime, gameObject);
        }
    }
}
