using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGear : EnemyControl
{
    [SerializeField] private bool clockwise;

    public override void Update()
    {
        base.Update();
        if (!clockwise)
            transform.eulerAngles += new Vector3(0, 0, 1) * curSpeed * 20 * Time.deltaTime;
        else
            transform.eulerAngles -= new Vector3(0, 0, 1) * curSpeed * 20 * Time.deltaTime;

    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HarmOnTouch && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHP>().getHurt(Damage, InvincibleTime, gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (HarmOnTouch && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHP>().getHurt(Damage, InvincibleTime, gameObject);
        }
    }
}
