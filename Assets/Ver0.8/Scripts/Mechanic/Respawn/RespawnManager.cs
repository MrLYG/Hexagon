using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    private GameObject curCP;
    [SerializeField] private GameObject m_Player;
    [SerializeField] private GameObject m_Clone;

    private void Start()
    {
        // Get reference of Player
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
        {
             m_Player = gameObject;
        }
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Clone"))
        {
            m_Clone = gameObject;
        }

        if (PlayerPrefs.HasKey("RespawnX"))
        {
            float respawnX = PlayerPrefs.GetFloat("RespawnX");
            float respawnY = PlayerPrefs.GetFloat("RespawnY");
            m_Player.transform.position = new Vector2(respawnX, respawnY);
            PlayerPrefs.DeleteKey("RespawnX");
            PlayerPrefs.DeleteKey("RespawnY");
        }

        if (PlayerPrefs.HasKey("RespawnXC") && m_Clone != null)
        {
            float respawnX = PlayerPrefs.GetFloat("RespawnXC");
            float respawnY = PlayerPrefs.GetFloat("RespawnYC");
            m_Clone.transform.position = new Vector2(respawnX, respawnY);
            PlayerPrefs.DeleteKey("RespawnXC");
            PlayerPrefs.DeleteKey("RespawnYC");
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
        //m_Player.GetComponent<PlayerHP>().setHP();

        // Reset the color of previous checkpoint
        if (curCP != null)
            curCP.GetComponent<RespawnPoint>().DeActivate();

        PlayerPrefs.DeleteKey("RespawnXC");
        PlayerPrefs.DeleteKey("RespawnYC");
        // Assign new checkpoint
        curCP = newCP;
        PlayerPrefs.SetFloat("RespawnX", curCP.transform.position.x);
        PlayerPrefs.SetFloat("RespawnY", curCP.transform.position.y);
    }

    public void SetCheckPoint(GameObject newCP, GameObject newCPC)
    {
        if (curCP != null)
            curCP.GetComponent<RespawnPoint>().DeActivate();

        // Assign new checkpoint
        curCP = newCP;
        PlayerPrefs.SetFloat("RespawnX", curCP.transform.position.x);
        PlayerPrefs.SetFloat("RespawnY", curCP.transform.position.y);

        PlayerPrefs.SetFloat("RespawnXC", newCPC.transform.position.x);
        PlayerPrefs.SetFloat("RespawnYC", newCPC.transform.position.y);
    }

    public void RespawnPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        /*
        m_Player.GetComponent<PlayerRespawn>().Respawn(curCP, curCP.GetComponent<RespawnPoint>().gravityDirection);
        if(m_Clone)
            m_Clone.GetComponent<PlayerRespawn>().Respawn(curCPClone, curCPClone.GetComponent<RespawnPoint>().gravityDirection);
        RespawnEnemies();
        RespawnObjects();
        */
    }

    public void RespawnEnemies() {
        foreach (GameObject enemy in curCP.GetComponent<RespawnPoint>().Enemies) {
            enemy.GetComponent<IEnemy>().enabled = true;
            enemy.tag = "Enemy";
            enemy.GetComponent<SpriteRenderer>().color = Color.red;
            enemy.GetComponent<IEnemy>().reset();
        }
    }

    public void RespawnObjects()
    {
        int index = 0;
        foreach (GameObject obj in curCP.GetComponent<RespawnPoint>().Objects)
        {
            obj.transform.position = curCP.GetComponent<RespawnPoint>().ObjectPositions[index];
            index++;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("RespawnX");
        PlayerPrefs.DeleteKey("RespawnY");
        PlayerPrefs.DeleteKey("RespawnXC");
        PlayerPrefs.DeleteKey("RespawnYC");
    }
}
