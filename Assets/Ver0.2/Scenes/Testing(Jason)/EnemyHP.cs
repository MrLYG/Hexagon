using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [Tooltip("Initial HP")]
    [SerializeField] private float initialHP = 2;
    private float hp = 2;

    private void Start()
    {
        resetHP();
    }

    public void damage(GameObject byObject, float damage)
    {
        //int damage = byObject.GetComponent<IWeapon>().Damage;
        hp -= damage;
        if (hp <= 0)
        {
            // Deactivate the enemy
            gameObject.SetActive(false);
            return;
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

    public void resetHP() {
        hp = initialHP;
    }
    public float getHp()
    {
        return hp;
    }
}
