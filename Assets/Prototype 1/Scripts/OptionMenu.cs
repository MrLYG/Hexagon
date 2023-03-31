using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    public GameObject optionMenuUI;
    [SerializeField] private GameObject deathMenu;
    private GameObject m_RespawnManager;
    private bool isDead = false;

    private void Start()
    {
        optionMenuUI.SetActive(false);
        deathMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && !isDead)
        {
            if(Time.timeScale == 0)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        optionMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        optionMenuUI.SetActive(true);
        Time.timeScale = 0f;  
    }

    private void ResumeAfterDeath()
    {
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        isDead = false;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
    }

    public void RestartLevel()
    {
        GameObject door = GameObject.FindWithTag("Door");
        door.GetComponent<Win>().generateJson(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (!isDead)
            Resume();
        else
            ResumeAfterDeath();
    }

    public void RestartCheckPoint()
    {
        if (m_RespawnManager == null) { 
            foreach(GameObject rm in GameObject.FindGameObjectsWithTag("RespawnManager"))
            {
                m_RespawnManager = rm;
            }
        }
        m_RespawnManager.GetComponent<RespawnManager>().RespawnPlayer();
        if (!isDead)
            Resume();
        else
            ResumeAfterDeath();
    }

    public void PlayerDeath(string reason) {
        deathMenu.SetActive(true);
        isDead = true;
        deathMenu.transform.Find("DeathReason").GetComponent<TextMeshProUGUI>().text = "You are killed by " + reason;
        Time.timeScale = 0f;
    }
}
