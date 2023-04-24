using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IEnemy))]
public class EnemyHP : MonoBehaviour
{
    [Tooltip("Initial HP")]
    [SerializeField] private float initialHP = 2;
    private float hp = 2;
    private List<float> DamageList = new List<float>();
    private List<GameObject> ByObjectList = new List<GameObject>();
    private bool isDead = false;

    private void Start()
    {
        resetHP();
    }

    public void damage(GameObject byObject, float damage)
    {
        if (!GetComponent<IEnemy>().enabled || hp <= 0)
            return;

        if (GetComponent<IEnemy>().freeze)
        {
            storeDamage(byObject, damage);
            return;
        }

        //int damage = byObject.GetComponent<IWeapon>().Damage;
        hp -= damage;

        /*
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
        */

        if (hp <= 0 && !isDead)
        {
            // Deactivate the enemy
            isDead = true;
            if (!GetComponent<IEnemy>().stay)
            {
                StartCoroutine("Death");
                //return;
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Death");
                gameObject.tag = "Object";
                gameObject.layer = 9;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                GetComponent<IEnemy>().HarmOnTouch = false;
                GetComponent<IEnemy>().enabled = false;
                GetComponent<SpriteRenderer>().color = Color.gray;
                GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
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

    IEnumerator Death()
    {
        GetComponent<IEnemy>().HarmOnTouch = false;
        GetComponent<IEnemy>().enabled = false;
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetTrigger("Death");
            yield return new WaitForSeconds(1f);
        }
        gameObject.SetActive(false);
    }

    private void storeDamage(GameObject byObject, float damage)
    {
        ByObjectList.Add(byObject);
        DamageList.Add(damage);
    }

    public void releaseDamage() {

        StartCoroutine("releaseOneDamage");
    }

    IEnumerator releaseOneDamage()
    {
        if (DamageList.Count > 0)
        {
            damage(ByObjectList[0], DamageList[0]);
            DamageList.RemoveAt(0);
            ByObjectList.RemoveAt(0);
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("releaseOneDamage");
    }

    public void resetHP() {
        hp = initialHP;
    }
    public float getHp()
    {
        return hp;
    }
}
