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
            //Debug.Log("Win Win");
            PlayerPrefs.DeleteKey("RespawnX");
            PlayerPrefs.DeleteKey("RespawnY");
            PlayerPrefs.DeleteKey("RespawnXC");
            PlayerPrefs.DeleteKey("RespawnYC");
            SceneManager.LoadScene(nextLevel);
            //GetComponent<LevelSelection>().StartGame();

            /*
            if(int.Parse(level) <= 10)
            {
                generateJson(true);
            }
            */
            
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

    public Model generateJson(bool win)
    {
        
        Model m = new Model();       
        string uuid = PlayerPrefs.GetString("UUID");
        m.uid = uuid;
        m.collectDate = getNowDate();
        m.level = level;
        m.isWin = win;
        m.runTime = analytics.GetComponent<Analytics>().runningTime;
        m.beHits = analytics.GetComponent<Analytics>().beHits;

        m.analyticsEnemiesDict = AnalyticsUtils.GetAllEnemiesObjectByInital(analytics.GetComponent<Analytics>().allEnemies);
        m.analyticsCheckpointsDict = AnalyticsUtils.GetAllCheckpointsObject();
        m.playerNumOfBluelight = analytics.GetComponent<Analytics>().playerNumOfBluelight;
        m.playerNumOfGreenlight = analytics.GetComponent<Analytics>().playerNumOfGreenlight;

        Debug.Log(m.ToString());
        //RestClient.Post(basePath + "/hexagon" + ".json?name=YASKJXH9892ASDFA26*&52#&9ASXS851", m.ToString());
        RestClient.Post(basePath + "/mid" + ".json", m.ToString());
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
