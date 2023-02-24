using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWeapon : MonoBehaviour
{
    public float AttackCD;
    public int Damage;
    public Vector3 InitialPos;
    public Vector3 InitialRot;
    public Vector3 CurPos;
    public Vector3 CurRot;
    public GameObject m_Player;
    public bool Reverse;

    public virtual void StartAttack()
    {

    }
    public virtual void Attack()
    {

    }
    public virtual void SetUp(GameObject Player)
    {
        if (m_Player == null)
        {
            m_Player = Player;
            transform.parent = m_Player.transform;
        }
        CurPos = InitialPos;
        CurRot = InitialRot;
        Reverse = false;
        Reset();
    }

    public virtual void Reset()
    {
        transform.localPosition = CurPos;
        transform.localRotation = Quaternion.Euler(CurRot);
    }

    public virtual void SwitchSide()
    {
        //facingRight = !facingRight;
    }

    public virtual void ReverseWeapon() {
        Reverse = !Reverse;
    }
}
