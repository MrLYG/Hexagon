using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] private float detectionRange;
    [SerializeField] private float shootingCD;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private LayerMask blockingLayer;

    [SerializeField] private GameObject Bullet;

    private float shootingCDCount = 0;
    private GameObject m_Player;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            m_Player = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 directToPlayer = m_Player.transform.position - transform.position;
        if ((directToPlayer).magnitude < detectionRange)
        {
            RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, directToPlayer, detectionRange, blockingLayer);

            //Debug.DrawRay(transform.position, directToPlayer * (detectionRange + 0.5f), Color.green, 1);

            if (hitPlayer.collider.gameObject.CompareTag("Player"))
            {
                if (shootingCDCount > shootingCD)
                {
                    GameObject bl = Instantiate(Bullet, transform.position, Quaternion.identity);
                    bl.GetComponent<Bullet>().setTarget(m_Player);
                    bl.GetComponent<Bullet>().setSpeed(bulletSpeed);

                    shootingCDCount = 0;
                }
            }
        }

        shootingCDCount += Time.deltaTime;
    }
}
