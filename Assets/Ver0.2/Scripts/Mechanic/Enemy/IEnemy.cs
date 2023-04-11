using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : IEnemyObstacle
{
    public float initialSpeed;
    public float curSpeed;
    public bool stay = false;
    public bool freeze = false;

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
        if(!freeze)
            curSpeed = initialSpeed * ratio;
    }

    public virtual void resetSpeed() {
        if (!freeze)
            curSpeed = initialSpeed;
    }

    public virtual void freezeSelf() {
        chageSpeed(0);        
        if (GetComponent<ObjectGravity>())
        {
            GetComponent<ObjectGravity>().changeGravityScale(0);
        }
        freeze = true;
    }

    public virtual void unfreezeSelf()
    {
        freeze = false;
        resetSpeed();
        if (GetComponent<ObjectGravity>())
        {
            GetComponent<ObjectGravity>().resetGravityScale();
        }
        if (GetComponent<EnemyHP>())
        {
            GetComponent<EnemyHP>().releaseDamage();
        }
    }
}
