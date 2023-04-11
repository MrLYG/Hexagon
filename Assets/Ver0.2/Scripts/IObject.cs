using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IObject : MonoBehaviour
{
    public float initialSpeed;
    public float curSpeed;
    public bool freeze = false;

    public virtual void Start()
    {
        curSpeed = initialSpeed;
    }

    public virtual void chageSpeed(float ratio)
    {
        if (!freeze)
            curSpeed = initialSpeed * ratio;
    }

    public virtual void resetSpeed()
    {
        if (!freeze)
            curSpeed = initialSpeed;
    }

    public virtual void freezeSelf()
    {
        chageSpeed(0);
        freeze = true;
    }

    public virtual void unfreezeSelf()
    {
        freeze = false;
        resetSpeed();
    }
}
