using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [Tooltip("Initial HP")]
    [SerializeField] private int initialHP = 2;
    private int hp = 2;

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
            // Deactivate the enemy
            gameObject.SetActive(false);
        }

        // If have knockback script, perform knockback
        if (GetComponent<EnemyKnockBack>() != null)
        {
            GetComponent<EnemyKnockBack>().KnockBack(byObject);
        }

        // If have damage script, perform damage poping
        if (GetComponent<DamagePop>() != null)
        {
            GetComponent<DamagePop>().PopDamage(damage);
        }
    }
}
