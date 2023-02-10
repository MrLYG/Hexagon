using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private GameObject curCP;
    [SerializeField] private GameObject m_Player;

    private void Start()
    {
        // Get reference of Player
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            m_Player = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    public void SetCheckPoint(GameObject newCP)
    {
        curCP = newCP;
    }

    private void RespawnPlayer()
    {
        m_Player.GetComponent<PlayerRespawn>().Respawn(curCP, curCP.GetComponent<RespawnPoint>().gravityDirection);
    }
}
