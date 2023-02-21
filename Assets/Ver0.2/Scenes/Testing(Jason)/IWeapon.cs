using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWeapon : MonoBehaviour
{
    public float AttackCD;
    public int Damage;
    public Vector3 InitialPos;
    public Vector3 InitialRot;

    public virtual void StartAttack()
    {

    }
    public virtual void Attack()
    {

    }
    public virtual void SetUp(GameObject Player)
    {
        transform.parent = Player.transform;
        transform.localPosition = InitialPos;
        transform.localRotation = Quaternion.Euler(InitialRot);
    }

    public virtual void Reset()
    {
        transform.localPosition = InitialPos;
        transform.localRotation = Quaternion.Euler(InitialRot);
    }

    public virtual void SwitchSide()
    {

    }
}
