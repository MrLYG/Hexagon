using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IEnemy))]
[RequireComponent(typeof(EnemyTrack))]
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
        if (!GetComponent<IEnemy>().enabled)
            return;
        //int damage = byObject.GetComponent<IWeapon>().Damage;
        hp -= damage;

        // analyse  ??  Falling to the deadzone,enemy can't be damag.
        Debug.Log(byObject.tag);
        gameObject.GetComponent<EnemyTrack>().harm += damage;
        if (byObject.CompareTag("Weapon"))
        {
            gameObject.GetComponent<EnemyTrack>().weapon = true;
            gameObject.GetComponent<EnemyTrack>().numOfWeapon += 1;
        }
        if (byObject.CompareTag("DeadZone"))
        {
            gameObject.GetComponent<EnemyTrack>().deadZone = true;
        }


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
                gameObject.layer = 9;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                GetComponent<IEnemy>().HarmOnTouch = false;
                GetComponent<IEnemy>().enabled = false;
                GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }

        // If have knockback script, perform knockback
        if (GetComponent<EnemyKnockBack>() != null && byObject.CompareTag("Weapon"))
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
