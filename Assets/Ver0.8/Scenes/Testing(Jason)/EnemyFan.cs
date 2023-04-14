using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFan : IEnemy
{
    [SerializeField] private bool clockwise;
    private void Update()
    {
        if(!clockwise)
            transform.eulerAngles += new Vector3(0, 0, 1) * curSpeed * Time.deltaTime;
        else
            transform.eulerAngles -= new Vector3(0, 0, 1) * curSpeed * Time.deltaTime;
    }
}
