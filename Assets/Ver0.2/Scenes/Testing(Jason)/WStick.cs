using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WStick : IWeapon
{
    private bool Attacking = false;
    
    [SerializeField] private float AttackSpeed = 3f;
    [SerializeField] private Vector3 EndLocationRotation;
    private Vector3 CurELR;
    private float switchTimes = 0;
    private List<GameObject> hitTargets = new List<GameObject>();

    public override void StartAttack()
    {
        Attacking = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
    public override void Attack()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(CurELR), AttackSpeed * Time.deltaTime);
        float zDiff = transform.localRotation.eulerAngles.z - CurELR.z;
        if (Mathf.Abs(zDiff % 360 - 360) <= 10 || Mathf.Abs(zDiff % 360) <= 10)
        {
            Attacking = false;
            Reset();
            if(switchTimes % 2 != 0)
            {
                SwitchSide();
            }
        }
    }
    public override void SetUp(GameObject Player)
    {
        base.SetUp(Player);
        CurELR = EndLocationRotation;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public override void Reset()
    {
        base.Reset();
        hitTargets.Clear();
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void FixedUpdate()
    {
        if (Attacking)
        {
            Attack();
        }
    }
    public override void SwitchSide()
    {
        base.SwitchSide();
        bool facingRight = m_Player.GetComponent<PlayerControl>().facingRight;
        if (!Attacking)
        {
            switchTimes = 0;
            if (!facingRight)
            {
                if (!Reverse)
                {
                    CurPos = new Vector3(InitialPos.x * -1, InitialPos.y, InitialPos.z);
                    CurELR = new Vector3(0, 0, EndLocationRotation.z * -1);
                }
                else {
                    CurPos = InitialPos;
                    CurELR = EndLocationRotation;
                }
            }
            else
            {
                if (!Reverse)
                {
                    CurPos = InitialPos;
                    CurELR = EndLocationRotation;
                }
                else
                {
                    CurPos = new Vector3(InitialPos.x * -1, InitialPos.y, InitialPos.z);
                    CurELR = new Vector3(0, 0, EndLocationRotation.z * -1);
                }
            }
            Reset();
        }
        else
        {
            switchTimes++;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!hitTargets.Contains(collision.gameObject))
            {
                collision.gameObject.GetComponent<EnemyHP>().damage(gameObject, Damage);
                hitTargets.Add(collision.gameObject);
            }
        }
    }
}
