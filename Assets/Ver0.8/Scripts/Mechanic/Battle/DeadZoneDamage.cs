using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneDamage : MonoBehaviour
{
    [SerializeField] private float canBeHurtCD = 1f;
    private bool canBeHurt = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            if (GetComponent<EnemyHP>() && canBeHurt)
            {
                GetComponent<EnemyHP>().damage(collision.gameObject, 1);
                canBeHurt = false;
                Invoke("resetHurtCD", canBeHurtCD);
            }
            //GetComponent<PlayerHP>().getHurt(0.5f, collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            if (GetComponent<EnemyHP>() && canBeHurt)
            {
                GetComponent<EnemyHP>().damage(collision.gameObject, 1);
                canBeHurt = false;
                Invoke("resetHurtCD", canBeHurtCD);
            }
            //GetComponent<PlayerHP>().getHurt(0.5f, collision.gameObject);
        }
    }

    private void resetHurtCD() {
        canBeHurt = true;
    }
}
