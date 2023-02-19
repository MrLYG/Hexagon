using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float moveSpeed = 2f;
    int wayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
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
                        moveSpeed * Time.deltaTime);

        if(transform.position == wayPoints[wayPointIndex].transform.position)
        {
            wayPointIndex++;
        }

        if(wayPointIndex == wayPoints.Length)
        {
            wayPointIndex = 0;
        }
    }
}
