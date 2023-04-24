using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : IEnemy
{
    [SerializeField] Transform[] wayPoints;
    private Animator m_Anim;
    int wayPointIndex = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        m_Anim = GetComponent<Animator>();
        if (wayPoints.Length > 0)
        {
            transform.position = wayPoints[wayPointIndex].transform.position;
            if(m_Anim)
                m_Anim.SetBool("Moving", true);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (wayPoints.Length > 0)
            Move();
    }

    void Move()
    {
        Vector2 targetPosition = wayPoints[wayPointIndex].transform.position;
        targetPosition.y = transform.position.y;

        if (GetComponent<SpriteRenderer>())
        {
            if (targetPosition.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position,
                        targetPosition,
                        curSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - wayPoints[wayPointIndex].transform.position.x) < 0.1f)
        {
            wayPointIndex++;
        }

        if (wayPointIndex == wayPoints.Length)
        {
            wayPointIndex = 0;
        }
    }

    public override void reset()
    {
        base.reset();
        wayPointIndex = 0;
    }
}
