using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : IEnemy
{
    [SerializeField] Transform[] wayPoints;
    int wayPointIndex = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 targetPosition = wayPoints[wayPointIndex].transform.position;
        targetPosition.y = transform.position.y;

        transform.position = Vector2.MoveTowards(transform.position,
                        targetPosition,
                        curSpeed * Time.deltaTime);

        if(Mathf.Abs(transform.position.x - wayPoints[wayPointIndex].transform.position.x) < 0.1f)
        {
            wayPointIndex++;
        }

        if(wayPointIndex == wayPoints.Length)
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
