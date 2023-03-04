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

        // analyse  ??  Falling to the deadzone,enemy can't be damag.
        Debug.Log(byObject.tag);


        if (hp <= 0)
        {
            if (!GetComponent<IEnemy>().stay)
            {
                // Deactivate the enemy
                gameObject.SetActive(false);
                return;
            }
            else
            {
                gameObject.tag = "Object";
                GetComponent<IEnemy>().enabled = false;
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
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
