using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float InitUpwardSpeed;
    [SerializeField] private float InitLRSpeed;
    [SerializeField] private float RemoveTime;

    private float upwardSpeed;
    private float lrSpeed;

    private void Start()
    {
        Invoke("RemoveText", RemoveTime);
        upwardSpeed = InitUpwardSpeed;
        lrSpeed = Random.Range(-InitLRSpeed, InitLRSpeed);
    }

    private void Update()
    {
        upwardSpeed -= 9.8f * Time.deltaTime;
        Vector3 curSpeed = new Vector2(lrSpeed, upwardSpeed);
        transform.position += curSpeed * Time.deltaTime;
    }

    private void RemoveText()
    {
        Destroy(gameObject);
    }
}
