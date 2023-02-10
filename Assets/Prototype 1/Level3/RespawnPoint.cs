using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject m_RespawnManager;
    public GravityDirection gravityDirection;

    private void Start()
    {
        // Get reference of RespawnManager
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("RespawnManager"))
        {
            m_RespawnManager = gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_RespawnManager.GetComponent<RespawnManager>().SetCheckPoint(gameObject);
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
