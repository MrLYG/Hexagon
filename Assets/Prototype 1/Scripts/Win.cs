using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.SceneManagement;
using System.Linq;

public class Win : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    public string level;
    //public int nextLevel;
    public GameObject analytics;
    private readonly string basePath = "https://hexagon-11bf5-default-rtdb.firebaseio.com";

    private bool isPlayerNearby = false;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Win Win");
            SceneManager.LoadScene(nextLevel);
            generateJson();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true;
            collision.gameObject.GetComponent<PlayerInstruction>().seteText("W");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;
            collision.gameObject.GetComponent<PlayerInstruction>().resetText();
        }
    }

    private Model generateJson()
    {
        
        Model m = new Model();       
        string uuid = PlayerPrefs.GetString("UUID");
        m.uid = uuid;
        m.collectDate = getNowDate();
        m.level = level;
        m.isWin = true;
        m.runTime = analytics.GetComponent<Analytics>().runningTime;
        m.beHits = analytics.GetComponent<Analytics>().beHits;
        m.analyticsEnemiesDict = AnalyticsUtils.GetAllEnemiesObject();
        m.analyticsCheckpointsDict = AnalyticsUtils.GetAllCheckpointsObject();
        Debug.Log(m.ToString());
        //RestClient.Post(basePath + "/hexagon" + ".json?name=YASKJXH9892ASDFA26*&52#&9ASXS851", m.ToString());
        RestClient.Post(basePath + "/hexagon" + ".json", m.ToString());
        //analytics.GetComponent<Analytics>().GetAllEnemies();
        return null;
    }

    private string getNowDate()
    {
        DateTime currentTime = DateTime.Now;
        string timeString = currentTime.ToString("yyyy/MM/dd HH:mm:ss");
        return timeString;
    }
}
