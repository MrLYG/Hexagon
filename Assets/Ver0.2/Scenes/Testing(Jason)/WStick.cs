using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WStick : IWeapon
{
    private bool Attacking = false;
    
    [SerializeField] private float AttackSpeed = 3f;
    [SerializeField] private Vector3 EndLocationRotation;
    private float switchTimes = 0;

    public override void StartAttack()
    {
        Attacking = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
    public override void Attack()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(EndLocationRotation), AttackSpeed * Time.deltaTime);
        float zDiff = transform.eulerAngles.z - EndLocationRotation.z;
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
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public override void Reset()
    {
        base.Reset();
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
        if (!Attacking)
        {
            switchTimes = 0;
            InitialPos = new Vector3(InitialPos.x * -1, InitialPos.y, InitialPos.z);
            EndLocationRotation = new Vector3(0, 0, EndLocationRotation.z * -1);
            Reset();
        }
        else
        {
            switchTimes++;
        }
    }
}
