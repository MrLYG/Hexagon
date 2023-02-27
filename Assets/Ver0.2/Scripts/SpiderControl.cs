using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderControl : IEnemy
{
    [SerializeField] private Transform[] wayPoints;
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
        transform.position = Vector2.MoveTowards(transform.position,
                        wayPoints[wayPointIndex].transform.position,
                        curSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y - wayPoints[wayPointIndex].transform.position.y) < 0.1f)
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
