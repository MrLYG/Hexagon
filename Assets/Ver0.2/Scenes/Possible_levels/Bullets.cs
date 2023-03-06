using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float distance;
    public float speed;
    public GameObject bullet;
    public float startDelay = 1.5f;
    public float spawnInterval = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireBullet", startDelay, spawnInterval);
    }

    void FireBullet()
    {
        GameObject bulletObject = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletObject.GetComponent<BulletControl>().distance = distance;
        bulletObject.GetComponent<IEnemy>().initialSpeed = speed;
        bulletObject.GetComponent<IEnemy>().curSpeed = speed;
        bulletObject.transform.parent = transform;
    }
}
