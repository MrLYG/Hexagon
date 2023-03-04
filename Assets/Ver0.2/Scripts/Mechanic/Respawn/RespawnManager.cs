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

    void Update()
    {
        // Respawn via pressing 'R'
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    public void SetCheckPoint(GameObject newCP)
    {
        // Reset Player HP
        m_Player.GetComponent<PlayerHP>().setHP();

        // Reset the color of previous checkpoint
        if (curCP != null)
            curCP.GetComponent<SpriteRenderer>().color = Color.yellow;

        // Assign new checkpoint
        curCP = newCP;
        curCP.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void RespawnPlayer()
    {
        m_Player.GetComponent<PlayerRespawn>().Respawn(curCP, curCP.GetComponent<RespawnPoint>().gravityDirection);
        RespawnEnemies();
    }

    public void RespawnEnemies() {
        foreach (GameObject enemy in curCP.GetComponent<RespawnPoint>().Enemies) {
            enemy.GetComponent<IEnemy>().reset();
        }
    }
}
