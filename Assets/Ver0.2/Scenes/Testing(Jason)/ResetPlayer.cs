using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    [SerializeField] private GameObject m_RepawnManager;
    void Start()
    {
        if (m_RepawnManager == null)
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("RespawnManager"))
            {
                m_RepawnManager = gameObject;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_RepawnManager.GetComponent<RespawnManager>().RespawnPlayer();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
