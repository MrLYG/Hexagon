using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : MonoBehaviour
{
    public float initialSpeed;
    public float curSpeed;
    public bool stay = false;

    public Vector3 initialPos;

    public virtual void Start()
    {
        curSpeed = initialSpeed;
        initialPos = transform.position;
    }

    public virtual void damage(GameObject byObject, float damage)
    {
        if(GetComponent<EnemyHP>())
            GetComponent<EnemyHP>().damage(byObject, damage);
    }

    public virtual void reset() {
        gameObject.SetActive(true);
        transform.position = initialPos;
        if (GetComponent<EnemyHP>())
            GetComponent<EnemyHP>().resetHP();
    }

    public virtual void chageSpeed(float ratio){
        curSpeed = initialSpeed * ratio;
    }

    public virtual void resetSpeed() {
        curSpeed = initialSpeed;
    }
}
