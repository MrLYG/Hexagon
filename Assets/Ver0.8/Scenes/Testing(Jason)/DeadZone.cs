using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : IEnemyObstacle
{
    [SerializeField] private float DamageToEnemy;
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<IEnemy>())
        {
            collision.gameObject.GetComponent<IEnemy>().damage(gameObject, DamageToEnemy);
        }
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<IEnemy>())
        {
            collision.gameObject.GetComponent<IEnemy>().damage(gameObject, DamageToEnemy);
        }
    }
}
