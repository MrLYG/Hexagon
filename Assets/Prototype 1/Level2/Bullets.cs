using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public GameObject bullet;
    public float startDelay = 1.5f;
    public float spawnInterval = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireBullet", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FireBullet()
    {
        Instantiate(bullet, transform.position, bullet.transform.rotation);

    }
}
