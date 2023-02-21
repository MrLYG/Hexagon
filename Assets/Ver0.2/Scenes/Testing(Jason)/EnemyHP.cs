using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    private int hp = 2;
    [SerializeField] private int initialHP = 2;

    private void Start()
    {
        hp = initialHP;
    }

    public void damage(GameObject byObject)
    {
        int damage = byObject.GetComponent<IWeapon>().Damage;
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        // If have knockback script, perform knockback
        if (GetComponent<EnemyKnockBack>() != null)
        {
            GetComponent<EnemyKnockBack>().KnockBack(byObject);
        }

        if (GetComponent<DamagePop>() != null)
        {
            GetComponent<DamagePop>().PopDamage(damage);
        }
    }
}
